using System;
using System.Collections.Generic;
using GameCoreModule;
using MAEngine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;

namespace Player
{
    public class PlayerActions : IAction, IInitialisation, ICleanUp, IFixedExecute, IExecute
    {
        private const string FLOOR_LAYER_NAME = "Floor";
        private const string INTERACTABLE_LAYER_NAME = "InteractableObject";
        
        private InputSystem_Actions _inputSystemActions;
        private PlayerView _playerView;
        private PlayerConfig _config;
        private PlayerInventory _playerInventory;
        private PlayerEventBus _playerEventBus;
        private GameEventBus _gameEventBus;
        private UIEventBus _uiEventBus;
        private AudioEventBus _audioEventBus;
        
        private Camera _camera;
        private Transform _cameraTransform;
        private Vector2 _cameraMoveVector;
        private Vector3 _playerTargetPosition;
        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDMotionSpeed;
        private bool _isWalking;
        private GameObject _markerObject;


        [Inject]
        public void Construct(InputSystem_Actions inputSystemActions, PlayerView playerView, PlayerConfig config,
            PlayerInventory playerInventory,
            PlayerEventBus playerEventBus, GameEventBus gameEventBus,
            UIEventBus uiEventBus, AudioEventBus audioEventBus)
        {
            _inputSystemActions = inputSystemActions;
            _playerView = playerView;
            _config = config;
            _playerInventory = playerInventory;
            _playerEventBus = playerEventBus;
            _gameEventBus = gameEventBus;
            _uiEventBus = uiEventBus;
            _audioEventBus = audioEventBus;
        }
        
        public void Initialisation()
        {
            _camera = Camera.main;
            _cameraTransform = _camera.transform;
            _inputSystemActions.Enable();
            AssignAnimationIDs();
            _playerTargetPosition = _playerView.transform.position;
            InitializeParametrs();
            InitializeInventory();
            _inputSystemActions.Player.Attack.performed += CheckTargetPosition;
            _playerEventBus.OnGetProductItem += GetProductItem;
        }

        public void Cleanup()
        {
            _inputSystemActions.Player.Attack.performed -= CheckTargetPosition;
            _playerEventBus.OnGetProductItem -= GetProductItem;
        }
        
        public void Execute(float deltaTime)
        {
            _cameraMoveVector = _inputSystemActions.Player.Move.ReadValue<Vector2>();
        }

        public void FixedExecute(float fixedDeltaTime)
        {
            MoveCamera();
            MovePlayer(fixedDeltaTime);
            AnimatePlayer();
        }
        
        private void InitializeParametrs()
        {
            _playerView.PlayerAgent.speed = _config.Speed;
        }
        
        private void InitializeInventory()
        {
            _playerInventory.InitializeInventory(_config);
            _playerEventBus.OnPlayerInventoryInitialized(_playerInventory);
        }
        
        private void GetProductItem(ProductID productID, InventoryItemCallback callback)
        {
            InventoryItem item = _playerInventory.GetItem(productID);
            callback.SetInventoryItem(item);
        }
        
        private void CheckTargetPosition(InputAction.CallbackContext callbackContext)
        {
            Vector3 targetPos = Vector3.zero;
            PointerCheckEventCallback callback = new PointerCheckEventCallback();
            _uiEventBus.OnPointerCheck?.Invoke(callback);
            if (callback.IsPointerOverUI)
            {
                return;
            }

            if (Touchscreen.current != null)
            {
                if (Touchscreen.current.touches.Count > 0)
                {
                    Vector2 touchPosition = Touchscreen.current.position.ReadValue();
                    targetPos =
                        Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, 5));
                }
            }
            else
            {
                Vector2 mousePosition = Mouse.current.position.ReadValue();
                targetPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 5));
            }
#if PLATFORM_WEBGL
            Vector2 pointerPosition = Mouse.current.position.ReadValue();
            targetPos = Camera.main.ScreenToWorldPoint(new Vector3(pointerPosition.x, pointerPosition.y, 5));
#endif
            RaycastHit hit;
            LayerMask layerMask = LayerMask.GetMask(FLOOR_LAYER_NAME, INTERACTABLE_LAYER_NAME);
            Vector3 direction = (targetPos - _cameraTransform.position).normalized;
            if (Physics.Raycast(_cameraTransform.position, direction, out hit, Mathf.Infinity,
                    layerMask))
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.layer == LayerMask.NameToLayer(FLOOR_LAYER_NAME))
                {
                    ChangeTargetPosition(hit);
                }
                else
                {
                    _playerEventBus.OnTryInteractWithObject?.Invoke(hitObject);
                }
            }
            else
            { 
                return;
            }
        }

        private void ChangeTargetPosition(RaycastHit hit)
        {
            Vector3 direction;
            direction = (hit.point - _playerView.gameObject.transform.position).normalized;
            _playerTargetPosition = hit.point;
            _isWalking = true;
            if (_markerObject == null)
            {
                GameObjectSpawnCallback callback = new GameObjectSpawnCallback();
                _gameEventBus.OnSpawnObject?.Invoke(PrefabID.TargetPosPerfab, _playerTargetPosition,
                    _playerView.gameObject.transform.parent, callback);
                SetupSpawndMarker(callback.SpawnedObject);
            }
            else
            {
                _markerObject.SetActive(true);
                _markerObject.transform.position = _playerTargetPosition;
            }
            _audioEventBus.OnPlaySound?.Invoke(SoundID.SoundClick1);
        }

        private void SetupSpawndMarker(GameObject markerObject)
        {
            _markerObject = markerObject;
        }


        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }

        private void AnimatePlayer()
        {
            if (_isWalking)
            {
                PlayWalkAnimation();
            }
            else
            {
                PlayIdleAnimation();
            }
        }

        private void PlayIdleAnimation()
        {
            if (_playerView.Animator.GetFloat(_animIDSpeed) != 0f)
            {
                _playerView.Animator.SetFloat(_animIDSpeed, 0);
                _playerView.Animator.SetFloat(_animIDMotionSpeed, 1);
            }
        }

        private void PlayWalkAnimation()
        {
            
            if (_playerView.Animator.GetFloat(_animIDSpeed) != 2f)
            {
                _playerView.Animator.SetFloat(_animIDSpeed, 2f);
                _playerView.Animator.SetFloat(_animIDMotionSpeed, 1);
            }
            
        }

        private void MovePlayer(float fixedDeltaTime)
        {
            _playerView.PlayerAgent.SetDestination(_playerTargetPosition);
            if (Vector3.Distance(_playerView.gameObject.transform.position, _playerTargetPosition) < 1.5f)
            {
                _isWalking = false;
                PlayIdleAnimation();
                if (_markerObject != null)
                {
                    _markerObject.SetActive(false);
                }
            }
        }

        private void MoveCamera()
        {
            _playerView.CameraRigidbody.linearVelocity =
                new Vector3(_cameraMoveVector.x * 10, 0, _cameraMoveVector.y * 10);
        }
    }
}