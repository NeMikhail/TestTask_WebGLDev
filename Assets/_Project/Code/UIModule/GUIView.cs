using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class GUIView : MonoBehaviour
    {
        [SerializeField] private RectTransform _productInventoryPanel;
        [SerializeField] private RectTransform _productInventoryLayout;
        [SerializeField] private ButtonView _pauseButton;
        [SerializeField] private ButtonView _inventoryResizeButton;
        [SerializeField] private PopupView _popupView;

        private Dictionary<ProductID, InventoryItemUIView> _inventoryItems;
    
        public RectTransform ProductInventoryPanel => _productInventoryPanel;
        public RectTransform ProductInventoryLayout => _productInventoryLayout;
        public ButtonView PauseButton => _pauseButton;
        public ButtonView InventoryResizeButton => _inventoryResizeButton;
        public PopupView PopupView => _popupView;
        public Dictionary<ProductID, InventoryItemUIView> InventoryItems => _inventoryItems;

        public void InitializeView()
        {
            _inventoryItems = new Dictionary<ProductID, InventoryItemUIView>();
        }
    }
}
