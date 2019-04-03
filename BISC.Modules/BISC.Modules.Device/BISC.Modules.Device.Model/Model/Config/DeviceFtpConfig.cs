using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Device.Infrastructure.Model.Config;

namespace BISC.Modules.Device.Model.Model.Config
{
   public class DeviceFtpConfig: IDeviceFtpConfig
    {
        #region Implementation of IDeviceFtpConfig

        public string MacAddress { get; set; }
        public string TechKey { get; set; }
        public string SwitchMode { get; set; }
        public List<string> FilterAMacList { get; set; }
        public List<string> FilterBMacList { get; set; }
        public string Version { get; set; }

        #endregion
    }
}
