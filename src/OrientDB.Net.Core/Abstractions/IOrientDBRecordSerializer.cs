using OrientDB.Net.Core.Models;

namespace OrientDB.Net.Core.Abstractions
{
    public interface IOrientDBRecordSerializer<TDataType>
    {
        OrientDBRecordFormat RecordFormat { get; }

        TResultType Deserialize<TResultType>(TDataType data) where TResultType : OrientDBEntity;

        TDataType Serialize<T>(T input) where T : OrientDBEntity;
    }
}
