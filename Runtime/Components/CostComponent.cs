using System;
using NaughtyAttributes;
using UnityEngine;

namespace LittleBit.Modules.Description.Components
{
    [Serializable]
    public class CostComponent : Component
    {
        public string ResourceId => _resource.Result.GetKey();

        public IResourceConfig ResourceConfig => _resource.Result;

        [Obsolete(nameof(_resourceId) + " is deprecated, please use " + nameof(_resource) + " instead.")]
        [SerializeField, AllowNesting, DisableIf(nameof(Boolean.TrueString))] private string _resourceId = "resources/gold";
        [SerializeField] private ResourceConfigInterfaceContainer _resource;

        [AllowNesting] public CostBasedOnLevel baseCost;

        [HideInInspector] public AttributeBasedOnLevel cost;

        public override void InitializeAttributes()
        {
            cost = new AttributeBasedOnLevel(baseCost, 0);
        }
    }
}