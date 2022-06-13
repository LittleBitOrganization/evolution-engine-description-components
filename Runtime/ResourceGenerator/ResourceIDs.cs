using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace LittleBit.Modules.Description.ResourceGenerator
{
    internal partial class ResourceIDs
    {  
        public static List<string> FieldsValue =>
            GetTypes().GetFields()
                   .Select(fieldInfo => (string) fieldInfo.GetValue(null)).ToList();


        private static Type GetTypes()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                if (assembly == typeof(ResourceIDs).Assembly) continue;
                if (assembly.FullName.StartsWith("Assembly") == false) continue;
                var type = assembly.GetTypes()
                                   .FirstOrDefault(type => type.Name == nameof(ResourceIDs));
                
                if (type != null) return type;
            }

            throw new Exception("Cannot find class ResourceIDs in configs");
        }
    }
}