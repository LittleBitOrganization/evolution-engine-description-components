using System;
using System.Collections.Generic;
using System.Reflection;
using LittleBit.Modules.CoreModule;
using LittleBit.Modules.Description.Utils;
using UnityEngine;

namespace LittleBit.Modules.Description.Components
{
    [Serializable]
    public class RequirementComponent : Component
    {
        [SerializeField] private List<Requirement> requirements;

        public IReadOnlyList<Requirement> Requirements => requirements;

        public void OnValidate()
        {
            foreach (var requirement in requirements)
            {
                requirement.OnValidate();
            }
        }

        public bool Check<T>(IDataStorageService dataStorageService) where T : Data, new()
        {
            int numRequirement = Requirements.Count;
            int countRequirement = 0;

            foreach (var requirement in requirements)
            {
                FieldInfo[] fieldInfos = typeof(T).GetFields();

                foreach (FieldInfo fieldInfo in fieldInfos)
                {
                    if (HasField(requirement, fieldInfo))
                    {
                        if (IsRequirementValid<T>(requirement, fieldInfo, dataStorageService))
                        {
                            countRequirement++;
                            break;
                        }
                    }
                }
            }

            return numRequirement == countRequirement;
        }

        private bool IsRequirementValid<T>(Requirement requirement, FieldInfo fieldInfo,
            IDataStorageService dataStorageService) where T : Data, new()
        {
            string keyRequirementData = requirement.DataHolder.GetDataKey();
            T dataRequirementData = dataStorageService.GetData<T>(keyRequirementData);

            var typeFieldGroupSaveData = fieldInfo.FieldType;
            return requirement.Check(typeFieldGroupSaveData, fieldInfo.GetValue(dataRequirementData));
        }

        private bool HasField(Requirement requirement, FieldInfo fieldInfo)
        {
            string fieldName = fieldInfo.Name;
            return requirement.IsFieldNameEquals(fieldName);
        }
    }
}