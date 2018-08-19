using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Connection.Presentation.Events;
using BISC.Modules.Connection.Presentation.Interfaces.Factorys;
using BISC.Modules.Connection.Presentation.Interfaces.ViewModel;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Device.Presentation.Interfaces;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Navigation;

namespace BISC.Modules.Device.Presentation.ViewModels
{
   public class DeviceConnectingViewModel:NavigationViewModelBase,IDeviceConnectingViewModel
    {
        private readonly ICommandFactory _commandFactory;
        private readonly IDeviceConnectionService _deviceConnectionService;
        private readonly IConfigurationService _configurationService;
        private readonly IGlobalEventsService _globalEventsService;
        private readonly IIpAddressViewModelFactory _ipAddressViewModelFactory;
        private IIpAddressViewModel _selectedIpAddressViewModel;

        public DeviceConnectingViewModel(ICommandFactory commandFactory, IDeviceConnectionService deviceConnectionService,
            IConfigurationService configurationService,IGlobalEventsService globalEventsService,IIpAddressViewModelFactory ipAddressViewModelFactory)
        {
            _commandFactory = commandFactory;
            _deviceConnectionService = deviceConnectionService;
            _configurationService = configurationService;
            _globalEventsService = globalEventsService;
            _ipAddressViewModelFactory = ipAddressViewModelFactory;
            LastConnectedIps=new ObservableCollection<IIpAddressViewModel>();
            ConnectDeviceCommand = commandFactory.CreatePresentationCommand(OnConnectDeviceExecute);
            var lastConnectedIp = _configurationService.LastConnectedIpAddresses.Count > 0
                ? _configurationService.LastConnectedIpAddresses[0]
                : "...";
           SelectedIpAddressViewModel= _ipAddressViewModelFactory.GetPingItemViewModel(lastConnectedIp, false);

            LastConnectedIps =
                _ipAddressViewModelFactory.GetPingViewModelReadonlyCollection(_configurationService.LastConnectedIpAddresses);

        }

        private void OnConnectDeviceExecute()
        {
            _deviceConnectionService.ConnectDevice(SelectedIpAddressViewModel.FullIp);
        }

        public IIpAddressViewModel SelectedIpAddressViewModel
        {
            get => _selectedIpAddressViewModel;
            set { SetProperty(ref _selectedIpAddressViewModel, value); }
        }

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _globalEventsService.Subscribe<IpSelectedEvent>(OnIpSelectedEvent);
            base.OnNavigatedTo(navigationContext);
        }

        protected override void OnNavigatedFrom(BiscNavigationContext navigationContext)
        {
            _globalEventsService.Unsubscribe<IpSelectedEvent>(OnIpSelectedEvent);
            base.OnNavigatedFrom(navigationContext);
        }

        private void OnIpSelectedEvent(IpSelectedEvent ipSelectedEvent)
        {
            SelectedIpAddressViewModel.FullIp = ipSelectedEvent.Ip;
        }

        public ObservableCollection<IIpAddressViewModel> LastConnectedIps { get; }
        public ICommand ConnectDeviceCommand { get; }
    }
}
