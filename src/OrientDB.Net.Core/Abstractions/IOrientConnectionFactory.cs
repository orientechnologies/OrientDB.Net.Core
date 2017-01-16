namespace OrientDB.Net.Core.Abstractions
{
    public interface IOrientConnectionFactory
    {
        IOrientConnection GetConnection();
    }
}
