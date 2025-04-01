using UnityEngine;
using Zenject;

namespace UI
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private GUIView _view;
        
        public override void InstallBindings()
        {
            Container.Bind<GUIView>().FromInstance(_view).AsSingle();
            
            Container.Bind<UIActions>().AsSingle();
            Container.Bind<GUIActions>().AsSingle();
        }
    }
}