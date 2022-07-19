using UnityEngine;

namespace LittleBit.Modules.Description.Functions
{
    public abstract class GrowthFunction : ScriptableObject
    {
        public abstract double GetValue(double startValue, int levels, float xArgument = 0,float yArgument = 0,float zArgument = 0);
        public abstract bool HasStartValue { get; protected set; }
        public abstract bool HasXArgument { get; protected set; }
        public abstract bool HasYArgument { get; protected set; }
        public abstract bool HasZArgument { get; protected set; }
    }
}