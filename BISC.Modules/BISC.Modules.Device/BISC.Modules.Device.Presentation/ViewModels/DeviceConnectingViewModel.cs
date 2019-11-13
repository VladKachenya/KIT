using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.Connection.Presentation.Events;
using BISC.Modules.Connection.Presentation.Interfaces.Factorys;
using BISC.Modules.Connection.Presentation.Interfaces.ViewModel;
using BISC.Modules.Device.Infrastructure.Loading;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Device.Presentation.Interfaces;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Commands;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Infrastructure.Global.Common;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.Device.Presentation.ViewModels
{
    public class DeviceConnectingViewModel : NavigationViewModelBase, IDeviceConnectingViewModel
    {
        private readonly ICommandFactory _commandFactory;
        private readonly IDeviceConnectionService _deviceConnectionService;
        private readonly IGlobalEventsService _globalEventsService;
        private readonly ITreeManagementService _treeManagementService;
        private readonly IBiscProject _biscProject;
        private readonly IDeviceLoadingService _deviceLoadingService;
        private readonly ILoggingService _loggingService;
        private readonly ISclCommunicationModelService _communicationModelService;
        private readonly IDeviceModelService _deviceModelService;
        private readonly IConnectionPoolService _connectionPoolService;
        private ILastIpAddressesViewModel _lastConnectedIps;
        private IIpAddressViewModel _selectedIpAddressViewModel;
        private bool _isDeviceConnectionFailed;
        private bool _isConnectionProcess;
        private Timer _failedSatatusHidingTimer;

        public DeviceConnectingViewModel(ICommandFactory commandFactory,
            IDeviceConnectionService deviceConnectionService,
            IGlobalEventsService globalEventsService,
            ITreeManagementService treeManagementService,
            IBiscProject biscProject, IDeviceLoadingService deviceLoadingService,
            ILastIpAddressesViewModelFactory lastConnectedIpsFactoty, ILoggingService loggingService,
            ISclCommunicationModelService communicationModelService, IDeviceModelService deviceModelService,
            IConnectionPoolService connectionPoolService)
            : base(null)
        {
            _commandFactory = commandFactory;
            _deviceConnectionService = deviceConnectionService;
            _globalEventsService = globalEventsService;
            _treeManagementService = treeManagementService;
            _biscProject = biscProject;
            _deviceLoadingService = deviceLoadingService;
            _loggingService = loggingService;
            _communicationModelService = communicationModelService;
            _deviceModelService = deviceModelService;
            _connectionPoolService = connectionPoolService;
            ConnectDeviceCommand = commandFactory.CreatePresentationCommand(OnConnectDeviceExecute, () => !_isConnectionProcess);
            lastConnectedIpsFactoty.BuildLastConnectedIpAdresses(out _selectedIpAddressViewModel, out _lastConnectedIps);
            _failedSatatusHidingTimer = new Timer(FailStatusHide, null, Timeout.Infinite, Timeout.Infinite);
            IsConnectionProcess = false;
        }

        #region private methods
        private async void OnConnectDeviceExecute()
        {
            IsDeviceConnectionFailed = false;
            IsConnectionProcess = true;
            try
            {
                (ConnectDeviceCommand as IPresentationCommand)?.RaiseCanExecute();
                await SelectedIpAddressViewModel.PingGlobalEventAsync();
                if (SelectedIpAddressViewModel.IsPingSuccess == false || _connectionPoolService.GetIsDeviceConnect(SelectedIpAddressViewModel.FullIp))
                {
                    return;
                }

                if (_deviceModelService.GetDevicesFromModel(_biscProject.MainSclModel.Value).Any(d =>
                    _communicationModelService.GetIpOfDevice(d.Name, _biscProject.MainSclModel.Value) ==
                    SelectedIpAddressViewModel.FullIp))
                {
                    _loggingService.LogMessage($"Устройство с IP {SelectedIpAddressViewModel.FullIp} уже имеется в проектеп"
                        , SeverityEnum.Warning);
                    return;
                }

                var connectResult = await _deviceConnectionService.ConnectDevice(SelectedIpAddressViewModel.FullIp);


                if (connectResult.IsSucceed)
                {
                    var res = await _deviceLoadingService.LoadElements(connectResult.Item);
                    if (!res.IsSucceed)
                    {
                        _loggingService.LogMessage(res.GetFirstError(), SeverityEnum.Warning);
                    }
                }
                else
                {
                    IsDeviceConnectionFailed = true;
                    _failedSatatusHidingTimer.Change(5000, Timeout.Infinite);
                }
                _loggingService.LogMessage($"Устройство {connectResult.Item.Name} c IP {connectResult.Item.Ip} подключено!", SeverityEnum.Info);
            }
            finally
            {
                IsConnectionProcess = false;
                (ConnectDeviceCommand as IPresentationCommand)?.RaiseCanExecute();
            }

        }

        private void FailStatusHide(object o)
        {
            IsDeviceConnectionFailed = false;
        }

        private void OnIpSelectedEvent(IpSelectedEvent ipSelectedEvent)
        {
            SelectedIpAddressViewModel.FullIp = ipSelectedEvent.Ip;
        }
        #endregion

        #region protected methods

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

        #endregion

        #region Implementation of IDeviceConnectingViewModel

        public IIpAddressViewModel SelectedIpAddressViewModel
        {
            get => _selectedIpAddressViewModel;
            set { SetProperty(ref _selectedIpAddressViewModel, value); }
        }

        public ILastIpAddressesViewModel LastConnectedIps
        {
            get => _lastConnectedIps;
        }
        public ICommand ConnectDeviceCommand { get; }

        public bool IsDeviceConnectionFailed
        {
            get => _isDeviceConnectionFailed;
            set { SetProperty(ref _isDeviceConnectionFailed, value); }
        }

        public bool IsConnectionProcess
        {
            get => _isConnectionProcess;
            set => SetProperty(ref _isConnectionProcess, value);
        }
        #endregion
    }
}
