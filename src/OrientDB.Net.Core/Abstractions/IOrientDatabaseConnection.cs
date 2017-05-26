using OrientDB.Net.Core.Models;
using System.Collections.Generic;

namespace OrientDB.Net.Core.Abstractions
{
    public interface IOrientDatabaseConnection
    {
        IEnumerable<TResultType> ExecuteQuery<TResultType>(string sql) where TResultType : OrientDBEntity;
        IEnumerable<TResultType> ExecutePreparedQuery<TResultType>(string sql, IDictionary<string, object> parameters) where TResultType : OrientDBEntity;
        IOrientDBCommandResult ExecuteCommand(string sql);
        IOrientDBTransaction CreateTransaction();
    }
}
