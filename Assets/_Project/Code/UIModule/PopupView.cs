using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PopupView : MonoBehaviour
    {
        [SerializeField] private RectTransform _popupTransform;
        [SerializeField] private TMP_Text _infoText;
        [SerializeField] private ButtonView _closeButtonView;
        [SerializeField] private Image _itemImage;
        
        public RectTransform PopupTransform => _popupTransform;
        public TMP_Text InfoText => _infoText;
        public ButtonView CloseButtonView => _closeButtonView;
        public Image ItemImage => _itemImage;
    }
}