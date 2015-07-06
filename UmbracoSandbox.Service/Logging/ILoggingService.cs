namespace UmbracoSandbox.Service.Logging
{
    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error
    }

    public interface ILoggingService
    {
        void Log(string message, LogLevel level);
    }
}
