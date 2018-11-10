namespace BISC.Modules.Device.Infrastructure.Loading.Events
{
    public class DeviceLoadingEvent
    {
        public DeviceLoadingEvent(string ip, string deviceName=null, int? totalProgressCount=null, int? currentProgressCount=null, bool? isFinished=null)
        {
            Ip = ip;
            IsFinished = isFinished;
            DeviceName = deviceName;
            TotalProgressCount = totalProgressCount;
            CurrentProgressCount = currentProgressCount;
        }

        public string Ip { get;  }
        public string DeviceName { get;  }
        public int? TotalProgressCount { get; }
        public int? CurrentProgressCount { get;  }
        public bool? IsFinished { get; set; }
    }
}
