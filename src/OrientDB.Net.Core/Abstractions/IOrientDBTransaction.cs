namespace OrientDB.Net.Core.Abstractions
{
    public interface IOrientDBTransaction
    {
        void AddQuery(string query);
        void AddCommand(string command);
        void Commit();
    }
}