using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Device.Infrastructure.Loading
{
    public class DeviceLoadingEvent
    {
        public DeviceLoadingEvent(int? fullItemsCount, int currentItemsCount,string deviceNameFinded=null)
        {
            FullItemsCount = fullItemsCount;
            CurrentItemsCount = currentItemsCount;
            DeviceNameFinded = deviceNameFinded;
        }
        public int? FullItemsCount { get;  }
        public int CurrentItemsCount { get;}
        public string DeviceNameFinded { get; }
    }
}
