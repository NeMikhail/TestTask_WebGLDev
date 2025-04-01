using System;
using Player;

namespace GameCoreModule
{
    public class PlayerEventBus
    {
        private Action<ProductID> _onGetProductItem;
        private Action<InventoryItem> _onSendProductInventoryItem;
        
        public Action<ProductID> OnGetProductItem { get => _onGetProductItem; set => _onGetProductItem = value; }
        public Action<InventoryItem> OnSendProductInventoryItem 
        { get => _onSendProductInventoryItem; set => _onSendProductInventoryItem = value; }
    }
}