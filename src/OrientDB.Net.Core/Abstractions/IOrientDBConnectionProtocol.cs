using OrientDB.Net.Core.Models;
using System;
using System.Collections.Generic;

namespace OrientDB.Net.Core.Abstractions
{
    public interface IOrientDBConnectionProtocol<TDataType> : IDisposable
    {
        IEnumerable<TResultType> ExecuteQuery<TResultType>(string sql, IOrientDBRecordSerializer<TDataType> serializer) where TResultType : OrientDBEntity;
        IOrientDBCommandResult ExecuteCommand(string sql, IOrientDBRecordSerializer<TDataType> serializer);
    }
}
