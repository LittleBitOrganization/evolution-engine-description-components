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
        [SerializeField, AllowNesting, ShowIf(nameof(IsShowYArgument)), OnValueChanged(nameof(OnValueChanged))]
        protected float _yArgument;
        [SerializeField, AllowNesting, ShowIf(nameof(IsShowZArgument)), OnValueChanged(nameof(OnValueChanged))]
        protected float _zArgument;
        
        [SerializeField, AllowNesting, ShowIf(nameof(IsShowCustomStartStep)), OnValueChanged(nameof(OnValueChanged))]
        protected bool _customStartStepCalculate = false;

        [Tooltip("Максимальное кол-во точек на графике и в списке")]
        [SerializeField] protected int _maxShowKeys = 50;

        private bool IsShowStartValue => IsNotGrowthComponent == false && _growthFunction.HasStartValue;
        private bool IsShowXArgument => IsNotGrowthComponent == false && _growthFunction.HasXArgument;
        private bool IsShowYArgument => IsNotGrowthComponent == false && _growthFunction.HasYArgument;
        private bool IsShowZArgument => IsNotGrowthComponent == false && _growthFunction.HasZArgument;
        
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
        public float YArgument => _yArgument;
        public float ZArgument => _zArgument;

        protected virtual void OnValueChanged()
        {
            _keyCurves.Clear();
            if (!_growthFunction) return;
        
            List<Keyframe> keyframes = new List<Keyframe>();

            int countKeys = _maxLevel > _maxShowKeys ? _maxShowKeys : _maxLevel;
            
            for (int i = 0; i < countKeys; i++)
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

            return _growthFunction.GetValue(_startValue, level, _xArgument, _yArgument, _zArgument);
        }

        public void SetMaxLevel(int maxLevel)
        {
            _maxLevel = maxLevel;
            OnValueChanged();
        }
    }
}