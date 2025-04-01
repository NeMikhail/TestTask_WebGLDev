using System;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _playerAgent;
        [SerializeField] private Rigidbody _cameraRigidbody;
        [SerializeField] private Animator _animator;

        public NavMeshAgent PlayerAgent => _playerAgent;
        public Rigidbody CameraRigidbody => _cameraRigidbody;
        public Animator Animator => _animator;
    }
}



