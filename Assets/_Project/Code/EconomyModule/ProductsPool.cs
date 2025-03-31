using MAEngine.Extention;
using UnityEngine;

namespace Economy
{
    [CreateAssetMenu(fileName = "ProductsPool", menuName = "SO/Config/Economy/ProductsPool")]
    public class ProductsPool : ScriptableObject
    {
        [SerializeField] private SerializableDictionary<ProductID, ProductConfig> _productsDict;
        
        public SerializableDictionary<ProductID, ProductConfig> ProductsDict => _productsDict;
    }
}