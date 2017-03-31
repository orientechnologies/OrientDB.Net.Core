using OrientDB.Net.Core.Models;
using System;

namespace OrientDB.Net.Core.Abstractions
{
    public interface IOrientDBConnectionProtocol<TDataType> : IDisposable
    {
        IOrientServerConnection CreateServerConnection(IOrientDBRecordSerializer<TDataType> serializer, IOrientDBLogger logger);
    }
}
