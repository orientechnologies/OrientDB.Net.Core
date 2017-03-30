using OrientDB.Net.Core.Models;
using System.Collections.Generic;

namespace OrientDB.Net.Core.Abstractions
{
    public interface IOrientServerConnection
    {
        IOrientDatabaseConnection CreateDatabase(string database, StorageType type);
        void DeleteDatabase(string database, StorageType type);
        bool DatabaseExists(string database, StorageType type);
        void Shutdown(string username, string password);
        IEnumerable<string> ListDatabases();
        string GetConfigValue(string name);
        void SetConfigValue(string name, string value);
    }
}
