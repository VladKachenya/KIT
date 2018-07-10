using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Device.Presentation.Interfaces;
using BISC.Presentation.BaseItems.ViewModels;

namespace BISC.Modules.Device.Presentation.ViewModels
{
   public class DeviceViewModel:ComplexViewModelBase, IDeviceViewModel
    {
        public string DeviceName { get; set; }
    }
}
