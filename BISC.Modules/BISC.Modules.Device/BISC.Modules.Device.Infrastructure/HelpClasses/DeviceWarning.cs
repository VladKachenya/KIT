using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Device.Infrastructure.HelpClasses
{
   public class DeviceWarning
    {
        public DeviceWarning(string deviceName, string warningTag)
        {
            DeviceName = deviceName;
            WarningTag = warningTag;
        }

        public string DeviceName { get; }
        public string WarningTag { get; }
    }
}
