using OrientDB.Net.Core.Abstractions;
using System;

namespace OrientDB.Net.Core.Configuration
{
    public class OrientDBConnectionProtocolConfiguration<TDataType>
    {
        public OrientDBSerializationConfiguration<TDataType> SerializeWith { get; }

        private readonly OrientDBConfiguration _configuration;
        private readonly Action<IOrientDBConnectionProtocol<TDataType>> _configAction;
        private readonly Action<IOrientDBRecordSerializer<TDataType>> _serializerAction;

        internal OrientDBConnectionProtocolConfiguration(OrientDBConfiguration configuration, Action<IOrientDBRecordSerializer<TDataType>> serializerAction, Action<IOrientDBConnectionProtocol<TDataType>> configAction)
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
            SerializeWith = new OrientDBSerializationConfiguration<TDataType>(_configuration, _serializerAction);
        }
    }
}
