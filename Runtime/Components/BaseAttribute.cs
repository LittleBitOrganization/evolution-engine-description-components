using System;

namespace LittleBit.Modules.Description.Components
{
    [Serializable]
    public class BaseAttribute
    {
        protected float _baseValue, _baseMultiplier;
        protected bool _isDirty = true;
        
        public BaseAttribute(float baseValue, float baseMultiplier)
        {
            _baseValue = baseValue;
            _baseMultiplier = baseMultiplier;
        }

        public float GetBaseValue()
        {
            return _baseValue;
        }

        public float GetBaseMultiplier()
        {
            return _baseMultiplier;
        }
        
        
        public void SetBaseValue(float value)
        {
            _baseValue = value;
            _isDirty = true;
        }

        public void SetBaseMultiplier(float value)
        {
            _baseMultiplier = value;
            _isDirty = true;
        }
    }
}