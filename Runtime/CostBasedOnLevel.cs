using System;
using System.Collections.Generic;
using LittleBit.Modules.Description.Components;
using LittleBit.Modules.Description.Utils;
using UnityEngine;

namespace InternalAssets.Scripts.Game.Descriptions
{
    [Serializable]
    public class CostBasedOnLevel : ValueBasedOnLevel
    {
        protected override void OnValueChanged()
        {
            _keyCurves.Clear();
            if (!_growthFunction) return;

            List<Keyframe> keyframes = new List<Keyframe>();
            for (int i = 0; i < _maxLevel; i++)
            {
                var keyframe = new Keyframe(i, (float) GetValue(i));

                _keyCurves.Add(new KeyCurve(keyframe));
                keyframes.Add(keyframe);
            }

            curve.keys = keyframes.ToArray();
        }

        public virtual double GetValue(int level)
        {
            if (_growthFunction == null) return 0;
            
            return _growthFunction.GetValue(_startValue, level, _xArgument);
        }
    }
}