using UnityEngine;

namespace Player
{
    public class InventoryItem
    {
        private Sprite _itemSprite;
        private string _itemName;
        private int _itemValue;
        private int _itemMaxValue;
        
        public Sprite ItemSprite => _itemSprite;
        public string ItemName => _itemName;
        public int ItemValue => _itemValue;
        public int ItemMaxValue => _itemMaxValue;

        public InventoryItem(Sprite itemSprite, string itemName, int itemValue, int itemMaxValueMax)
        {
            _itemSprite = itemSprite;
            _itemName = itemName;
            _itemValue = itemValue;
            _itemMaxValue = itemMaxValueMax;
        }

        public void AddValue(int value)
        {
            if (_itemValue + value > _itemMaxValue)
            {
                _itemValue = _itemMaxValue;
            }
            else
            {
                _itemValue += value;
            }
        }

        public bool RemoveValue(int value)
        {
            if (_itemValue - value < 0)
            {
                return false;
            }
            else
            {
                _itemValue -= value;
                return true;
            }
        }
        
        public void SetValue(int value)
        {
            if (value < 0 || value > _itemMaxValue)
            {
                Debug.LogError("Invalid inventory item value");
                return;
            }
            _itemValue = value;
        }
    }
}