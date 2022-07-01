using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LittleBit.Modules.Description.ResourceGenerator
{
    public static class ResourcesConfigsCreator  
    {
        private const string Extension = ".asset";
        
        public static void CreateConfigs(List<string> fields)
        {
            CheckPath();
            foreach (var field in fields) 
                CreateScriptable(field);
        }

        private static void CreateScriptable(string field)
        {
            field = field.Split('/')[1];
            var instance = ScriptableObject.CreateInstance<ResourceConfig>();
            var name = field + "Config";
            AssetDatabase.CreateAsset(instance, GetPath(name));
            AssetDatabase.SaveAssets();
        }

        private static void CheckPath()
        {
            var assetsResources = Constants.ConfigsFolderPath + "/ResourcesConfigs";
            if (AssetDatabase.IsValidFolder(assetsResources)) return;
            AssetDatabase.CreateFolder(Constants.ConfigsFolderPath, 
                                    "ResourcesConfigs");
        }

        private static string GetPath(string name) => 
            Constants.ConfigsFolderPath + "/ResourcesConfigs/" + name + Extension;
    }
}