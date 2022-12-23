using System.Collections.Generic;
using System.Text;
using LittleBit.Modules.Description;
using NaughtyAttributes;
using UnityEngine;

namespace LittleBit.Modules.Description.ResourceGenerator
{
    public class ResourceConfig : ScriptableObject, IResourceConfig
    {
        [SerializeField, Dropdown(nameof(ResourceIds))] private string _key;

        private List<string> ResourceIds => ResourceIDs.FieldsValue;

        [SerializeField] private string _resourceName;
        [Min(0)] public double initialValue;
        [SerializeField] private bool _isCurrency = false;
        public bool IsCurrency => _isCurrency;
        [ShowAssetPreview, SerializeField] private Sprite _sprite;
        [ShowAssetPreview, SerializeField] private Sprite _warehouseSprite;
        public int drawOrder = -1;
        
        public string GetKey()
        {
            return _key;
        }

        public string GetName()
        {
            return _resourceName;
        }

        public Sprite GetSprite()
        {
            return _sprite;
        }
        public Sprite GetWarehouseSprite()
        {
            return _warehouseSprite;
        }
        
        public static implicit operator string(ResourceConfig resourceConfig)
        {
            return resourceConfig.GetKey();
        }
        
    }
}
