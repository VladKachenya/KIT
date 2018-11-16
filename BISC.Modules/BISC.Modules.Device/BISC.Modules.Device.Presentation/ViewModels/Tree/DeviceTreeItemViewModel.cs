using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using BISC.Infrastructure.Global.Logging;
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
using BISC.Modules.FTP.Infrastructure.Serviсes;
using BISC.Presentation.BaseItems.Common;
using BISC.Presentation.Infrastructure.Commands;

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
        private readonly ISaveCheckingService _saveCheckingService;
        private readonly IUserInteractionService _userInteractionService;
        private readonly ILoggingService _loggingService;
        private readonly IDeviceSerializingService _deviceSerializingService;
        private readonly IDeviceWarningsService _deviceWarningsService;
        private readonly IDeviceRestartService _deviceRestartService;
        private readonly IDeviceManufacturerService _deviceManufacturerService;
        private Dispatcher _dispatcher;

        private string _deviceName;
        private IDevice _device;
        private TreeItemIdentifier _treeItemIdentifier;
        private bool _isDeviceConnected;

        public DeviceTreeItemViewModel(ICommandFactory commandFactory, IDeviceModelService deviceModelService, IGlobalEventsService globalEventsService, IConnectionPoolService connectionPoolService,
            IBiscProject biscProject, ITreeManagementService treeManagementService, ITabManagementService tabManagementService,
            IGoosesModelService goosesModelService,ISaveCheckingService saveCheckingService,IUserInteractionService userInteractionService,ILoggingService loggingService,
            IDeviceSerializingService deviceSerializingService,IDeviceWarningsService deviceWarningsService, IDeviceRestartService deviceRestartService,
            IDeviceManufacturerService deviceManufacturerService)
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
            _deviceModelService = deviceModelService;
            _globalEventsService = globalEventsService;
            _connectionPoolService = connectionPoolService;
            _biscProject = biscProject;
            _treeManagementService = treeManagementService;
            _tabManagementService = tabManagementService;
            _goosesModelService = goosesModelService;
            _saveCheckingService = saveCheckingService;
            _userInteractionService = userInteractionService;
            _loggingService = loggingService;
            _deviceSerializingService = deviceSerializingService;
            _deviceWarningsService = deviceWarningsService;
            _deviceRestartService = deviceRestartService;
            _deviceManufacturerService = deviceManufacturerService;
            DeleteDeviceCommand = commandFactory.CreatePresentationCommand(OnDeleteDeviceExecute);
            NavigateToDetailsCommand = commandFactory.CreatePresentationCommand(OnNavigateToDetailsExecute);
            ResetDeviceViaFtpCommand = commandFactory.CreatePresentationCommand(OnResetDeviceViaFtp, IsResetDeviceViaFtp);
            ExportCidDeviceCommand = commandFactory.CreatePresentationCommand(OnExportCidDevice);
        }

        private void OnExportCidDevice()
        {
           var filePath= FileHelper.SelectFilePathToSave($"Сохранение устройства {_device.Name} в файл", ".cid", "Cid SCL files (*.cid)|*.cid",
                $"BISC_{_device.Name}.cid");
            if (filePath.Any())
            {
                _deviceSerializingService.SerializeCidSingleDevice(_device, filePath.GetFirstValue());
                _loggingService.LogMessage($"Модель устройства {_device.Name} записана в файл {filePath.GetFirstValue()}",SeverityEnum.Info);
            }
        }

        private async void OnResetDeviceViaFtp()
        {
            var res = await _userInteractionService.ShowOptionToUser("Перезагрузка устройства",
                $"Устройство {_device.Name} будет перезагружено", new List<string> { "Ok", "Отмена" });
            if(res == 1)
                return;
            _loggingService.LogMessage($"Устройство {_device.Name} перезагружается",SeverityEnum.Info);
           await _deviceRestartService.RestartDevice(_device,_treeItemIdentifier);

        }

        private bool IsResetDeviceViaFtp()
        {
            if (!IsDeviceConnected) return false;
            if (_device.Manufacturer != DeviceKeys.DeviceManufacturer.BemnManufacturer) return false;
            return true;
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
            set
            {
                SetProperty(ref _isDeviceConnected, value);
                _dispatcher.Invoke( () => (ResetDeviceViaFtpCommand as IPresentationCommand)?.RaiseCanExecute());
            }
        }

        private async void OnDeleteDeviceExecute()
        {
            Dispose();
            _loggingService.LogUserAction("Пользователь удаляет устройство "+_device.Name);
            var isSaved=await _saveCheckingService.GetIsDeviceEntitiesSaved(_device.Name);
            if (!isSaved)
            {
              var res= await _userInteractionService.ShowOptionToUser("Несохраненные изменения",
                    "В устройстве имеются несохраненные изменения." + Environment.NewLine + "Все равно удалить?",
                    new List<string>() {"Удалить", "Отмена"});
                if (res == 1)
                {
                    return;
                }
            }
            var result = _deviceModelService.DeleteDeviceFromModel(_biscProject.MainSclModel.Value, _device.Name);
            if (result.IsSucceed)
            {
                _goosesModelService.DeleteAllDeviceReferencesInGooseControlsInModel(_biscProject.MainSclModel.Value,
                    _device.Name);
                
                _treeManagementService.DeleteTreeItem(_treeItemIdentifier);
                _connectionPoolService.GetConnection(_device.Ip).StopConnection();
                _tabManagementService.CloseTabWithChildren(_treeItemIdentifier.ItemId.ToString());
                _deviceWarningsService.ClearDeviceWarningsOfDevice(_device.Name);
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
            if (_device.Manufacturer == null) _deviceManufacturerService.GetManufacturerOfDevice(_device);
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
        public ICommand ResetDeviceViaFtpCommand { get; }

        public ICommand ExportCidDeviceCommand { get; }

    }
}
