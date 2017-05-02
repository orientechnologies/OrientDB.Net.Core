namespace OrientDB.Net.Core.Abstractions
{
    public interface IOrientDBLogger
    {
        void Debug(string message, params string[] properties);
        void Information(string message, params string[] properties);
        void Verbose(string message, params string[] properties);
        void Error(string message, params string[] properties);
        void Fatal(string message, params string[] properties);
        void Warning(string message, params string[] properties);
    }
}
