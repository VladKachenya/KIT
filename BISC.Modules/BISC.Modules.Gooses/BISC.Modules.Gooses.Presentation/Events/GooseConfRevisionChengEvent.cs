using System;

namespace BISC.Modules.Gooses.Presentation.Events
{
    public class GooseConfRevisionChengEvent
    {
        public Guid DeviceGuid { get; }
   
        public GooseConfRevisionChengEvent(Guid deviceGuid)
        {
            DeviceGuid = deviceGuid;
        }
    }
}