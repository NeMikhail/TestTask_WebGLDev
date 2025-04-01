using System.Collections.Generic;
using GameCoreModule;
using MAEngine;
using Player;
using UnityEngine;
using Zenject;

namespace UI
{
    public class GUIActions : IAction, IPreInitialisation, IInitialisation, ICleanUp, IFixedExecute
    {
        private const float SHOWED_INVENTORY_PANEL_Y_POS = -100f;
        private const float HIDDEN_INVENTORY_PANEL_Y_POS = 50f;
        
        private GUIView _guiView;
        private StateEventsBus _stateEventsBus;
        private PlayerEventBus _playerEventBus;
        private GameEventBus _gameEventBus;

        private bool _isInventoryShowed;
        private PlayerInventory _inventory;

        [Inject]
        public void Construct(GUIView guiView, StateEventsBus stateEventsBus, PlayerEventBus playerEventBus,
            GameEventBus gameEventBus)
        {
            _guiView = guiView;
            _stateEventsBus = stateEventsBus;
            _playerEventBus = playerEventBus;
            _gameEventBus = gameEventBus;
        }
        
        public void PreInitialisation()
        {
            _guiView.InitializeView();
            _playerEventBus.OnPlayerInventoryInitialized += ShowPlayerInventory;
        }

        public void Initialisation()
        {
            _guiView.PauseButton.Button.onClick.AddListener(SetPauseState);
            _guiView.InventoryResizeButton.Button.onClick.AddListener(ResizeInventory);
            _isInventoryShowed = false;
        }

        public void Cleanup()
        {
            _playerEventBus.OnPlayerInventoryInitialized -= ShowPlayerInventory;
            _guiView.PauseButton.Button.onClick.RemoveListener(SetPauseState);
            _guiView.InventoryResizeButton.Button.onClick.RemoveListener(ResizeInventory);
        }
        
        public void FixedExecute(float fixedDeltaTime)
        {
            UpdateUI();
        }
        
        private void ShowPlayerInventory(PlayerInventory inventory)
        {
            _inventory = inventory;
            for (int i = 0; i < _inventory.ProductionInventory.Length; i++)
            {
                GameObjectSpawnCallback callback = new GameObjectSpawnCallback();
                _gameEventBus.OnSpawnObject?.Invoke(PrefabID.InventoryItemPrefab, Vector3.zero,
                    _guiView.ProductInventoryLayout, callback);
                InventoryItemUIView itemView = callback.SpawnedObject.GetComponent<InventoryItemUIView>();
                ProductID id = _inventory.ProductionInventory.GetKeyByIndex(i);
                InventoryItem inventoryItem = _inventory.ProductionInventory.GetValueByIndex(i);
                itemView.Icon.sprite = inventoryItem.ItemSprite;
                itemView.ValueText.text = inventoryItem.ItemValue.ToString();
                itemView.MaxValueText.text = inventoryItem.ItemMaxValue.ToString();
                _guiView.InventoryItems.Add(id, itemView);
            }
        }

        public void UpdateUI()
        {
            if (_inventory != null)
            {
                for (int i = 0; i < _inventory.ProductionInventory.Length; i++)
                {
                    ProductID id = _inventory.ProductionInventory.GetKeyByIndex(i);
                    InventoryItemUIView itemView = _guiView.InventoryItems[id];
                    InventoryItem inventoryItem = _inventory.ProductionInventory.GetValueByIndex(i);
                    itemView.Icon.sprite = inventoryItem.ItemSprite;
                    itemView.ValueText.text = inventoryItem.ItemValue.ToString();
                    itemView.MaxValueText.text = inventoryItem.ItemMaxValue.ToString();
                }
            }
        }
        
        
        private void SetPauseState()
        {
            _stateEventsBus.OnPauseStateActivate?.Invoke();
        }
        
        private void ResizeInventory()
        {
            RectTransform resizeButtonImageRect =
                _guiView.InventoryResizeButton.ButtonImage.gameObject.GetComponent<RectTransform>();
            resizeButtonImageRect.rotation = Quaternion.Inverse(resizeButtonImageRect.rotation);
            RectTransform inventoryPanelRect = _guiView.ProductInventoryPanel;
            if (!_isInventoryShowed)
            {
                inventoryPanelRect.anchoredPosition =
                    new Vector2(inventoryPanelRect.anchoredPosition.x, SHOWED_INVENTORY_PANEL_Y_POS);
                _isInventoryShowed = true;
            }
            else
            {
                inventoryPanelRect.anchoredPosition =
                    new Vector2(inventoryPanelRect.anchoredPosition.x, HIDDEN_INVENTORY_PANEL_Y_POS);
                _isInventoryShowed = false;
            }
            
        }
    }
}