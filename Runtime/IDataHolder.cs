using System;

namespace LittleBit.Modules.Description
{
    public interface IDataHolder : IKeyHolder
    {
        public Type GetSaveDataType();
    }
}