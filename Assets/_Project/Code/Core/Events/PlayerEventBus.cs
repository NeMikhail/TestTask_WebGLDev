using System;

namespace GameCoreModule
{
    public class PlayerEventBus
    {
        private Action<ProductID, InventoryItemCallback> _onGetProductItem;
        
        public Action<ProductID, InventoryItemCallback> OnGetProductItem 
        { get => _onGetProductItem; set => _onGetProductItem = value; }
    }
}