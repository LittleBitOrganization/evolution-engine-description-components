namespace LittleBit.Modules.Description.Components
{
    public class AttributeBasedOnLevel : Attribute
    {
        private ValueBasedOnLevel _valueBasedOnLevel;

        public AttributeBasedOnLevel(ValueBasedOnLevel valueBasedOnLevel, double baseMultiplier) : base(
            valueBasedOnLevel.StartValue, baseMultiplier)
        {
            _valueBasedOnLevel = valueBasedOnLevel;
        }

        public double CalculateValue(int level)
        {
            SetBaseValue(_valueBasedOnLevel.GetValue(level));

            return CalculateValue();
        }
    }
}