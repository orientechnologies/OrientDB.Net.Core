using OrientDB.Net.Core.Models;

namespace OrientDB.Net.Core.Abstractions
{
    public interface IOrientConnectionFactory
    {
        IOrientServerConnection CreateConnection();
    }
}
