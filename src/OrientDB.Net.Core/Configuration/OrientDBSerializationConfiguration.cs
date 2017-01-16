using OrientDB.Net.Core.Abstractions;
using System;

namespace OrientDB.Net.Core.Configuration
{
    public class OrientDBSerializationConfiguration<TDataType>
    {
        private readonly OrientDBConfiguration _configuration;
        private readonly Action<IOrientDBRecordSerializer<TDataType>> _addSerializer;

        internal OrientDBSerializationConfiguration(OrientDBConfiguration configuration, Action<IOrientDBRecordSerializer<TDataType>> addSerializer)
        {
            if (configuration == null)
                throw new ArgumentNullException($"{nameof(configuration)} cannot be null.");
            if (addSerializer == null)
                throw new ArgumentNullException($"{nameof(addSerializer)} cannot be null.");
            _configuration = configuration;
            _addSerializer = addSerializer;
        }

        public OrientDBConfiguration Serializer(IOrientDBRecordSerializer<TDataType> serializer)
        {
            if (serializer == null)
                throw new ArgumentNullException($"{nameof(serializer)} cannot be null.");
            _addSerializer(serializer);
            return _configuration;
        }
    }
}