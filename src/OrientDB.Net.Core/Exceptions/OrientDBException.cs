using System;

namespace OrientDB.Net.Core.Exceptions
{
    public class OrientDBException : Exception
    {
        public OrientDBExceptionType ExceptionType { get; }

        public OrientDBException(OrientDBExceptionType type, string message) : base(message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("A message must be provided.");
            ExceptionType = type;
        }
    }
}
