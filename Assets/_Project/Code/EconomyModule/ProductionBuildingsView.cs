using System.Collections.Generic;
using UnityEngine;

namespace Economy
{
    public class ProductionBuildingsView : MonoBehaviour
    {
        [SerializeField] private List<ProductionBuildingView> _productionBuildingViews;
        
        public List<ProductionBuildingView> ProductionBuildingViews => _productionBuildingViews;
    }
}