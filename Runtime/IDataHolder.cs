using System;

namespace LittleBit.Modules.Description
{
    public interface IDataHolder
    {
        public Type GetSaveDataType();
        public string GetDataKey();
    }
}