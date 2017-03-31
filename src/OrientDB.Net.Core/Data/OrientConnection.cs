using OrientDB.Net.Core.Abstractions;
using OrientDB.Net.Core.Models;
using System;
using System.Collections.Generic;

namespace OrientDB.Net.Core.Data
{
    public class OrientConnection<TDataType> : IOrientDatabaseConnection
    {
        private readonly IOrientDBLogger _logger;

        private readonly IOrientServerConnection _serverConnection;
        private readonly IOrientDatabaseConnection _databaseConnection;

        internal OrientConnection(IOrientDBRecordSerializer<TDataType> serializer, IOrientDBConnectionProtocol<TDataType> connectionProtocol, IOrientDBLogger logger, string database, DatabaseType databaseType, int poolSize = 10)
        {
            if (serializer == null) throw new ArgumentNullException($"{nameof(serializer)}");
            if (connectionProtocol == null) throw new ArgumentNullException($"{nameof(connectionProtocol)}");
            if (string.IsNullOrWhiteSpace(database)) throw new ArgumentException($"{nameof(database)}");
            _logger = logger ?? throw new ArgumentNullException($"{nameof(logger)}");

            _serverConnection = connectionProtocol.CreateServerConnection(serializer, logger);
            _databaseConnection = _serverConnection.DatabaseConnect(database, databaseType, poolSize);
        }

        public IEnumerable<TResultType> ExecuteQuery<TResultType>(string sql) where TResultType : OrientDBEntity
        {
            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentException($"{nameof(sql)} cannot be zero length or null");
            _logger.Debug($"Executing SQL Query: {sql}");
            var data = _databaseConnection.ExecuteQuery<TResultType>(sql);
            return data;
        }

        public IOrientDBCommandResult ExecuteCommand(string sql)
        {
            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentException($"{nameof(sql)} cannot be zero length or null");
            _logger.Debug($"Executing SQL Command: {sql}");
            var data = _databaseConnection.ExecuteCommand(sql);
            return data;
        }

        public IOrientDBTransaction CreateTransaction()
        {
            return _databaseConnection.CreateTransaction();
        }
    }
}
