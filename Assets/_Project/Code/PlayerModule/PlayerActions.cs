using MAEngine;
using MAEngine.Extention;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using Zenject;

namespace Player
{
    public class PlayerActions : IAction, IInitialisation, ICleanUp, IFixedExecute, IExecute
    {
        private InputSystem_Actions _inputSystemActions;
        private PlayerView _playerView;
        
        private Camera _camera;
        private Transform _cameraTransform;
        private Vector2 _cameraMoveVector;
        private Vector3 _playerTargetPosition;
        private int _animIDSpeed;
        private bool _isWalking;

        [Inject]
        public void Construct(InputSystem_Actions inputSystemActions, PlayerView playerView)
        {
            _inputSystemActions = inputSystemActions;
            _playerView = playerView;
        }
        
        public void Initialisation()
        {
            _camera = Camera.main;
            _cameraTransform = _camera.transform;
            _inputSystemActions.Enable();
            _inputSystemActions.Player.Attack.performed += CheckTargetPosition;
            AssignAnimationIDs();
            _playerTargetPosition = _playerView.transform.position;
        }

        public void Cleanup()
        {
            _inputSystemActions.Player.Attack.performed -= CheckTargetPosition;
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
        
        private void CheckTargetPosition(InputAction.CallbackContext obj)
        {
            Vector3 targetPos = Vector3.zero;
            
            if (UnityEngine.Input.touchCount > 0)
            {
                Touch touch = UnityEngine.Input.GetTouch(0);
                
                targetPos =
                    Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 5));
            }
            else
            {
                Vector3 mousePosition = Input.mousePosition;
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