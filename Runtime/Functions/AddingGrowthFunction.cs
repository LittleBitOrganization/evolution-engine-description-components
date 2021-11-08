using System;
using NaughtyAttributes;
using UnityEngine;

namespace LittleBit.Modules.Description.Functions
{
    [CreateAssetMenu(fileName = "AddingGrowthFunction", menuName = "DataSettingsManager/GrowthComponent/Adding", order = 0)]
    public class AddingGrowthFunction : GrowthFunction
    {
        [SerializeField, DisableIf(nameof(Boolean.TrueString))] 
        private string _description = "Каждый уровень прибавляет к предыдущему xArgument";
        
        public override double GetValue(double startValue, int levels, float xArgument)
        {
            double sum = startValue;
            for (int i = 0; i < levels; i++)
            {
                sum += xArgument;
            }
            
            return sum;
        }
    }
}