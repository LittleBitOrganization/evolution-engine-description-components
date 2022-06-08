using System;
using System.Collections.Generic;
using LittleBit.Modules.Description.Functions;
using LittleBit.Modules.Description.Utils;
using NaughtyAttributes;
using UnityEngine;

namespace LittleBit.Modules.Description
{
    [Serializable]
    public class ProgressionValue
    {
        [SerializeField, AllowNesting, OnValueChanged(nameof(OnValueChanged))]
        protected GrowthFunction _growthFunction;
        
        [SerializeField, AllowNesting, ShowIf(nameof(IsShowStartValue)), OnValueChanged(nameof(OnValueChanged))]
        protected double _startValue = 0;
        
        [SerializeField, AllowNesting, ShowIf(nameof(IsShowXArgument)), OnValueChanged(nameof(OnValueChanged))]
        protected float _xArgument;
        
        [SerializeField, AllowNesting, ShowIf(nameof(IsShowCustomStartStep)), OnValueChanged(nameof(OnValueChanged))]
        protected bool _customStartStepCalculate = false;

        private bool IsShowStartValue => IsNotGrowthComponent == false && _growthFunction.HasStartValue;
        private bool IsShowXArgument => IsNotGrowthComponent == false && _growthFunction.HasXArgument;
        
        private bool IsShowCustomStartStep => IsNotGrowthComponent == false;
        
        protected bool IsNotGrowthComponent => _growthFunction == null;
        
        [SerializeField, AllowNesting, ShowIf(nameof(_customStartStepCalculate))]
        protected int _startStep = 0;

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
            
            if (_customStartStepCalculate)
                level = Math.Max(level - _startStep, 0);
            
            return _growthFunction.GetValue(_startValue, level, _xArgument);
        }

        public void SetMaxLevel(int maxLevel)
        {
            _maxLevel = maxLevel;
            OnValueChanged();
        }
    }
}