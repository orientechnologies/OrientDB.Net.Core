## OrientDB.Net.Core ##
---

*Status:* **_Experimental_**

OrientDB.Net.Core represents a re-imaging of the .NET OrientDB SDK for .NET Framework. Written using the new .NET Core SDK, it supports .NET Framework 4.5.1 and .NET Core 1.0 and above. The purpose in re-imaging the .NET OrientDB SDK, is to design an extensible base library that can support any number of implementations in the same vein as the Serilog project.

The current state of the driver and its derived components is pre-alpha. As such, the library itself can and will change substantially over the coming months.

OrientDB.Net.Core is the core library that provides the
base classes and abstractions that represent the "core" of the API. It provides abstractions for communication with OrientDB and Serialization/De-Serialization of the output. The intent is to be able to flip between binary and http in the beginning and add any additional support that may arise.

To install the driver using NuGet:

```
Install-Package OrientDB.Net.Core -Version 0.1.0
```

## Interface Documentation - OrientDB.Net.Core.Abstractions

### IOrientDatabaseConnection

IOrientConnection provides an interface for interacting with an OrientDB database.

```
namespace OrientDB.Net.Core.Abstractions
{
    public interface IOrientDatabaseConnection
    {
        IEnumerable<TResultType> ExecuteQuery<TResultType>(string sql) where TResultType : OrientDBEntity;
        IOrientDBCommandResult ExecuteCommand(string sql);        
    }
}
```

### IOrientServerConnection

IOrientServerConnection provides an interface for interacting with an OrientDB Server Host itself.

```
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
```

### IOrientConnectionFactory

IOrientConnectionFactory provides the basic interface for implementing an OrientDB Connection Factory.

```
namespace OrientDB.Net.Core.Abstractions
{
    public interface IOrientConnectionFactory
    {
        IOrientDatabaseConnection GetClientConnection();
        IOrientServerConnection GetServerConnection();
    }
}
```

### IOrientDBCommandResult

IOrientDBCommandResult provides the basic interface for implementing a Command Result. This is not for Queries. Also note that this interface is currently under review to possibly be removed or heavily modified.

```
namespace OrientDB.Net.Core.Abstractions
{
    public interface IOrientDBCommandResult
    {
        int RecordsAffected { get; }
    }
}
```

### IOrientDBConnectionProtocol

IOrientDBConnectionProtocol<TDataType> interface is used to create an OrientDB Protocol implementation. This could be the binary, HTTP, or yet undeveloped means of communicating with the OrientDB server. The <TDataType> generic constraint is used to define what kind of IOrientDBRecordSerializer<TDataType> can be used with the protocol implementation. This is in place so that an incompatible IOrientDBRecordSerializer cannot be used.

```
namespace OrientDB.Net.Core.Abstractions
{
    public interface IOrientDBConnectionProtocol<TDataType> : IDisposable
    {
        IEnumerable<TResultType> ExecuteQuery<TResultType>(string sql, IOrientDBRecordSerializer<TDataType> serializer) where TResultType : OrientDBEntity;
        IOrientDBCommandResult ExecuteCommand(string sql, IOrientDBRecordSerializer<TDataType> serializer);
        IOrientDBTransaction CreateTransaction(IOrientDBRecordSerializer<byte[]> serializer);
    }
}
```

### IOrientDBLogger

The IOrientDBLogger interface is used to create Logging wrappers to hook into the logging framework with OrientDB.Net.Core factories and implementations. It is recommended that any Protocols or Serializers use the interface for maximum logging compatibility.

```
namespace OrientDB.Net.Core.Abstractions
{
    public interface IOrientDBLogger
    {
        void Debug(string message);
        void Information(string message);
        void Verbose(string message);
        void Error(string message);
        void Fatal(string message);
        void Warning(string message);
    }
}
```

### IOrientDBRecordSerializer

The IOrientDBRecordSerializer<TDataType> interface is used to create Record Serializers for IOrientDBConnectionProtocols. It should be used to effectively serialize records for the chosen protocol and deserialize results in a way that the protocol implementation can reason about.

```
namespace OrientDB.Net.Core.Abstractions
{
    public interface IOrientDBRecordSerializer<TDataType>
    {
        OrientDBRecordFormat RecordFormat { get; }
        TResultType Deserialize<TResultType>(TDataType data) where TResultType : OrientDBEntity;
        TDataType Serialize<T>(T input) where T : OrientDBEntity;
    }
}
```

### IOrientDBTransaction

IOrientDBTransaction is used to create Transaction implementations for OrientDB.Net Protocols.

```
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
```