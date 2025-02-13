﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Device.Presentation.Services.Helpers
{
   public class RestartDeviceContext
    {
        public RestartDeviceContext(IDevice device, CancellationTokenSource cts)
        {
            Device = device;
            Cts = cts;
            HaveConflicts = false;
        }

        public IDevice Device { get; }
        public CancellationTokenSource Cts { get; }
        public bool HaveConflicts { get; set; }
        public UiEntityIdentifier UiEntityIdentifier { get; set; }
        public DeviceConflictContext DeviceConflictContext { get; set; }
    }
}
