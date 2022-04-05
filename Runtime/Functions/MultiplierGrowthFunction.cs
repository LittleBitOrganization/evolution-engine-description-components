using System;
using NaughtyAttributes;
using UnityEngine;

namespace LittleBit.Modules.Description.Functions
{
    [CreateAssetMenu(fileName = "MultiplierGrowthComponent",
        menuName = "DataSettingsManager/GrowthComponent/Multiplier", order = 0)]
    public class MultiplierGrowthFunction : GrowthFunction
    {
        [SerializeField, DisableIf(nameof(Boolean.TrueString))]
        private string _description = "Каждый уровень умножает предыдущий уровень на xArgument";

        public override double GetValue(double startValue, int levels, float xArgument)
        {
            double multiplier = Mathf.Pow(xArgument, levels);
            double sum = startValue * multiplier;
            return sum;
        }

        public override bool HasStartValue { get; protected set; } = true;
        public override bool HasXArgument { get; protected set; } = true;
    }
}