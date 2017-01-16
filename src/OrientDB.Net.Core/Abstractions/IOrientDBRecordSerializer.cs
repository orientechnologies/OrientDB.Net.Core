using OrientDB.Net.Core.Models;
using System.Collections.Generic;

namespace OrientDB.Net.Core.Abstractions
{
    public interface IOrientDBRecordSerializer<TDataType>
    {
        OrientDBRecordFormat RecordFormat { get; }

        TResultType Deserialize<TResultType>(TDataType data) where TResultType : OrientDBEntity;

        TDataType Serialize<T>(T input) where T : OrientDBEntity;
    }
}
