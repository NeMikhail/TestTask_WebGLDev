using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class GUIView : MonoBehaviour
    {
        [SerializeField] private RectTransform _productInventoryPanel;
        [SerializeField] private ButtonView _pauseButton;
        [SerializeField] private ButtonView _inventoryResizeButton;
        [SerializeField] private List<InventoryItemUIView> _inventoryItems;
    
        public RectTransform ProductInventoryPanel => _productInventoryPanel;
        public ButtonView PauseButton => _pauseButton;
        public ButtonView InventoryResizeButton => _inventoryResizeButton;
        public List<InventoryItemUIView> InventoryItems => _inventoryItems;
    }
}
