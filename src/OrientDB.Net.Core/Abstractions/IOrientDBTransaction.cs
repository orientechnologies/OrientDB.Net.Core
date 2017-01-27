using OrientDB.Net.Core.Models;

namespace OrientDB.Net.Core.Abstractions
{
    public interface IOrientDBTransaction
    {
        void AddEntity<T>(T entity) where T : OrientDBEntity;
        void Commit();
    }
}