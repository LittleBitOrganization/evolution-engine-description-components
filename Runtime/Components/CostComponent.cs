using System;
using UnityEngine;

namespace LittleBit.Modules.Description.Components
{
    [Serializable]
    public class CostComponent : ComponentGrowth
    {
        public string ResourceId => _resourceId;

        [SerializeField] private string _resourceId = "resources/gold";
        
        public double GetCost(int level)
        {
            if (_growthFunction == null) return 0;
            return _growthFunction.GetValue(StartValue, level, XArgument);
        }

        protected override double GetValue(int level)
        {
            return GetCost(level);
        }
    }
}