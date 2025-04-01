using MAEngine.Extention;
using UnityEngine;

namespace Economy
{
    public class ActiveProduction
    {
        private ProductionBuildingView _productionBuildingView;
        private ProductionBuildingConfig _config;
        private Timer _productionTimer;
        private int _productionAddingValue;
        private int _productionMaxValue;
        private int _productionValue;
        private Timer _productionAddingIndicationTimer;

        public ProductionBuildingView ProductionBuildingView => _productionBuildingView;
        public ProductionBuildingConfig Config => _config;
        public Timer ProductionTimer => _productionTimer;
        public int ProductionAddingValue => _productionAddingValue;
        public int ProductionValue => _productionValue;
        public Timer ProductionAddingIndicationTimer => _productionAddingIndicationTimer;

        public ActiveProduction()
        {

        }
        public ActiveProduction(ProductionBuildingView view, ProductionBuildingConfig config, int savedValue = 0)
        {
            _productionBuildingView = view;
            _config = config;
            ProductionSettings settings = config.ProductionTiersSettings[view.ProductionTier];
            _productionTimer = new Timer(settings.ProductionTime);
            _productionAddingValue = settings.ProductAdditionValue;
            _productionMaxValue = settings.ProductMaxValue;
            _productionValue = savedValue;
        }

        public void AddProduction()
        {
            _productionValue += _productionAddingValue;
            _productionValue = Mathf.Clamp(_productionValue, 0, _productionMaxValue);
            float addingIndicationTime = 1f;
            if (_productionTimer.Duration < 4f)
            {
                addingIndicationTime = _productionTimer.Duration / 2f;
            }
            else
            {
                addingIndicationTime = 2f;
            }
            _productionAddingIndicationTimer = new Timer(addingIndicationTime);
            UpdateUI();
        }
        
        public void SetProduction(int value)
        {
            _productionValue = value;
            UpdateUI();
        }
        

        private void UpdateUI()
        {
            _productionBuildingView.ProductionValueText.text = _productionValue.ToString();
            if (_productionAddingIndicationTimer != null)
            {
                _productionBuildingView.ProductionAddingPanel.SetActive(true);
            }
        }

        public void ClearProductionAddingTimer()
        {
            _productionAddingIndicationTimer = null;
        }
    }
}