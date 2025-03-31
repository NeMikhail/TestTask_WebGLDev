using MAEngine.Extention;
using UnityEngine;

namespace Economy
{
    [CreateAssetMenu(fileName = "ProductionBuildingConfig", menuName = "SO/Config/Economy/ProductionBuildingConfig")]
    public class ProductionBuildingConfig : ScriptableObject
    {
        [SerializeField] private ProductID _productID;
        [SerializeField] private SerializableDictionary<Tiers, ProductionSettings> _productionTiersSettings;
        
        public ProductID ProductID => _productID;
        public SerializableDictionary<Tiers, ProductionSettings> ProductionTiersSettings => _productionTiersSettings;
    }
}