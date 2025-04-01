using System;
using Player;

namespace GameCoreModule
{
    public class PlayerEventBus
    {
        private Action<PlayerInventory> _onPlayerInventoryInitialized;
        private Action<ProductID, InventoryItemCallback> _onGetProductItem;
        
        public Action<PlayerInventory> OnPlayerInventoryInitialized
        { get => _onPlayerInventoryInitialized; set => _onPlayerInventoryInitialized = value; }
        public Action<ProductID, InventoryItemCallback> OnGetProductItem 
        { get => _onGetProductItem; set => _onGetProductItem = value; }
    }
}