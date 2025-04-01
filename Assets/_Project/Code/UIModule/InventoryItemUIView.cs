using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InventoryItemUIView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _valueText;
        [SerializeField] private TMP_Text _maxValueText;
    }
}