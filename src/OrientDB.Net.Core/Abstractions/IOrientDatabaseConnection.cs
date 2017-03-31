﻿using OrientDB.Net.Core.Models;
using System.Collections.Generic;

namespace OrientDB.Net.Core.Abstractions
{
    public interface IOrientDatabaseConnection
    {
        IEnumerable<TResultType> ExecuteQuery<TResultType>(string sql) where TResultType : OrientDBEntity;
        IOrientDBCommandResult ExecuteCommand(string sql);
        IOrientDBTransaction CreateTransaction();
    }
}
