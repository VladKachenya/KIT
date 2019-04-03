using System.Collections.Generic;

namespace BISC.Modules.Device.Infrastructure.Model.Config
{
    public interface IDeviceFtpConfig
    {
        string MacAddress { get; set; }
        string TechKey { get; set; }
        string SwitchMode { get; set; }
        List<string> FilterAMacList { get; set; }
        List<string> FilterBMacList { get; set; }
        string Version { get; set; }

    }
}