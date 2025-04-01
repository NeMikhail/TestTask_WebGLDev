using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private PlayerConfig _playerConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerView>().FromInstance(_playerView).AsSingle();
            
            Container.Bind<PlayerConfig>().FromInstance(_playerConfig).AsSingle();
            
            Container.Bind<InputSystem_Actions>().AsSingle();
            Container.Bind<PlayerInventory>().AsSingle();
            Container.Bind<PlayerActions>().AsSingle();
            
        }
    }
}