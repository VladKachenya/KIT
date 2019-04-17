using System;

namespace BISC.Modules.Reports.Infrastructure.Events
{
    public class ReportConfRevisionChengEvent
    {
        public Guid DeviceGuid { get; }

        public ReportConfRevisionChengEvent(Guid deviceGuid)
        {
            DeviceGuid = deviceGuid;
        }
    }
}