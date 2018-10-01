using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Presentation.Interfaces;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Navigation;

namespace BISC.Modules.Device.Presentation.ViewModels
{
   public class DeviceDetailsViewModel:NavigationViewModelBase,IDeviceDetailsViewModel
    {
        public DeviceDetailsViewModel()
        {
            
        }


        private string _deviceName;
        private IDevice _device;
        private string _deviceIp;

        public string DeviceName
        {
            get => _deviceName;
            set { SetProperty(ref _deviceName, value); }
        }
      public string DeviceIp
        {
            get => _deviceIp;
            set { SetProperty(ref _deviceIp, value); }
        }

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>(DeviceKeys.DeviceModelKey);
            DeviceName = _device.Name;
            DeviceIp = _device.Ip;
            base.OnNavigatedTo(navigationContext);
        }
    }
}
