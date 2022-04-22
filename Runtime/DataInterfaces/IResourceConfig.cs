using UnityEngine;

namespace LittleBit.Modules.Description
{
    public interface IResourceConfig : IKeyHolder
    {
        public string GetName();
        public Sprite GetSprite();
    }
}