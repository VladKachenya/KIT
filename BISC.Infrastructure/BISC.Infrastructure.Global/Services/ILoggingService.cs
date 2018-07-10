namespace BISC.Infrastructure.Global.Services
{
    public interface ILoggingService
    {
        void LogUserAction(string actionName);
        void LogMessage(string message);
    }
}