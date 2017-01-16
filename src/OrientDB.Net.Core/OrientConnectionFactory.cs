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
            if (connectionProtocol == null)
                throw new ArgumentNullException($"{nameof(connectionProtocol)} cannot be null.");
            if (serializer == null)
                throw new ArgumentNullException($"{nameof(serializer)} cannot be null");
            if (logger == null)
                throw new ArgumentNullException($"{nameof(logger)} cannot be null");
            _connectionProtocol = connectionProtocol;
            _serializer = serializer;
            _logger = logger;
        }

        public IOrientConnection GetConnection()
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
    }
}
