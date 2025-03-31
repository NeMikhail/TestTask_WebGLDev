using UnityEngine;

namespace GameCoreModule
{
    public class CanvasList : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseCanvas;
        
        public GameObject PauseCanvas => _pauseCanvas;

    }
}
