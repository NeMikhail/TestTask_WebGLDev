using UnityEngine;

namespace Economy
{
    [CreateAssetMenu(fileName = "ProductConfig", menuName = "SO/Config/Economy/ProductConfig")]
    public class ProductConfig : ScriptableObject
    {
        [SerializeField] private string _productName;
        [SerializeField] private Sprite _productSprite;
        
        public string ProductName => _productName;
        public Sprite ProductSprite => _productSprite;
    }
}