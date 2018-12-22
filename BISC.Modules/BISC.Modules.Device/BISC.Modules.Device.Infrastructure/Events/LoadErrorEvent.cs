namespace BISC.Modules.Device.Infrastructure.Events
{
    public class LoadErrorEvent
    {
        public string Ip { get; }
        public string DeviceName { get; }

        public LoadErrorEvent(string ip, string deviceName)
        {
            Ip = ip;
            DeviceName = deviceName;
        }
    }
}