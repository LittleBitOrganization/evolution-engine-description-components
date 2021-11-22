
using System;
using System.Collections.Generic;

namespace LittleBit.Modules.Description.Components
{
    [Serializable]
    public class Attribute : BaseAttribute
    {
        private List<RawBonus> _rawBonuses;
        private List<FinalBonus> _finalBonuses;

        private float _finalValue = 0;
        
        public Attribute(float baseValue, float baseMultiplier) : base(baseValue, baseMultiplier)
        {
            _rawBonuses = new List<RawBonus>();
            _finalBonuses = new List<FinalBonus>();
        }

        public void AddRawBonus(RawBonus bonus)
        {
            _rawBonuses.Add(bonus);
        }
        
        public void RemoveRawBonus(RawBonus bonus)
        {
            if(!_rawBonuses.Contains(bonus)) return;

            _rawBonuses.Remove(bonus);
        }

        public void AddFinalBous(FinalBonus bonus)
        {
            _finalBonuses.Add(bonus);
        }

        public void RemoveFinalBonus(FinalBonus bonus)
        {
            if(!_finalBonuses.Contains(bonus)) return;

            _finalBonuses.Remove(bonus);
        }

        public float CalculateValue()
        {
            if (!_isDirty) return _finalValue;
            
            _finalValue = _baseValue;

            var rawBonusValue = 0f;
            var rawBonusMultiplier = 0f;

            foreach (var bonus in _rawBonuses)
            {
                rawBonusValue += bonus.GetBaseValue();
                rawBonusMultiplier += bonus.GetBaseMultiplier();
            }

            _finalValue += rawBonusMultiplier;
            _finalValue *= (1 + rawBonusMultiplier);
            
            var finalBonusValue = 0f;
            var finalBonusMultiplier = 0f;

            foreach (var bonus in _finalBonuses)
            {
                finalBonusValue += bonus.GetBaseValue();
                finalBonusMultiplier += bonus.GetBaseMultiplier();
            }
            
            _finalValue += finalBonusMultiplier;
            _finalValue *= (1 + finalBonusMultiplier);

            _isDirty = false;

            return _finalValue;
        }
    }
}