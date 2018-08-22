using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Device.Infrastructure.Loading
{
    public class DeviceLoadingEvent
    {
        public DeviceLoadingEvent(int fullItemsCount, int currentItemsCount)
        {
            FullItemsCount = fullItemsCount;
            CurrentItemsCount = currentItemsCount;
        }
        public int FullItemsCount { get;  }
        public int CurrentItemsCount { get;}
    }
}
