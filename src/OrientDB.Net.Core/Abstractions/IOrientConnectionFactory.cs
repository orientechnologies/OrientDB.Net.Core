namespace OrientDB.Net.Core.Abstractions
{
    public interface IOrientConnectionFactory
    {
        IOrientDatabaseConnection GetClientConnection();
        IOrientServerConnection GetServerConnection();
    }
}
