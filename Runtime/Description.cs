using System;
using UnityEngine;

namespace LittleBit.Modules.Description
{
    public abstract class Description : ScriptableObject
    {
        protected bool IsShowComponent<T>(T allSelectedComponents, T currentComponent) where T : Enum
        {
            int allSelectedValue = Convert.ToInt32(allSelectedComponents);
            int currentValue = Convert.ToInt32(currentComponent);
            return (allSelectedValue & currentValue) != 0;
        }
    }
}