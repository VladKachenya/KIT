using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Presentation.Interfaces.Services;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Device.Presentation.Services
{
   public class DeviceAddingService: IDeviceAddingService
    {
        private readonly INavigationService _navigationService;

        public DeviceAddingService(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OpenDeviceAddingView()
        {
            _navigationService.NavigateViewToGlobalRegion(DeviceKeys.DeviceAddingViewKey);
        }
    }
}
