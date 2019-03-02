using System;

namespace BISC.Modules.Device.Infrastructure.Loading.Events
{
    public class DeviceLoadingEvent
    {
        public DeviceLoadingEvent(Guid deviceGuid, string deviceName=null, int? totalProgressCount=null, int? currentProgressCount=null, bool? isFinished=null)
        {
            DeviceGuid = deviceGuid;
            IsFinished = isFinished;
            DeviceName = deviceName;
            TotalProgressCount = totalProgressCount;
            CurrentProgressCount = currentProgressCount;
        }

        public Guid DeviceGuid { get;  }
        public string DeviceName { get;  }
        public int? TotalProgressCount { get; }
        public int? CurrentProgressCount { get;  }
        public bool? IsFinished { get; set; }
    }
}
