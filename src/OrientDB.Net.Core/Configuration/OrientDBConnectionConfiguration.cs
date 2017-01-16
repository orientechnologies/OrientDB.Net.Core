using OrientDB.Net.Core.Abstractions;
using System;

namespace OrientDB.Net.Core.Configuration
{
    public class OrientDBConnectionConfiguration<TDataType>
    {
        private readonly OrientDBConfiguration _configuration;
        private readonly Action<IOrientDBConnectionProtocol<TDataType>> _configAction;
        private readonly Action<IOrientDBRecordSerializer<TDataType>> _serializerAction;

        internal OrientDBConnectionConfiguration(OrientDBConfiguration configuration, Action<IOrientDBRecordSerializer<TDataType>> serializerAction, Action<IOrientDBConnectionProtocol<TDataType>> configAction)
        {
            if (configuration == null)
                throw new ArgumentNullException($"{nameof(configuration)} cannot be null.");
            if (serializerAction == null)
                throw new ArgumentNullException($"{nameof(serializerAction)} cannot be null.");
            if (configAction == null)
                throw new ArgumentNullException($"{nameof(configAction)} cannot be null.");
            _configuration = configuration;
            _configAction = configAction;
            _serializerAction = serializerAction;
        }

        public OrientDBConnectionProtocolConfiguration<TDataType> Connect(IOrientDBConnectionProtocol<TDataType> protocol)
        {
            if (protocol == null)
                throw new ArgumentNullException($"{nameof(protocol)} cannot be null.");
            _configAction(protocol);
            return new OrientDBConnectionProtocolConfiguration<TDataType>(_configuration, _serializerAction, _configAction);
        }
    }
}