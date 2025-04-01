using System;
using Player;
using UnityEngine;

namespace GameCoreModule
{
    public class PlayerEventBus
    {
        private Action<PlayerInventory> _onPlayerInventoryInitialized;
        private Action<ProductID, InventoryItemCallback> _onGetProductItem;
        private Action<GameObject> _tryInteractWithObject;
        
        public Action<PlayerInventory> OnPlayerInventoryInitialized
        { get => _onPlayerInventoryInitialized; set => _onPlayerInventoryInitialized = value; }
        public Action<ProductID, InventoryItemCallback> OnGetProductItem 
        { get => _onGetProductItem; set => _onGetProductItem = value; }
        public Action<GameObject> OnTryInteractWithObject
        { get => _tryInteractWithObject; set => _tryInteractWithObject = value; }
    }
}