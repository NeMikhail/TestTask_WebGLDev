using System;
using UnityEngine;

namespace Economy
{
    [Serializable]
    public class ProductionSettings
    {
        [SerializeField] private float _productionTime;
        [SerializeField] private int _productAdditionValue;
        [SerializeField] private int _productMaxValue;
        
        public float ProductionTime => _productionTime;
        public int ProductAdditionValue => _productAdditionValue;
        public int ProductMaxValue => _productMaxValue;
    }
}