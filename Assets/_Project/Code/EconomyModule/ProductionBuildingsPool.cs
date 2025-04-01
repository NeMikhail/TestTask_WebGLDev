using MAEngine.Extention;
using UnityEngine;

namespace Economy
{
    [CreateAssetMenu(fileName = "ProductionBuildingsPool", menuName = "SO/Config/Economy/ProductionBuildingsPool")]
    public class ProductionBuildingsPool : ScriptableObject
    {
        [SerializeField]
        private SerializableDictionary<ProductionBuildingID, ProductionBuildingConfig> _productionBuildings;
        
        public SerializableDictionary<ProductionBuildingID, ProductionBuildingConfig> ProductionBuildings =>
            _productionBuildings;
    }
}