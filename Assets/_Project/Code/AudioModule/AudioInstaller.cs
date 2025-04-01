using UnityEngine;
using Zenject;

namespace Audio
{
    public class AudioInstaller : MonoInstaller
    {
        [SerializeField] private AudioSourceView _audioSourceView;
        public override void InstallBindings()
        {
            Container.Bind<AudioSourceView>().FromInstance(_audioSourceView).AsSingle();
            
            Container.Bind<AudioOperator>().AsSingle();
        }
    }
}