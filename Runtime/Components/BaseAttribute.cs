using System;

namespace LittleBit.Modules.Description.Components
{
    [Serializable]
    public class BaseAttribute
    {
        protected double _baseValue, _baseMultiplier;
        protected bool _isDirty = true;
        
        public BaseAttribute(double baseValue, double baseMultiplier)
        {
            _baseValue = baseValue;
            _baseMultiplier = baseMultiplier;
        }

        public double GetBaseValue()
        {
            return _baseValue;
        }

        public double GetBaseMultiplier()
        {
            return _baseMultiplier;
        }

        public void SetBaseValue(double value)
        {
            _baseValue = value;
            _isDirty = true;
        }

        public void SetBaseMultiplier(double value)
        {
            _baseMultiplier = value;
            _isDirty = true;
        }
    }
}