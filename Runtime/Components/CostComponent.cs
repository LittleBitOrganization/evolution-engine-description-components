using System;
using UnityEngine;

namespace LittleBit.Modules.Description.Components
{
    [Serializable]
    public class CostComponent : Component
    {
        public string ResourceId => _resourceId;

        [SerializeField] private string _resourceId = "resources/gold";
        
        [SerializeField] private ValueBasedOnLevel baseCost;
        
        [HideInInspector] public AttributeBasedOnLevel cost;

        public override void InitializeAttributes()
        {
            cost = new AttributeBasedOnLevel(baseCost, 0);
        }
    }
}