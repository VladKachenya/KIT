using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Infrastructure.Global.Constants;
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
using BISC.Presentation.Infrastructure.Commands;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;

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
        private ILastIpAddressesViewModel _lastConnectedIps;
        private IIpAddressViewModel _selectedIpAddressViewModel;
        private bool _isDeviceConnectionFailed;
        private bool _isConnectionProcess;
        private Timer _failedSatatusHidingTimer;

        public DeviceConnectingViewModel(ICommandFactory commandFactory,
            IDeviceConnectionService deviceConnectionService,
            IGlobalEventsService globalEventsService,
            ITreeManagementService treeManagementService ,
            IBiscProject biscProject,IDeviceLoadingService deviceLoadingService,
            ILastIpAddressesViewModelFactory lastConnectedIpsFactoty)
        {
            _commandFactory = commandFactory;
            _deviceConnectionService = deviceConnectionService;
            _globalEventsService = globalEventsService;
            _treeManagementService = treeManagementService;
            _biscProject = biscProject;
            _deviceLoadingService = deviceLoadingService;
            ConnectDeviceCommand = commandFactory.CreatePresentationCommand(OnConnectDeviceExecute, ()=> !_isConnectionProcess);
            //SelectedIpAddressViewModel = _ipAddressViewModelFactory.GetPingItemViewModel();
            lastConnectedIpsFactoty.BuildLastConnectedIpAdresses(out _selectedIpAddressViewModel, out _lastConnectedIps);
            _failedSatatusHidingTimer = new Timer(FailStatusHide,null,Timeout.Infinite,Timeout.Infinite);
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
                await SelectedIpAddressViewModel.PingAsync();
                if (SelectedIpAddressViewModel.IsPingSuccess == false) return;
                var connectResult = await _deviceConnectionService.ConnectDevice(SelectedIpAddressViewModel.FullIp);
                if (connectResult.IsSucceed)
                {
                    await _deviceLoadingService.LoadElements(new List<IDevice>() { connectResult.Item });
                }
                else
                {
                    IsDeviceConnectionFailed = true;
                    _failedSatatusHidingTimer.Change(5000, Timeout.Infinite);
                }
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
