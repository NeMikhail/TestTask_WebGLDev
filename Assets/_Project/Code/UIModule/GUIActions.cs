using GameCoreModule;
using MAEngine;
using UnityEngine;
using Zenject;

namespace UI
{
    public class GUIActions : IAction, IInitialisation, ICleanUp
    {
        private const float SHOWED_INVENTORY_PANEL_Y_POS = -100f;
        private const float HIDDEN_INVENTORY_PANEL_Y_POS = 50f;
        
        private GUIView _guiView;
        private StateEventsBus _stateEventsBus;

        private bool _isInventoryShowed;

        [Inject]
        public void Construct(GUIView guiView, StateEventsBus stateEventsBus)
        {
            _guiView = guiView;
            _stateEventsBus = stateEventsBus;
        }
        
        public void Initialisation()
        {
            _guiView.PauseButton.Button.onClick.AddListener(SetPauseState);
            _guiView.InventoryResizeButton.Button.onClick.AddListener(ResizeInventory);
            _isInventoryShowed = false;
        }

        public void Cleanup()
        {
            _guiView.PauseButton.Button.onClick.RemoveListener(SetPauseState);
            _guiView.InventoryResizeButton.Button.onClick.RemoveListener(ResizeInventory);
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