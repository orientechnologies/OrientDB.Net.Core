using OrientDB.Net.Core.Models;

namespace OrientDB.Net.Core.Abstractions
{
    public interface IOrientDBTransaction
    {
        void AddEntity<T>(T entity) where T : OrientDBEntity;
        void Remove<T>(T entity) where T : OrientDBEntity;
        void Update<T>(T entity) where T : OrientDBEntity;
        void AddEdge(Edge edge, Vertex from, Vertex to);
        void Commit();
        void Reset();
    }
}