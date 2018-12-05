using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Device.Infrastructure.HelpClasses
{
   public class DeviceWarning
    {
        public DeviceWarning(string deviceName, string warningTag, string warningMasage = null)
        {
            DeviceName = deviceName;
            WarningTag = warningTag;
            WarningMasage = warningMasage;
        }

        public string DeviceName { get; }
        public string WarningTag { get; }
        public string WarningMasage { get; }

    }
}
