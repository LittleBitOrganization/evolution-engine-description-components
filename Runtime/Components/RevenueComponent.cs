using System;
using UnityEngine;

namespace LittleBit.Modules.Description.Components
{
    [Serializable]
    public class RevenueComponent : ComponentGrowth
    {
        public string ResourceId => _resourceId;

        [SerializeField] private string _resourceId = "resources/gold";
        [SerializeField] private float _timeRevenue = 1;
        public float TimeRevenue => _timeRevenue;
        public double GetRevenue(int level)
        {
            if (_growthFunction == null) return 0;
            if (level <= 0) return 0;
            return _growthFunction.GetValue(StartValue, level - 1, XArgument);
        }

        protected override double GetValue(int level)
        {
            return GetRevenue(level);
        }

        
    }
}