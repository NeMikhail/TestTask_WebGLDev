using UnityEngine;
using Zenject;

namespace Economy
{
    public class EconomyInstaller : MonoInstaller
    {
        [SerializeField] private ProductionBuildingsView _productionBuildingsView;
        
        [SerializeField] private ProductsPool _productsPool;
        [SerializeField] private ProductionBuildingsPool _productionBuildingsPool;
        
        public override void InstallBindings()
        {
            Container.Bind<ProductionBuildingsView>().FromInstance(_productionBuildingsView).AsSingle();
            
            Container.Bind<ProductsPool>().FromInstance(_productsPool).AsSingle();
            Container.Bind<ProductionBuildingsPool>().FromInstance(_productionBuildingsPool).AsSingle();

            Container.Bind<ProductionActions>().AsSingle();
        }
    }
}