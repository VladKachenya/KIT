﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Device.Presentation.Services.Helpers
{
   public class ReconnectDeviceContext
    {
        public ReconnectDeviceContext(IDevice device, UiEntityIdentifier deviceUiEntityIdentifier)
        {
            Device = device;
            DeviceUiEntityIdentifier = deviceUiEntityIdentifier;
        }
        public IDevice Device { get; }
        public UiEntityIdentifier DeviceUiEntityIdentifier { get; }
    }
}
