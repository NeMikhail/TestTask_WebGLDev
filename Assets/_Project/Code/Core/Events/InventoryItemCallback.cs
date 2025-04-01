using Player;

namespace GameCoreModule
{
    public class InventoryItemCallback
    {
        private InventoryItem _inventoryItem;

        public InventoryItem InventoryItem => _inventoryItem;

        public void SetInventoryItem(InventoryItem inventoryItem)
        {
            _inventoryItem = inventoryItem;
        }
    }
}