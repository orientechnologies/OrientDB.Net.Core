using OrientDB.Net.Core.Abstractions;
using System;

namespace OrientDB.Net.Core.Configuration
{
    public class OrientDBConnectionConfiguration<TDataType>
    {
        private readonly OrientDBConfiguration _configuration;
        private readonly Action<IOrientDBConnectionProtocol<TDataType>> _configAction;
        private readonly Action<IOrientDBRecordSerializer<TDataType>> _serializerAction;

        internal OrientDBConnectionConfiguration(
            OrientDBConfiguration configuration, 
            Action<IOrientDBRecordSerializer<TDataType>> serializerAction, 
            Action<IOrientDBConnectionProtocol<TDataType>> configAction)
        {
            _configuration = configuration ?? throw new ArgumentNullException($"{nameof(configuration)} cannot be null.");
            _configAction = configAction ?? throw new ArgumentNullException($"{nameof(configAction)} cannot be null.");
            _serializerAction = serializerAction ?? throw new ArgumentNullException($"{nameof(serializerAction)} cannot be null.");
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