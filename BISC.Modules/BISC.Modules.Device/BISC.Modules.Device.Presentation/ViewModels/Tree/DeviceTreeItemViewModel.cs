using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Events;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Device.Presentation.Interfaces;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Infrastructure.Tree;
using BISC.Modules.Gooses.Infrastructure.Services;

namespace BISC.Modules.Device.Presentation.ViewModels.Tree
{
    public class DeviceTreeItemViewModel : NavigationViewModelBase, IDeviceTreeItemViewModel
    {
        private readonly IDeviceModelService _deviceModelService;
        private readonly IGlobalEventsService _globalEventsService;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly IBiscProject _biscProject;
        private readonly ITreeManagementService _treeManagementService;
        private readonly ITabManagementService _tabManagementService;
        private readonly IGoosesModelService _goosesModelService;

        private string _deviceName;
        private IDevice _device;
        private TreeItemIdentifier _treeItemIdentifier;
        private bool _isDeviceConnected;

        public DeviceTreeItemViewModel(ICommandFactory commandFactory, IDeviceModelService deviceModelService, IGlobalEventsService globalEventsService, IConnectionPoolService connectionPoolService,
            IBiscProject biscProject, ITreeManagementService treeManagementService, ITabManagementService tabManagementService,IGoosesModelService goosesModelService)
        {
            _deviceModelService = deviceModelService;
            _globalEventsService = globalEventsService;
            _connectionPoolService = connectionPoolService;
            _biscProject = biscProject;
            _treeManagementService = treeManagementService;
            _tabManagementService = tabManagementService;
            _goosesModelService = goosesModelService;
            DeleteDeviceCommand = commandFactory.CreatePresentationCommand(OnDeleteDeviceExecute);
            NavigateToDetailsCommand = commandFactory.CreatePresentationCommand(OnNavigateToDetailsExecute);
        }

        private void OnNavigateToDetailsExecute()
        {
            BiscNavigationParameters biscNavigationParameters = new BiscNavigationParameters();
            biscNavigationParameters.AddParameterByName(DeviceKeys.DeviceModelKey, _device);
            _tabManagementService.NavigateToTab(DeviceKeys.DeviceDetailsViewKey, biscNavigationParameters, $"IED {_device.Name}", _treeItemIdentifier);
        }

        public bool IsDeviceConnected
        {
            get => _isDeviceConnected;
            set { SetProperty(ref _isDeviceConnected, value); }
        }

        private void OnDeleteDeviceExecute()
        {
            Dispose();
            var result = _deviceModelService.DeleteDeviceFromModel(_biscProject.MainSclModel.Value, _device);
            if (result.IsSucceed)
            {
                _goosesModelService.DeleteAllDeviceReferencesInGooseControlsInModel(_biscProject.MainSclModel.Value,
                    _device.Name);
                _treeManagementService.DeleteTreeItem(_treeItemIdentifier);
                _connectionPoolService.GetConnection(_device.Ip).StopConnection();
            }
        }

        #region Implementation of IMainTreeItem


        public string DeviceName
        {
            get => _deviceName;
            set
            {
                _deviceName = value;
                OnPropertyChanged();
            }
        }

        public IDevice Device { get; set; }
        #endregion

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            IDevice device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>(DeviceKeys.DeviceModelKey);
            _treeItemIdentifier =
                navigationContext.BiscNavigationParameters.GetParameterByName<TreeItemIdentifier>(TreeItemIdentifier.Key);
            DeviceName = device.Name;
            _device = device;
            IsDeviceConnected = _connectionPoolService.GetConnection(_device.Ip).IsConnected;
            _globalEventsService.Subscribe<ConnectionEvent>(OnConnectionChangedEvent);
            base.OnNavigatedTo(navigationContext);
        }

        private void OnConnectionChangedEvent(ConnectionEvent ea)
        {
            if (ea.Ip == _device.Ip)
            {
                IsDeviceConnected = ea.IsConnected;
            }
        }


        #region Overrides of ViewModelBase

        protected override void OnDisposing()
        {
            base.OnDisposing();
        }

        #endregion

        public ICommand DeleteDeviceCommand { get; }
        public ICommand NavigateToDetailsCommand { get; }
    }
}
