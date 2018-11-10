using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BISC.Modules.Device.Presentation.Services.Helpers
{
   public class RestartDeviceEntity
    {
        public RestartDeviceEntity(string deviceName, CancellationTokenSource cts, string ip, bool haveConflicts)
        {
            DeviceName = deviceName;
            Cts = cts;
            Ip = ip;
            HaveConflicts = haveConflicts;
        }

        public string DeviceName { get; }
        public CancellationTokenSource Cts { get; }
        public string Ip { get; }
        public bool HaveConflicts { get; }

    }
}
