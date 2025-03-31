using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerView _playerView;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerView>().FromInstance(_playerView).AsSingle();
            
            Container.Bind<InputSystem_Actions>().AsSingle();
            Container.Bind<PlayerActions>().AsSingle();
            
        }
    }
}