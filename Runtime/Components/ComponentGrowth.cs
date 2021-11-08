using System;
using System.Collections.Generic;
using LittleBit.Modules.Description.Functions;
using LittleBit.Modules.Description.Utils;
using NaughtyAttributes;
using UnityEngine;

namespace LittleBit.Modules.Description.Components
{
    [Serializable]
    public abstract class ComponentGrowth : Component
    {
        [SerializeField, AllowNesting, OnValueChanged(nameof(OnValueChanged))] 
        private double _startValue = 0;
        [SerializeField, AllowNesting, OnValueChanged(nameof(OnValueChanged))]
        protected GrowthFunction _growthFunction;
        protected bool IsNotGrowthComponent => _growthFunction == null;
        
        [SerializeField, AllowNesting, HideIf(nameof(IsNotGrowthComponent)), OnValueChanged(nameof(OnValueChanged))]
        private float _xArgument;
        
        [SerializeField, AllowNesting, HideIf(nameof(IsNotGrowthComponent))]
        private AnimationCurve curve;
        
        [SerializeField, AllowNesting, HideIf(nameof(IsNotGrowthComponent))]
        private List<KeyCurve> _keyCurves = new List<KeyCurve>();

        private int _maxLevel = 0;

        public double StartValue => _startValue;

        public float XArgument => _xArgument;
        protected void OnValueChanged()
        {
            _keyCurves.Clear();
            if (!_growthFunction) return;

            List<Keyframe> keyframes = new List<Keyframe>();
            for (int i = 0; i < _maxLevel; i++)
            {
                var keyframe = new Keyframe(i, (float) GetValue(i) );

                _keyCurves.Add(new KeyCurve(keyframe));
                keyframes.Add(keyframe);
            }

            curve.keys = keyframes.ToArray();
        }

        protected abstract double GetValue(int level);
        
        public void SetMaxLevel(int maxLevel)
        {
            _maxLevel = maxLevel;
            OnValueChanged();
        }
    }
}