using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.Connection.Infrastructure.Events;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Connection.Presentation.Interfaces.Factorys;
using BISC.Modules.Connection.Presentation.Interfaces.ViewModel;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Presentation.Interfaces;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Navigation;

namespace BISC.Modules.Device.Presentation.ViewModels
{
    public class DeviceDetailsViewModel : NavigationViewModelBase, IDeviceDetailsViewModel
    {
        private readonly ISclCommunicationModelService _sclCommunicationModel;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly IBiscProject _biscProject;
        private readonly IIpAddressViewModelFactory _ipAddressViewModelFactory;
        private readonly IGlobalEventsService _globalEventsService;

        private string _deviceName;
        private IDevice _device;
        private string _regionName;
        private string _deviceIp;
        private bool _isIpUnchangeable;

        public DeviceDetailsViewModel(ISclCommunicationModelService sclCommunicationModel, 
            IConnectionPoolService connectionPoolService, IBiscProject biscProject, 
            IIpAddressViewModelFactory ipAddressViewModelFactory, IGlobalEventsService globalEventsService)
        {
            _sclCommunicationModel = sclCommunicationModel;
            _connectionPoolService = connectionPoolService;
            _biscProject = biscProject;
            _ipAddressViewModelFactory = ipAddressViewModelFactory;
            _globalEventsService = globalEventsService;
        }

        #region public methods

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

        public bool IsIpUnchangeable
        {
            get => _isIpUnchangeable;
            set => SetProperty(ref _isIpUnchangeable, value, true);
        }

        public IIpAddressViewModel IpAddressViewModel { get; protected set; }
        #endregion

        #region override methods
        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>(DeviceKeys.DeviceModelKey);
            _regionName = navigationContext.BiscNavigationParameters
                .GetParameterByName<UiEntityIdentifier>(UiEntityIdentifier.Key).ItemId.ToString();
            DeviceName = _device.Name;
            if (_device.Ip != null)
            {
                DeviceIp = _device.Ip;
            }
            else
            {
                DeviceIp = _sclCommunicationModel.GetIpOfDevice(_device.Name, _biscProject.MainSclModel.Value);
            }
            IsIpUnchangeable = _connectionPoolService.GetConnection(DeviceIp).IsConnected;
            IpAddressViewModel = _ipAddressViewModelFactory.GetPingItemViewModel(DeviceIp,
                _connectionPoolService.GetConnection(DeviceIp).IsConnected);
            _globalEventsService.Subscribe<LossConnectionEvent>(OnLostConnectionEvent);
            base.OnNavigatedTo(navigationContext);
        }

        protected override void OnDisposing()
        {
            _globalEventsService.Unsubscribe<LossConnectionEvent>(OnLostConnectionEvent);
            base.OnDisposing();
        }
        #endregion

        #region private methods

        private void OnLostConnectionEvent(LossConnectionEvent lossConnectionEvent)
        {
            if (lossConnectionEvent.Ip == _device.Ip)
            {
                (IpAddressViewModel as ComplexViewModelBase)?.SetIsEditable(!_connectionPoolService.GetConnection(_device.Ip).IsConnected);
            }
        }


        #endregion

    }
}
