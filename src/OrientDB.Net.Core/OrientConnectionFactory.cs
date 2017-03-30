using OrientDB.Net.Core.Abstractions;
using OrientDB.Net.Core.Data;
using System;

namespace OrientDB.Net.Core
{
    internal class OrientConnectionFactory<TDataType> : IOrientConnectionFactory
    {
        private readonly IOrientDBConnectionProtocol<TDataType> _connectionProtocol;
        private readonly IOrientDBRecordSerializer<TDataType> _serializer;
        private readonly IOrientDBLogger _logger;

        internal OrientConnectionFactory(IOrientDBConnectionProtocol<TDataType> connectionProtocol,
            IOrientDBRecordSerializer<TDataType> serializer, IOrientDBLogger logger)
        {
            _connectionProtocol = connectionProtocol ?? throw new ArgumentNullException($"{nameof(connectionProtocol)} cannot be null.");
            _serializer = serializer ?? throw new ArgumentNullException($"{nameof(serializer)} cannot be null");
            _logger = logger ?? throw new ArgumentNullException($"{nameof(logger)} cannot be null");
        }

        public IOrientDatabaseConnection GetClientConnection()
        {         
            if (_connectionProtocol == null)
                throw new NullReferenceException($"{nameof(_connectionProtocol)} cannot be null.");
            if (_serializer == null)
                throw new NullReferenceException($"{nameof(_serializer)} cannot be null.");
            if (_logger == null)
                throw new NullReferenceException($"{nameof(_logger)} cannot be null.");

            var connection = new OrientConnection<TDataType>(_serializer, _connectionProtocol, _logger);

            return connection;
        }

        public IOrientServerConnection GetServerConnection()
        {
            throw new NotImplementedException();
        }
    }
}
