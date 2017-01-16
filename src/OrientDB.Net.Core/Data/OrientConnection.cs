using OrientDB.Net.Core.Abstractions;
using OrientDB.Net.Core.Models;
using System;
using System.Collections.Generic;

namespace OrientDB.Net.Core.Data
{
    public class OrientConnection<TDataType> : IOrientConnection
    {
        private readonly IOrientDBRecordSerializer<TDataType> _serializer;
        private readonly IOrientDBConnectionProtocol<TDataType> _connectionProtocol;
        private readonly IOrientDBLogger _logger;

        internal OrientConnection(IOrientDBRecordSerializer<TDataType> serializer, IOrientDBConnectionProtocol<TDataType> connectionProtocol, IOrientDBLogger logger)
        {
            if (serializer == null)
                throw new ArgumentNullException($"{nameof(serializer)} cannot be null.");
            if (connectionProtocol == null)
                throw new ArgumentNullException($"{nameof(connectionProtocol)} cannot be null.");
            if (logger == null)
                throw new ArgumentNullException($"{nameof(logger)} cannot be null.");
            _serializer = serializer;
            _connectionProtocol = connectionProtocol;
            _logger = logger;
        }

        public IEnumerable<TResultType> ExecuteQuery<TResultType>(string sql) where TResultType : OrientDBEntity
        {
            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentException($"{nameof(sql)} cannot be zero length or null");
            _logger.Debug($"Executing SQL Query: {sql}");
            var data = _connectionProtocol.ExecuteQuery<TResultType>(sql, _serializer);
            return data;
        }

        public IOrientDBCommandResult ExecuteCommand(string sql)
        {
            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentException($"{nameof(sql)} cannot be zero length or null");
            _logger.Debug($"Executing SQL Command: {sql}");
            var data = _connectionProtocol.ExecuteCommand(sql, _serializer);
            return data;
        }
    }
}
