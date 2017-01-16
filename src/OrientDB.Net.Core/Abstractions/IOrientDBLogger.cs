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
