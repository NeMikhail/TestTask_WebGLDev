using System;
using System.Collections.Generic;
using GameCoreModule;
using MAEngine;
using Player;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Economy
{
    public class ProductionActions : IAction, IInitialisation, ICleanUp, IFixedExecute
    {
        private ProductionBuildingsView _productionBuildingsView;
        private ProductsPool _productsPool; 
        private ProductionBuildingsPool _productionBuildingsPool;
        private PlayerEventBus _playerEventBus;
        private UIEventBus _uiEventBus;

        private List<ActiveProduction> _activeProductions;
        
        [Inject]
        public void Construct( ProductionBuildingsView productionBuildingsView,
            ProductsPool productsPool, ProductionBuildingsPool productionBuildings,
            PlayerEventBus playerEventBus, UIEventBus uiEventBus)
        {
            _productionBuildingsView = productionBuildingsView;
            _productsPool = productsPool;
            _productionBuildingsPool = productionBuildings;
            _playerEventBus = playerEventBus;
            _uiEventBus = uiEventBus;
        }
        
        public void Initialisation()
        {
            _activeProductions = new List<ActiveProduction>();
            _playerEventBus.OnTryInteractWithObject += TryInteractWithProductionBuilding;
            SetupActiveProductions();
            SetupProductionUI();
        }
        
        public void Cleanup()
        {
            _playerEventBus.OnTryInteractWithObject -= TryInteractWithProductionBuilding;
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

        private void TryInteractWithProductionBuilding(GameObject buildingObj)
        {
            ProductionBuildingView view;
            if (buildingObj.transform.parent.TryGetComponent<ProductionBuildingView>(out view))
            {
                if (view.InteractableView.IsInteractable)
                {
                    ActiveProduction interactableProduction = new ActiveProduction();
                    foreach (ActiveProduction production in _activeProductions)
                    {
                        if (production.ProductionBuildingView == view)
                        {
                            interactableProduction = production;
                            break;
                        }
                        else
                        {
                            interactableProduction = null;
                        }
                    }

                    if (interactableProduction == null)
                    {
                        Debug.LogWarning("Interactable building not produced");
                        return;
                    }
                    InventoryItemCallback callback = new InventoryItemCallback();
                    _playerEventBus.OnGetProductItem?.Invoke(interactableProduction.Config.ProductID, callback);
                    InventoryItem item = callback.InventoryItem;
                    int addedValue = interactableProduction.ProductionValue;
                    int remainder = item.AddValue(addedValue);
                    interactableProduction.SetProduction(remainder);
                    addedValue -= remainder;
                    if (addedValue > 0)
                    {
                        _uiEventBus.OnShowCollectPopup?.Invoke(item.ItemName, item.ItemSprite, addedValue);
                    }
                }
            }
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