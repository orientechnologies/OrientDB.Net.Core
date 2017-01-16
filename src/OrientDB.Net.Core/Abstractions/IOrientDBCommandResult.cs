namespace OrientDB.Net.Core.Abstractions
{
    public interface IOrientDBCommandResult
    {
        int RecordsAffected { get; }
    }
}
