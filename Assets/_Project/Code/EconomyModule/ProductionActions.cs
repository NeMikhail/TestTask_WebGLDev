using System;
using System.Collections.Generic;
using MAEngine;
using UnityEngine;
using Zenject;

namespace Economy
{
    public class ProductionActions : IAction, IInitialisation, ICleanUp, IFixedExecute
    {
        private ProductionBuildingsView _productionBuildingsView;
        private ProductsPool _productsPool; 
        private ProductionBuildingsPool _productionBuildingsPool;

        private List<ActiveProduction> _activeProductions;
        
        

        [Inject]
        public void Construct( ProductionBuildingsView productionBuildingsView,
            ProductsPool productsPool, ProductionBuildingsPool productionBuildings)
        {
            _productionBuildingsView = productionBuildingsView;
            _productsPool = productsPool;
            _productionBuildingsPool = productionBuildings;
        }
        
        public void Initialisation()
        {
            _activeProductions = new List<ActiveProduction>();
            SetupActiveProductions();
            SetupProductionUI();
        }

        private void SetupProductionUI()
        {
            foreach (ActiveProduction production in _activeProductions)
            {
                ProductConfig productConfig = _productsPool.ProductsDict[production.Config.ProductID];
                production.ProductionBuildingView.ProductionImage.sprite = productConfig.ProductSprite;
                production.ProductionBuildingView.ProductionNameText.text = productConfig.ProductName;
                production.ProductionBuildingView.ProductionAddingValueText.text =
                    $"+{production.ProductionAddingValue}";
                production.ProductionBuildingView.ProductionAddingPanel.SetActive(false);
                production.ProductionBuildingView.ProgressSlider.maxValue = production.ProductionTimer.Duration;
                
                production.ProductionBuildingView.ProductionValueText.text = production.ProductionValue.ToString();
                production.ProductionBuildingView.ProgressSlider.value = 
                    production.ProductionTimer.Duration - production.ProductionTimer.GetRemainingTime();
                
            }
        }

        private void SetupActiveProductions()
        {
            foreach (ProductionBuildingView view in _productionBuildingsView.ProductionBuildingViews)
            {
                ProductionBuildingConfig config = 
                    _productionBuildingsPool.ProductionBuildings[view.ProductionBuildingID];
                ActiveProduction production = new ActiveProduction(view, config);
                _activeProductions.Add(production);
            }
        }

        public void Cleanup()
        {
            
        }

        public void FixedExecute(float fixedDeltaTime)
        {
            foreach (ActiveProduction production in _activeProductions)
            {
                if (production.ProductionTimer.Wait())
                {
                    production.AddProduction();
                }
                UpdeateUI(production);
            }
        }

        private void UpdeateUI(ActiveProduction production)
        {
            production.ProductionBuildingView.ProductionValueText.text = production.ProductionValue.ToString();
            production.ProductionBuildingView.ProgressSlider.value = 
                production.ProductionTimer.Duration - production.ProductionTimer.GetRemainingTime();
            if (production.ProductionAddingIndicationTimer != null)
            {
                if (production.ProductionAddingIndicationTimer.Wait())
                {
                    production.ProductionBuildingView.ProductionAddingPanel.SetActive(false);
                    production.ClearProductionAddingTimer();
                }
            }
        }
    }
}