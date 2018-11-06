using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Device.Infrastructure.HelpClasses;

namespace BISC.Modules.Device.Infrastructure.Events
{
   public class DeviceWarningsChanged
   {
       public DeviceWarningsChanged(string deviceNameOfWarning)
       {
           DeviceNameOfWarning = deviceNameOfWarning;
       }

       public string DeviceNameOfWarning { get; }
   }
}
