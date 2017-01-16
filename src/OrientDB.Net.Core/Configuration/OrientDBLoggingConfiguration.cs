using OrientDB.Net.Core.Abstractions;
using System;

namespace OrientDB.Net.Core.Configuration
{
    public class OrientDBLoggingConfiguration
    {
        private readonly OrientDBConfiguration _configuration;
        private readonly Action<IOrientDBLogger> _loggerAction;

        internal OrientDBLoggingConfiguration(OrientDBConfiguration configuration, Action<IOrientDBLogger> loggerAction)
        {
            if (configuration == null)
                throw new ArgumentNullException($"{nameof(configuration)} cannot be null.");
            if (loggerAction == null)
                throw new ArgumentNullException($"{nameof(loggerAction)} cannot be null");
            _configuration = configuration;
            _loggerAction = loggerAction;
        }

        public OrientDBConfiguration Logger(IOrientDBLogger logger)
        {
            if (logger == null)
                throw new ArgumentNullException($"{nameof(logger)} cannot be null.");
            _loggerAction(logger);
            return _configuration;
        }
    }
}