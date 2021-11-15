using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NaughtyAttributes;
using UnityEngine;

namespace LittleBit.Modules.Description.Utils //TODO: сменить namespace
{
    [Serializable]
    public class Requirement
    {
        [SerializeField] private DataHolderInterfaceContainer _dataHolder = null;

        [ShowIf(nameof(HasDataHolder))]
        [Dropdown(nameof(FieldName)), SerializeField, AllowNesting, OnValueChanged(nameof(OnSelectedField))]
        private string _field;

        [SerializeField] private Operator _operator;
        [SerializeField] private string _value = String.Empty;

        private Type _type;
        private bool HasDataHolder => DataHolder != null;
        private List<string> FieldName => _fieldName;
        private List<string> _fieldName = new List<string>();
        private List<FieldInfo> _fieldInfoList = new List<FieldInfo>();

        public string GetValue() => _value;

        public IDataHolder DataHolder
        {
            get { return _dataHolder.Result; }
            set
            {
                _dataHolder.Result = value;
                OnValueChanged();
            }
        }

        private void OnSelectedField()
        {
            Debug.Log("OnSelectedFieldChanged " + _fieldInfoList.Count);
            _type = GetTypeByField(_field, _fieldInfoList);
        }

        public bool IsFieldNameEquals(string fieldName)
        {
            return string.Equals(_field, fieldName, StringComparison.Ordinal);
        }

        private enum Operator
        {
            Equals = 0,
            NotEquals = 1,
            EqualsOrGreater = 2,
            EqualsOrLess = 3,
            Greater = 4,
            Less = 5
        }

        public bool Check(Type type, object value)
        {
            if (!type.Equals(type)) throw new Exception();
            
            return Compare(type, GetValue(), value);
        }

        private bool Compare(Type comparsionType, object firstValue, object secondValue)
        {
            var firstValueComparable = (Convert.ChangeType(firstValue, comparsionType) as IComparable);
            var secondValueComparable = (Convert.ChangeType(secondValue, comparsionType) as IComparable);

            if (firstValueComparable == null || secondValueComparable == null)
            {
                Debug.Log("Can't compare " + firstValue + " and " + secondValue);
                return false;
            }
            
            switch (_operator)
            {
                case Operator.Equals: return firstValue.Equals(secondValue);
                case Operator.NotEquals: return !(firstValue.Equals(secondValue));
                case Operator.EqualsOrGreater: return secondValueComparable.CompareTo(firstValueComparable) >= 0;
                case Operator.EqualsOrLess: return secondValueComparable.CompareTo(firstValueComparable) <= 0;
                case Operator.Greater: return secondValueComparable.CompareTo(firstValueComparable) > 0;
                case Operator.Less: return secondValueComparable.CompareTo(firstValueComparable) < 0;
            }

            return false;
        }


        private void OnValueChanged()
        {
            Debug.Log("OnValueChanged");

            _fieldInfoList = new List<FieldInfo>();
            _fieldName = new List<string>();

            if (!HasDataHolder)
            {
                _fieldName.Clear();
                _field = string.Empty;

                return;
            }

            var dataType = DataHolder.GetSaveDataType();
            _fieldInfoList = dataType.GetFields().ToList();

            foreach (var fieldInfo in _fieldInfoList)
            {
                _fieldName.Add(fieldInfo.Name);
            }

            _type = GetTypeByField(_field, _fieldInfoList);
        }

        private Type GetTypeByField(string fieldName, List<FieldInfo> fieldInfos)
        {
            foreach (var fieldInfo in fieldInfos)
            {
                if (fieldInfo.Name == fieldName)
                {
                    return fieldInfo.FieldType;
                }
            }

            throw new Exception();
        }

        public void OnValidate()
        {
            OnValueChanged();
            OnSelectedField();
        }
    }
}