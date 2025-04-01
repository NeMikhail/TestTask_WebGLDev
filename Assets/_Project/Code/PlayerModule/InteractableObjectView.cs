using MAEngine;
using UnityEngine;

namespace Player
{
    public class InteractableObjectView : MonoBehaviour
    {
        [SerializeField] private Scene3DActor _scene3DActor;

        private bool _isInteractable;
        
        public bool IsInteractable => _isInteractable;

        private void OnEnable()
        {
            _scene3DActor.TriggerEnter += CheckPlayerEnterTrigger;
            _scene3DActor.TriggerExit += CheckPlayerExitTrigger;
        }
        
        private void OnDisable()
        {
            _scene3DActor.TriggerEnter -= CheckPlayerEnterTrigger;
            _scene3DActor.TriggerExit -= CheckPlayerExitTrigger;
        }

        private void CheckPlayerEnterTrigger(Scene3DActor actor, Collider collider)
        {
            if (collider.CompareTag("Player"))
            {
                _isInteractable = true;
            }
        }
        
        private void CheckPlayerExitTrigger(Scene3DActor actor, Collider collider)
        {
            if (collider.CompareTag("Player"))
            {
                _isInteractable = false;
            }
        }
    }
}