using OrientDB.Net.Core.Models;
using System.Collections.Generic;

namespace OrientDB.Net.Core.Abstractions
{
    public interface IOrientDBCommandResult
    {
        int RecordsAffected { get; }

        IEnumerable<DictionaryOrientDBEntity> UpdatedRecords { get; }
    }
}
