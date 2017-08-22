namespace OrientDB.Net.Core.Abstractions
{
    public interface IOrientConnectionFactory
    {
        IOrientServerConnection CreateConnection();
    }
}