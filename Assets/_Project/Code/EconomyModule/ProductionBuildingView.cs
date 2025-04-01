using System;
using MAEngine;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Economy
{
    public class ProductionBuildingView : MonoBehaviour
    {
        [SerializeField] private ProductionBuildingID _productionBuildingID;
        [SerializeField] private Tiers _productionTier;
        [SerializeField] private Canvas _productionCanvas;
        [SerializeField] private TMP_Text _productionNameText;
        [SerializeField] private TMP_Text _productionValueText;
        [SerializeField] private Image _productionImage;
        [SerializeField] private Slider _progressSlider;
        [SerializeField] private GameObject _productionAddingPanel;
        [SerializeField] private TMP_Text _productionAddingValueText;
        [SerializeField] private InteractableObjectView _interactableView;

        public ProductionBuildingID ProductionBuildingID => _productionBuildingID;
        public Tiers ProductionTier => _productionTier;
        public Canvas ProductionCanvas => _productionCanvas;
        public TMP_Text ProductionNameText => _productionNameText;
        public TMP_Text ProductionValueText => _productionValueText;
        public Image ProductionImage => _productionImage;
        public Slider ProgressSlider => _progressSlider;
        public GameObject ProductionAddingPanel => _productionAddingPanel;
        public TMP_Text ProductionAddingValueText => _productionAddingValueText;
        public InteractableObjectView InteractableView => _interactableView;
    }
}

