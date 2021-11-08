using System;
using UnityEngine;

namespace LittleBit.Modules.Description.Utils
{
    [Serializable]
    public class KeyCurve
    {
        [HideInInspector] public string name;

        public KeyCurve(Keyframe keyframe)
        {
            name = keyframe.time.ToString() + "\t" + keyframe.value;
        }
    }
}