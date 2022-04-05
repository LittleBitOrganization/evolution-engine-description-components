using UnityEngine;

namespace LittleBit.Modules.Description.Functions
{
    public abstract class GrowthFunction : ScriptableObject
    {
        public abstract double GetValue(double startValue, int levels, float xArgument);
        public abstract bool HasStartValue { get; protected set; }
        public abstract bool HasXArgument { get; protected set; }
    }
}