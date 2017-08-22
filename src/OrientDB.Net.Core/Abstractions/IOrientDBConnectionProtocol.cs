using System;
using Microsoft.Extensions.Logging;

namespace OrientDB.Net.Core.Abstractions
{
    public interface IOrientDBConnectionProtocol<TDataType> : IDisposable
    {
        IOrientServerConnection CreateServerConnection(IOrientDBRecordSerializer<TDataType> serializer, ILogger logger);
    }
}
