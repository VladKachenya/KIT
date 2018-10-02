using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Presentation.Interfaces;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Navigation;

namespace BISC.Modules.Device.Presentation.ViewModels
{
   public class DeviceDetailsViewModel:NavigationViewModelBase,IDeviceDetailsViewModel
    {
        private readonly ISclCommunicationModelService _sclCommunicationModel;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly IBiscProject _biscProject;

        public DeviceDetailsViewModel(ISclCommunicationModelService sclCommunicationModel,IConnectionPoolService connectionPoolService,IBiscProject biscProject)
        {
            _sclCommunicationModel = sclCommunicationModel;
            _connectionPoolService = connectionPoolService;
            _biscProject = biscProject;
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
            if (_device.Ip != null)
            {
                DeviceIp = _device.Ip;
            }
            else
            {
                DeviceIp = _sclCommunicationModel.GetIpOfDevice(_device.Name, _biscProject.MainSclModel.Value);
            }
            base.OnNavigatedTo(navigationContext);
        }
    }
}
