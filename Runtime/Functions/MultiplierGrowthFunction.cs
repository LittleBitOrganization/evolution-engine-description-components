using NaughtyAttributes;
using UnityEngine;

namespace LittleBit.Modules.Description.Functions
{
    [CreateAssetMenu(fileName = "MultiplierGrowthComponent",
        menuName = "DataSettingsManager/GrowthComponent/Multiplier", order = 0)]
    public class MultiplierGrowthFunction : GrowthFunction
    {
        [SerializeField, InfoBox("Каждый уровень умножает предыдущий уровень на xArgument", EInfoBoxType.Error)]
        private bool _checkMe = false;

        public override double GetValue(double startValue, int levels, float xArgument = 0,float yArgument = 0,float zArgument = 0)
        {
            double multiplier = Mathf.Pow(xArgument, levels);
            double sum = startValue * multiplier;
            return sum;
        }

        public override bool HasStartValue { get; protected set; } = true;
        public override bool HasXArgument { get; protected set; } = true;
        public override bool HasYArgument { get; protected set; } = false;
        public override bool HasZArgument { get; protected set; } = false;
    }
}