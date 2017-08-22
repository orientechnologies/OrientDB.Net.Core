using OrientDB.Net.Core.Abstractions;
using System;

namespace OrientDB.Net.Core.Configuration
{
    public class OrientDBSerializationConfiguration<TDataType>
    {
        private readonly OrientDBConfiguration _configuration;
        private readonly Action<IOrientDBRecordSerializer<TDataType>> _addSerializer;

        internal OrientDBSerializationConfiguration(
            OrientDBConfiguration configuration, 
            Action<IOrientDBRecordSerializer<TDataType>> addSerializer)
        {
            _configuration = configuration ?? throw new ArgumentNullException($"{nameof(configuration)} cannot be null.");
            _addSerializer = addSerializer ?? throw new ArgumentNullException($"{nameof(addSerializer)} cannot be null.");
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