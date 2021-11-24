
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LittleBit.Modules.Description.Components
{
    [Serializable]
    public class Attribute : BaseAttribute
    {
        private List<RawBonus> _rawBonuses;
        private List<FinalBonus> _finalBonuses;

        private double _finalValue = 0;
        
        public Attribute(double baseValue, double baseMultiplier) : base(baseValue, baseMultiplier)
        {
            _rawBonuses = new List<RawBonus>();
            _finalBonuses = new List<FinalBonus>();
        }

        public void AddRawBonus(RawBonus bonus)
        {
            _isDirty = true;
            
            _rawBonuses.Add(bonus);
        }
        
        public void RemoveRawBonus(RawBonus bonus)
        {
            if(!_rawBonuses.Contains(bonus)) return;

            _isDirty = true;

            _rawBonuses.Remove(bonus);
        }

        public void AddFinalBous(FinalBonus bonus)
        {
            _isDirty = true;
            
            _finalBonuses.Add(bonus);
        }

        public void RemoveFinalBonus(FinalBonus bonus)
        {
            if(!_finalBonuses.Contains(bonus)) return;

            _isDirty = true;
            
            _finalBonuses.Remove(bonus);
        }

        public double CalculateValue()
        {
            if (!_isDirty) return _finalValue;
            
            _finalValue = _baseValue;

            var rawBonusValue = 0d;
            var rawBonusMultiplier = 0d;

            foreach (var bonus in _rawBonuses)
            {
                rawBonusValue += bonus.GetBaseValue();
                rawBonusMultiplier += bonus.GetBaseMultiplier();
            }

            _finalValue += rawBonusValue;
            _finalValue *= (1d + rawBonusMultiplier);
            
            var finalBonusValue = 0d;
            var finalBonusMultiplier = 0d;

            foreach (var bonus in _finalBonuses)
            {
                finalBonusValue += bonus.GetBaseValue();
                finalBonusMultiplier += bonus.GetBaseMultiplier();
            }
            
            _finalValue += finalBonusValue;
            _finalValue *= (1d + finalBonusMultiplier);

            _isDirty = false;
            
            return _finalValue;
        }
    }
    
    public interface IAttributesHandler
    {
        public void Initialize();
    }
}