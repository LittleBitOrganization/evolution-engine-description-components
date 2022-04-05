using System;
using System.Collections.Generic;
using LittleBit.Modules.Description.Functions;
using LittleBit.Modules.Description.Utils;
using NaughtyAttributes;
using UnityEngine;

namespace LittleBit.Modules.Description.Components
{
    [Serializable]
    public class ValueBasedOnLevel
    {
        [SerializeField, AllowNesting, OnValueChanged(nameof(OnValueChanged))]
        protected GrowthFunction _growthFunction;
        
        [SerializeField, AllowNesting, ShowIf(nameof(IsShowStartValue)), OnValueChanged(nameof(OnValueChanged))]
        protected double _startValue = 0;
        
        [SerializeField, AllowNesting, ShowIf(nameof(IsShowXArgument)), OnValueChanged(nameof(OnValueChanged))]
        protected float _xArgument;

        private bool IsShowStartValue => IsNotGrowthComponent == false && _growthFunction.HasStartValue;
        private bool IsShowXArgument => IsNotGrowthComponent == false && _growthFunction.HasXArgument;
        protected bool IsNotGrowthComponent => _growthFunction == null;
        

        [SerializeField, AllowNesting, HideIf(nameof(IsNotGrowthComponent))]
        protected AnimationCurve curve;

        [SerializeField, AllowNesting, HideIf(nameof(IsNotGrowthComponent))]
        protected List<KeyCurve> _keyCurves = new List<KeyCurve>();
        
        protected int _maxLevel = 0;

        public double StartValue => _startValue;

        public float XArgument => _xArgument;

        protected virtual void OnValueChanged()
        {
            _keyCurves.Clear();
            if (!_growthFunction) return;

            List<Keyframe> keyframes = new List<Keyframe>();
            
            for (int i = 1; i < _maxLevel; i++)
            {
                var keyframe = new Keyframe(i, (float) GetValue(i-1));

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

        public void SetMaxLevel(int maxLevel)
        {
            _maxLevel = maxLevel;
            OnValueChanged();
        }
    }
}