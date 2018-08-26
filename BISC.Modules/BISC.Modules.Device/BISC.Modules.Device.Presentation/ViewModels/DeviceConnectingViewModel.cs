using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Connection.Presentation.Events;
using BISC.Modules.Connection.Presentation.Interfaces.Factorys;
using BISC.Modules.Connection.Presentation.Interfaces.ViewModel;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Loading;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Device.Presentation.Interfaces;
using BISC.Presentation.BaseItems.Commands;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Device.Presentation.ViewModels
{
    public class DeviceConnectingViewModel : NavigationViewModelBase, IDeviceConnectingViewModel
    {
        private readonly ICommandFactory _commandFactory;
        private readonly IDeviceConnectionService _deviceConnectionService;
        private readonly IConfigurationService _configurationService;
        private readonly IGlobalEventsService _globalEventsService;
        private readonly IIpAddressViewModelFactory _ipAddressViewModelFactory;
        private readonly ITreeManagementService _treeManagementService;
        private readonly IBiscProject _biscProject;
        private IIpAddressViewModel _selectedIpAddressViewModel;
        private bool _isDeviceConnectionFailed;
        private Timer _failedSatatusHidingTimer;
        public DeviceConnectingViewModel(ICommandFactory commandFactory,
            IDeviceConnectionService deviceConnectionService,
            IConfigurationService configurationService, IGlobalEventsService globalEventsService,
            IIpAddressViewModelFactory ipAddressViewModelFactory,
            ITreeManagementService treeManagementService ,
            IBiscProject biscProject)
        {
            _commandFactory = commandFactory;
            _deviceConnectionService = deviceConnectionService;
            _configurationService = configurationService;
            _globalEventsService = globalEventsService;
            _ipAddressViewModelFactory = ipAddressViewModelFactory;
            _treeManagementService = treeManagementService;
            _biscProject = biscProject;
            LastConnectedIps = new ObservableCollection<IIpAddressViewModel>();
            ConnectDeviceCommand = commandFactory.CreatePresentationCommand(OnConnectDeviceExecute);
            var lastConnectedIp = _configurationService.LastConnectedIpAddresses.Count > 0
                ? _configurationService.LastConnectedIpAddresses[0]
                : "...";
            SelectedIpAddressViewModel = _ipAddressViewModelFactory.GetPingItemViewModel(lastConnectedIp, false);
            _failedSatatusHidingTimer=new Timer(FailStatusHide,null,Timeout.Infinite,Timeout.Infinite);
            LastConnectedIps =
                _ipAddressViewModelFactory.GetPingViewModelReadonlyCollection(_configurationService
                    .LastConnectedIpAddresses);

        }

        private async void OnConnectDeviceExecute()
        {
            IsDeviceConnectionFailed = false;
            var connectResult = await _deviceConnectionService.ConnectDevice(SelectedIpAddressViewModel.FullIp);

            if (connectResult.IsSucceed)
            {               
                BiscNavigationParameters biscNavigationParameters = new BiscNavigationParameters();
                biscNavigationParameters.AddParameterByName(DeviceKeys.DeviceModelKey, connectResult.Item);
                DialogCommands.CloseDialogCommand.Execute(null, null);
                _treeManagementService.AddTreeItem(biscNavigationParameters, DeviceKeys.DeviceLoadingTreeItemViewKey,
                    null);
               
            }
            else
            {
                IsDeviceConnectionFailed = true;
                _failedSatatusHidingTimer.Change(2000, Timeout.Infinite);
            }
        }

        private void FailStatusHide(object o)
        {

            IsDeviceConnectionFailed = false;
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

        public bool IsDeviceConnectionFailed
        {
            get => _isDeviceConnectionFailed;
            set { SetProperty(ref _isDeviceConnectionFailed, value); }
        }
    }
}
