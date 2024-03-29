﻿using NaughtyAttributes;
using UnityEngine;

namespace LittleBit.Modules.Description.Functions
{
    [CreateAssetMenu(fileName = "AddingGrowthFunction", 
        menuName = "DataSettingsManager/GrowthComponent/Adding", order = 0)]
    public class AddingGrowthFunction : GrowthFunction
    {
        [SerializeField, InfoBox("Каждый уровень прибавляет к предыдущему уровню xArgument", EInfoBoxType.Error)]
        private bool _checkMe = false;

        public override double GetValue(double startValue, int levels, float xArgument = 0,float yArgument = 0,float zArgument = 0)
        {
            double sum = startValue;
            for (int i = 0; i < levels; i++)
            {
                sum += xArgument;
            }
            
            return sum;
        }

        public override bool HasStartValue { get; protected set; } = true;
        public override bool HasXArgument { get; protected set; } = true;
        public override bool HasYArgument { get; protected set; } = false;
        public override bool HasZArgument { get; protected set; } = false;
    }
}