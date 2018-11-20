using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Device.Presentation.Services.Helpers
{
   public class RestartDeviceEntity
    {
        public RestartDeviceEntity(IDevice device, CancellationTokenSource cts)
        {
            Device = device;
            Cts = cts;
            HaveConflicts = false;
        }

        public IDevice Device { get; }
        public CancellationTokenSource Cts { get; }
        public bool HaveConflicts { get; set; }
        public TreeItemIdentifier TreeItemIdentifier { get; set; }
        public DeviceConflictEntity DeviceConflictEntity { get; set; }
    }
}
