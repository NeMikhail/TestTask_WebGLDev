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
        private InputSystem_Actions _inputSystemActions;
        private PlayerView _playerView;
        private PlayerConfig _config;
        private PlayerInventory _playerInventory;
        private PlayerEventBus _playerEventBus;
        
        private Camera _camera;
        private Transform _cameraTransform;
        private Vector2 _cameraMoveVector;
        private Vector3 _playerTargetPosition;
        private int _animIDSpeed;
        private bool _isWalking;
        private int _uILayer;

        [Inject]
        public void Construct(InputSystem_Actions inputSystemActions, PlayerView playerView, PlayerConfig config,
            PlayerInventory playerInventory, PlayerEventBus playerEventBus)
        {
            _inputSystemActions = inputSystemActions;
            _playerView = playerView;
            _config = config;
            _playerInventory = playerInventory;
            _playerEventBus = playerEventBus;
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
            _uILayer = LayerMask.NameToLayer("UI");
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
        }
        
        private void GetProductItem(ProductID productID)
        {
            InventoryItem item = _playerInventory.GetItem(productID);
            _playerEventBus.OnSendProductInventoryItem?.Invoke(item);
        }
        
        public bool IsPointerOverUIElement()
        {
            return IsPointerOverUIElement(GetEventSystemRaycastResults());
        }
        
        private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
        {
            for (int index = 0; index < eventSystemRaysastResults.Count; index++)
            {
                RaycastResult curRaysastResult = eventSystemRaysastResults[index];
                if (curRaysastResult.gameObject.layer == _uILayer)
                    return true;
            }
            return false;
        }
        
        static List<RaycastResult> GetEventSystemRaycastResults()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            if (Touchscreen.current != null)
            {
                if (Touchscreen.current.touches.Count > 0)
                {
                    eventData.position = Touchscreen.current.position.ReadValue();
                }
            }
            else
            {
                eventData.position = Mouse.current.position.ReadValue();
            }
            List<RaycastResult> raysastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raysastResults);
            return raysastResults;
        }
        
        private void CheckTargetPosition(InputAction.CallbackContext callbackContext)
        {
            Vector3 targetPos = Vector3.zero;
            if (IsPointerOverUIElement())
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
            RaycastHit hit;
            LayerMask layerMask = LayerMask.GetMask("Floor", "InteractableObject");
            Vector3 direction = (targetPos - _cameraTransform.position).normalized;
            if (Physics.Raycast(_cameraTransform.position, direction, out hit, Mathf.Infinity, layerMask))

            { 
                direction = (hit.point - _playerView.gameObject.transform.position).normalized;
                _playerTargetPosition = hit.point;
                _isWalking = true;
            }
            else
            { 
                return;
            }
        }
        
        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
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
                Debug.Log("Idle");
                _playerView.Animator.SetFloat(_animIDSpeed, 0);
            }
        }

        private void PlayWalkAnimation()
        {
            
            if (_playerView.Animator.GetFloat(_animIDSpeed) != 2f)
            {
                Debug.Log("Walk");
                _playerView.Animator.SetFloat(_animIDSpeed, 2f);
            }
            
        }

        private void MovePlayer(float fixedDeltaTime)
        {
            _playerView.PlayerAgent.SetDestination(_playerTargetPosition);
            if (Vector3.Distance(_playerView.gameObject.transform.position, _playerTargetPosition) < 1.5f)
            {
                _isWalking = false;
                PlayIdleAnimation();
            }
        }

        private void MoveCamera()
        {
            _playerView.CameraRigidbody.linearVelocity =
                new Vector3(_cameraMoveVector.x * 10, 0, _cameraMoveVector.y * 10);
        }
    }
}