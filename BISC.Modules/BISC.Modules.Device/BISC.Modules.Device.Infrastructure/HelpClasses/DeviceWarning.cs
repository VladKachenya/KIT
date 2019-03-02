using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Device.Infrastructure.HelpClasses
{
   public class DeviceWarning
    {
        public DeviceWarning(Guid deviceGuid, string warningTag, string warningMasage = null)
        {
            DeviceGuid = deviceGuid;
            WarningTag = warningTag;
            WarningMasage = warningMasage;
        }

        public Guid DeviceGuid { get; }
        public string WarningTag { get; }
        public string WarningMasage { get; }

    }
}
