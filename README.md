## OrientDB.Net.Core ##
---

*Status:* **_Experimental_**

OrientDB.Net.Core represents a re-imaging of the .NET OrientDB SDK for .NET Framework. Written targeting .NET Standard 1.6. The purpose in re-imaging the .NET OrientDB SDK, is to design an extensible base library that can support any number of implementations in the same vein as the Serilog project.

The current state of the driver and its derived components is pre-alpha. As such, the library itself can and will change substantially over the coming months.

OrientDB.Net.Core is the core library that provides the
base classes and abstractions that represent the "core" of the API. It provides abstractions for communication with OrientDB and Serialization/De-Serialization of the output. The intent is to be able to flip between binary and http in the beginning and add any additional support that may arise.

To install the driver using NuGet:

```
Install-Package OrientDB.Net.Core
```

Quick Usage Example of SDK:

Person Entity:

```cs
public class Person : OrientDBEntity
{
    public int Age { get; set; }
    public string Name { get; set; }
    public string FirstName {get; set; }
    public string LastName { get; set; }
    public IList<string> FavoriteColors { get; set; }

    public override void Hydrate(IDictionary<string, object> data)
    {
            Age = (int)data?["Age"];
            FirstName = data?["FirstName"]?.ToString();
            LastName = data?["LastName"]?.ToString();
            FavoriteColors = data.ContainsKey("FavoriteColors") ? (data?["FavoriteColors"] as IList<object>).Select(n => n.ToString()).ToList() : new List<string>();
    }
}
```

SDK Interaction:

```
Install-Package OrientDB.Net.ConnectionProtocols.Binary
Install-Package OrientDB.Net.Serializers.RecordCSVSerializer
```

```cs
IEnumerable<Person> persons = new List<Person>();

IOrientServerConnection server = new OrientDBConfiguration()
    .ConnectWith<byte[]>()
    .Connect(new BinaryProtocol("127.0.0.1", "root", "root"))
    .SerializeWith.Serializer(new OrientDBRecordCSVSerializer())
    .LogWith.Logger(new ConsoleOrientDBLogger())
    .CreateFactory()
    .CreateConnection();

IOrientDatabaseConnection database;

if (server.DatabaseExists("ConnectionTest", StorageType.PLocal))
    database = server.DatabaseConnect("ConnectionTest", DatabaseType.Document);
else
    database = server.CreateDatabase("ConnectionTest", DatabaseType.Document, StorageType.PLocal);

database.ExecuteCommand("CREATE CLASS Person");

var transaction = database.CreateTransaction();
var person1 = new Person { Age = 33, FirstName = "Jane", LastName = "Doe", FavoriteColors = new[] { "black", "blue" } };
transaction.AddEntity(person1);
transaction.AddEntity(new Person { Age = 5, FirstName = "John", LastName = "Doe", FavoriteColors = new[] { "red", "blue" } });
transaction.Commit();
transaction = database.CreateTransaction();
transaction.Remove(person1);
transaction.Commit();

persons = database.ExecuteQuery<Person>("SELECT * FROM Person");    
```

## Interface Documentation - OrientDB.Net.Core.Abstractions

### IOrientDatabaseConnection

IOrientConnection provides an interface for interacting with an OrientDB database.

```cs
namespace OrientDB.Net.Core.Abstractions
{
    public interface IOrientDatabaseConnection
    {
        IEnumerable<TResultType> ExecuteQuery<TResultType>(string sql) where TResultType : OrientDBEntity;
        IEnumerable<TResultType> ExecutePreparedQuery<TResultType>(string sql, params string[] parameters) where TResultType : OrientDBEntity;
        IOrientDBCommandResult ExecuteCommand(string sql);
        IOrientDBTransaction CreateTransaction();
    }
}
```

### IOrientServerConnection

IOrientServerConnection provides an interface for interacting with an OrientDB Server Host itself.

```cs
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

```cs
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

```cs
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

```cs
namespace OrientDB.Net.Core.Abstractions
{
    public interface IOrientDBConnectionProtocol<TDataType> : IDisposable
    {
        IOrientServerConnection CreateServerConnection(IOrientDBRecordSerializer<TDataType> serializer, IOrientDBLogger logger);
    }
}
```

### IOrientDBLogger

The IOrientDBLogger interface is used to create Logging wrappers to hook into the logging framework with OrientDB.Net.Core factories and implementations. It is recommended that any Protocols or Serializers use the interface for maximum logging compatibility.

```cs
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

```cs
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

```cs
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
