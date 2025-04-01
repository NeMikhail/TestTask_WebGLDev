using Economy;
using MAEngine.Extention;
using Zenject;

namespace Player
{
    public class PlayerInventory
    {
        private ProductsPool _productsPool;
        private SerializableDictionary<ProductID, InventoryItem> _productionInventory;
        
        public SerializableDictionary<ProductID, InventoryItem> ProductionInventory => _productionInventory;

        [Inject]
        public void Construct(ProductsPool productsPool)
        {
            _productsPool = productsPool;
        }
        

        public void InitializeInventory(PlayerConfig config)
        {
            _productionInventory = new SerializableDictionary<ProductID, InventoryItem>();
            for (int i = 0; i < config.StorableProducts.Length; i++)
            {
                ProductID itemID = config.StorableProducts.GetKeyByIndex(i);
                int maxValue = config.StorableProducts.GetValueByIndex(i);
                InventoryItem inventoryItem = new InventoryItem(_productsPool.ProductsDict[itemID].ProductSprite,
                    _productsPool.ProductsDict[itemID].ProductName, 0, maxValue);

                _productionInventory.Add(itemID, inventoryItem);
            }
        }
        
        public void AddItemValue(ProductID productID, int value)
        {
            _productionInventory[productID].AddValue(value);
        }

        public int GetItemValue(ProductID productID)
        {
            return _productionInventory[productID].ItemValue;
        }

        public InventoryItem GetItem(ProductID productID)
        {
            return _productionInventory.IsContainsKey(productID)? _productionInventory[productID] : null;
        }

    }
}