using System;

namespace BISC.Infrastructure.Global.Logging
{
    public interface ILoggableMessage
    {
        string Message { get; set; }
        SeverityEnum Severity { get; set; }
        DateTime MessageDateTime { get; set; }
        MessageTypeEnum MessageType { get; set; }
    }
}