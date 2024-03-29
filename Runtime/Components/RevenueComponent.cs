﻿using System;
using NaughtyAttributes;
using UnityEngine;

namespace LittleBit.Modules.Description.Components
{
    [Serializable]
    public class RevenueComponent : Component
    {
        public string ResourceId => _resourceId;

        [SerializeField] private string _resourceId = "resources/gold";

        [AllowNesting] public ValueBasedOnLevel baseRevenue, baseTime;
        
        [HideInInspector] public AttributeBasedOnLevel revenue, time;
        
        public override void InitializeAttributes()
        {
            revenue = new AttributeBasedOnLevel(baseRevenue, 0);
            time = new AttributeBasedOnLevel(baseTime, 0);
        }
    }
}