using System;

namespace BISC.Modules.Device.Infrastructure.Events
{
    public class LoadErrorEvent
    {
        public string Ip { get; }
        public Guid DeviceGuid { get; }

        public LoadErrorEvent(string ip, Guid deviceGuid)
        {
            Ip = ip;
            DeviceGuid = deviceGuid;
        }
    }
}