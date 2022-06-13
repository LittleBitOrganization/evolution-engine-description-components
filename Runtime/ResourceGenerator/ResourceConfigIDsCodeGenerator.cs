using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

namespace LittleBit.Modules.Description.ResourceGenerator
{
    public partial class ResourceConfigIDs
    {
#if UNITY_EDITOR
        [Button()]
        private void Generate()
        {
            var path = new StringBuilder();
            path.Append(Application.dataPath)
                .Append("/Resources/")
                .Append(Constants.ConfigName)
                .Append("/")
                .Append(Constants.ResourcesConfigName)
                .Append("/ResourceIDs.cs");

            var code = "namespace LittleBit.Modules.Description\n{\n";
            code += "\tpublic partial class ResourceIDs\n\t{\n";
            var fields = new List<string>();
            foreach (var id in ids)
            {
                fields.Add(id);
                var fieldName = id.Split('/')[1];
                fieldName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fieldName.ToLower());
                Debug.LogError(fieldName);
                code += "\t\t" + $"public const string {fieldName} = \"{id}\";" + "\n";
                
            }
            
            code += "\n\t}";
            code += "\n}";

            if (File.Exists(path.ToString()) == false)
            {
                var fileStream = new FileStream(path.ToString(), FileMode.Create);
                fileStream.Dispose();
            }

            File.WriteAllText(path.ToString(), code);
            ResourcesConfigsCreator.CreateConfigs(fields);

            AssetDatabase.Refresh();
        }
        #endif
    }
}