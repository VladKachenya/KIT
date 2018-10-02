namespace BISC.Infrastructure.Global.Logging
{
    public interface ILoggableMessage
    {
        string Message { get; set; }
        SeverityEnum Severity { get; set; }
    }
}