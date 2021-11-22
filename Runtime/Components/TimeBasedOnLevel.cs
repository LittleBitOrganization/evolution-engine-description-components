using System;
using LittleBit.Modules.Description.Components;

namespace InternalAssets.Scripts.Game.Descriptions
{
    [Serializable]
    public class TimeBasedOnLevel : ValueBasedOnLevel
    {
        public override double GetValue(int level)
        {
            if (_growthFunction == null) return 0;
            if (level == 0) return -0.1f;
            return _growthFunction.GetValue(StartValue, level, XArgument);
        }
    }
}