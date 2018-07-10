using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Presentation.Interfaces;
using BISC.Modules.Device.Presentation.Interfaces.Factories;

namespace BISC.Modules.Device.Presentation.ViewModels.Factories
{
   public class DeviceViewModelFactory: IDeviceViewModelFactory
    {
        private readonly Func<IDeviceViewModel> _deviceViewModelCreator;

        public DeviceViewModelFactory(Func<IDeviceViewModel> deviceViewModelCreator)
        {
            _deviceViewModelCreator = deviceViewModelCreator;
        }
        public IDeviceViewModel CreateDeviceViewModel(IDevice device)
        {
            IDeviceViewModel deviceViewModel = _deviceViewModelCreator();
            deviceViewModel.DeviceName = device.Name;
            deviceViewModel.Device = device;
            return deviceViewModel;
        }
    }
}
