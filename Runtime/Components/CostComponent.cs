using System;
using NaughtyAttributes;
using UnityEngine;

namespace LittleBit.Modules.Description.Components
{
    [Serializable]
    public class CostComponent : Component
    {
        public string ResourceId => _resourceId;

        [SerializeField] private string _resourceId = "resources/gold";

        [AllowNesting] public CostBasedOnLevel baseCost;

        [HideInInspector] public AttributeBasedOnLevel cost;

        public override void InitializeAttributes()
        {
            cost = new AttributeBasedOnLevel(baseCost, 0);
        }
    }
}