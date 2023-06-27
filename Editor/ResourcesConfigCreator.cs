using System.Text;
using UnityEditor;
using UnityEngine;

namespace LittleBit.Modules.Description.ResourceGenerator
{
    public static class ResourcesConfigCreator
    {
        private const string AssetName = "ResourcesConfig";
        private const string Extension = ".asset";
        
        private static string FullPath => Constants.ResourcesConfigPath + "/" + AssetName + Extension; 
        
        // [MenuItem("Tools/Configs/ResourcesConfig")]
        // public static void CreateResourceIdsConfig()
        // {
        //     var instance = ScriptableObject.CreateInstance<ResourceConfigIDs>();
        //     
        //     CheckOrCreateDirectory();
        //
        //     AssetDatabase.CreateAsset(instance, FullPath);
        //     AssetDatabase.SaveAssets();
        // }

        private static void CheckOrCreateDirectory()
        {
            if (AssetDatabase.IsValidFolder(Constants.ResourcesPath) == false)
            {
                AssetDatabase.CreateFolder("Assets", "Resources");
            }
            if (AssetDatabase.IsValidFolder(Constants.ConfigsFolderPath) == false)
            {
                AssetDatabase.CreateFolder(Constants.ResourcesPath, Constants.ConfigName);
            }

            if (AssetDatabase.IsValidFolder(Constants.ResourcesConfigPath) == false)
            {
                AssetDatabase.CreateFolder(Constants.ConfigsFolderPath, Constants.ResourcesConfigName);
            }
        }
    }
}