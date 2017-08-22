using System;
using Microsoft.Extensions.Logging;

namespace OrientDB.Net.Core.Configuration
{
    public class OrientDBLoggingConfiguration
    {
        private readonly OrientDBConfiguration _configuration;
        private readonly Action<ILogger> _loggerAction;

        internal OrientDBLoggingConfiguration(OrientDBConfiguration configuration, Action<ILogger> loggerAction)
        {
            _configuration = configuration ?? throw new ArgumentNullException($"{nameof(configuration)} cannot be null.");
            _loggerAction = loggerAction ?? throw new ArgumentNullException($"{nameof(loggerAction)} cannot be null");
        }

        public OrientDBConfiguration Logger(ILogger logger)
        {
            if (logger == null)
                throw new ArgumentNullException($"{nameof(logger)} cannot be null.");

            _loggerAction(logger);

            return _configuration;
        }
    }
}