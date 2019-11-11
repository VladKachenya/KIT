using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Events;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Events;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Saving;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Device.Presentation.Interfaces;
using BISC.Modules.Device.Presentation.Services.Helpers;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Infrastructure.Tree;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.FTP.Infrastructure.Serviсes;
using BISC.Presentation.BaseItems.Common;
using BISC.Presentation.Infrastructure.Commands;
using BISC.Presentation.Infrastructure.Events;
using BISC.Presentation.Infrastructure.HelperEntities;

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
        private readonly IDeviceReconnectionService _deviceReconnectionService;
        private readonly IDeviceConnectionService _deviceConnectionService;
        private readonly INavigationService _navigationService;
        private readonly IDeviceSavingService _deviceSavingService;
        private readonly IGlobalSavingService _globalSavingService;
        private readonly IProjectManagementService _projectManagementService;
        private Dispatcher _dispatcher;

        private string _deviceName;
        private IDevice _device;
        private UiEntityIdentifier _configEntityIdentifier;
        private UiEntityIdentifier _detailsEntityIdentifier;
        private UiEntityIdentifier _uiEntityIdentifier;

        private bool _isDeviceConnected;
        private bool _isReportWarning;

        public DeviceTreeItemViewModel(
            ICommandFactory commandFactory,
            IDeviceModelService deviceModelService,
            IGlobalEventsService globalEventsService,
            IConnectionPoolService connectionPoolService,
            IBiscProject biscProject,
            ITreeManagementService treeManagementService,
            ITabManagementService tabManagementService,
            IGoosesModelService goosesModelService,
            ISaveCheckingService saveCheckingService,
            IUserInteractionService userInteractionService,
            ILoggingService loggingService,
            IDeviceSerializingService deviceSerializingService,
            IDeviceWarningsService deviceWarningsService,
            IDeviceReconnectionService deviceReconnectionService,
            IDeviceConnectionService deviceConnectionService,
            INavigationService navigationService,
            IDeviceSavingService deviceSavingService,
            IGlobalSavingService globalSavingService,
            IProjectManagementService projectManagementService)
            : base(null)
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
            _deviceReconnectionService = deviceReconnectionService;
            _deviceConnectionService = deviceConnectionService;
            _navigationService = navigationService;
            _deviceSavingService = deviceSavingService;
            _globalSavingService = globalSavingService;
            _projectManagementService = projectManagementService;
            DeleteDeviceCommand = commandFactory.CreatePresentationCommand(OnDeleteDeviceExecute);
            NavigateToDetailsCommand = commandFactory.CreatePresentationCommand(OnNavigateToDetailsExecute);
            ResetDeviceViaFtpCommand = commandFactory.CreatePresentationCommand(OnResetDeviceViaFtp, IsDeviceReadyForFtpOps);
            NavigateToConfigCommand = commandFactory.CreatePresentationCommand(OnNavigateToConfig, IsDeviceReadyForFtpOps);
            ExportCidDeviceCommand = commandFactory.CreatePresentationCommand(OnExportCidDevice);
            DisconnectDeviceCommand = commandFactory.CreatePresentationCommand(OnDisconnectDevice, CanDisconnectDevice);
            ConnectDeviceCommand = commandFactory.CreatePresentationCommand(OnConnectDevice, CanConnectDevice);
            SaveDeviceChangesCommand = commandFactory.CreatePresentationCommand(OnSaveDeviceChanges, IsDeviceReadyForFtpOps);
        }

        private void OnNavigateToConfig()
        {
            BiscNavigationParameters biscNavigationParameters = new BiscNavigationParameters();
            biscNavigationParameters.AddParameterByName(DeviceKeys.DeviceModelKey, _device);
            _tabManagementService.NavigateToTab(DeviceKeys.DeviceConfigViewKey, biscNavigationParameters, $"IED Config {_device.Name}", _configEntityIdentifier);
        }

        private async void OnSaveDeviceChanges()
        {
            await _globalSavingService.SaveСhangesOfDevice(_device.DeviceGuid);
            var res = await _userInteractionService.ShowOptionToUser("Запись данных в устройство",
            $"Вы уверены что хотите записать данные из проекта в устройство {_device.Name}?" +
            $"\nПосле записи, модуль связи {_device.Name} будет перезагружен", new List<string> { "OK", "Отмена" });
            if (res == 1) return;

            _globalEventsService.SendMessage(new ShellBlockEvent() { IsBlocked = true, Message = $"Сохранение конфигурационных данных в устройство {_device.Name}" });

            try
            {

                await _deviceSavingService.SaveAllDeviceElements(_device);
            }
            finally
            {
                _globalEventsService.SendMessage(new ShellBlockEvent() { IsBlocked = false });
                await _deviceReconnectionService.RestartDevice(_device);

            }


        }
        private void OnConnectDevice()
        {
            _navigationService.NavigateViewToGlobalRegion(DeviceKeys.ReconnectDeviceViewKey,
                new BiscNavigationParameters().AddParameterByName(DeviceKeys.ReconnectDeviceContextKey,
                    new ReconnectDeviceContext(_device, _uiEntityIdentifier)));
        }

        private bool CanConnectDevice()
        {
            return !IsDeviceConnected && _device.Manufacturer == DeviceKeys.DeviceManufacturer.BemnManufacturer;
        }

        private bool CanDisconnectDevice()
        {
            return IsDeviceConnected;
        }

        private async void OnDisconnectDevice()
        {
            await _deviceConnectionService.DisconnectDevice(_device.Ip);
        }

        private void OnExportCidDevice()
        {
            var filePath = FileHelper.SelectFilePathToSave($"Сохранение устройства {_device.Name} в файл", ".cid", "Cid SCL files (*.cid)|*.cid",
                 $"{_device.Name}.cid");
            if (filePath.Any())
            {
                _deviceSerializingService.SerializeCidSingleDevice(_device, filePath.GetFirstValue());
                _loggingService.LogMessage($"Модель устройства {_device.Name} записана в файл {filePath.GetFirstValue()}", SeverityEnum.Info);
            }
        }

        private async void OnResetDeviceViaFtp()
        {
            var res = await _userInteractionService.ShowOptionToUser("Перезагрузка устройства",
                $"Устройство {_device.Name} \n будет перезагружено", new List<string> { "OK", "Отмена" });
            if (res == 1) return;
            ResetDeviceByFtp();
        }

        private async void ResetDeviceByFtp()
        {
            _loggingService.LogMessage($"Устройство {_device.Name} перезагружается", SeverityEnum.Info);

            await _deviceReconnectionService.RestartDevice(_device, _uiEntityIdentifier);
        }

        private bool IsDeviceReadyForFtpOps()
        {
            if (!IsDeviceConnected) return false;
            if (_device.Manufacturer != DeviceKeys.DeviceManufacturer.BemnManufacturer) return false;
            return true;
        }

        private void OnNavigateToDetailsExecute()
        {
            BiscNavigationParameters biscNavigationParameters = new BiscNavigationParameters();
            biscNavigationParameters.AddParameterByName(DeviceKeys.DeviceModelKey, _device);
            _tabManagementService.NavigateToTab(DeviceKeys.DeviceDetailsViewKey, biscNavigationParameters, $"IED {_device.Name}", _detailsEntityIdentifier);
        }

        public bool IsDeviceConnected
        {
            get => _isDeviceConnected;
            set
            {
                SetProperty(ref _isDeviceConnected, value);
                _dispatcher.Invoke(() => (ResetDeviceViaFtpCommand as IPresentationCommand)?.RaiseCanExecute());
            }
        }

        private void OnDeviceWarningsChanged(DeviceWarningsChanged deviceWarningsChanged)
        {
            if (deviceWarningsChanged.DeviceGuid != _device.DeviceGuid) return;
            WarningsCollection.Clear();
            if (_deviceWarningsService.GetIsDeviceWarningRegistered(_device.DeviceGuid))
            {
                List<string> warningList =
                    _deviceWarningsService.GetWarningMassagesOfDevice(deviceWarningsChanged.DeviceGuid);
                if (warningList == null) return;
                IsReportWarning = true;
                warningList.ForEach(warningMassage => WarningsCollection.Add(warningMassage));
            }
        }

        private async void OnDeleteDeviceExecute()
        {
            Dispose();
            var isSaved = (await _saveCheckingService.GetIsDeviceEntitiesSaved(_device.DeviceGuid)).IsEntitiesSaved;
            if (!isSaved)
            {
                var res = await _userInteractionService.ShowOptionToUser("Несохраненные изменения",
                    "В устройстве имеются несохраненные изменения." + Environment.NewLine + "Все равно удалить?",
                    new List<string>() { "Удалить", "Отмена" });
                if (res == 1)
                {
                    return;
                }
            }

            _loggingService.LogUserAction($"Удаление устройства {_device.Name}");
            _projectManagementService.DeleteDeviceFromProject(_device.DeviceGuid);
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
            _uiEntityIdentifier =
                navigationContext.BiscNavigationParameters.GetParameterByName<UiEntityIdentifier>(UiEntityIdentifier.Key);
            _configEntityIdentifier = new UiEntityIdentifier(Guid.NewGuid(), _uiEntityIdentifier);
            _detailsEntityIdentifier = new UiEntityIdentifier(Guid.NewGuid(), _uiEntityIdentifier);
            _deviceWarningsService.ClearDeviceWarningsOfDevice(device.DeviceGuid);
            DeviceName = device.Name;
            _device = device;
            IsDeviceConnected = _connectionPoolService.GetConnection(_device.Ip).IsConnected;
            _globalEventsService.Subscribe<ConnectionEvent>(OnConnectionChangedEvent);
            _globalEventsService.Subscribe<ResetByFtpEvent>(OnResetByFtpEvent);
            _globalEventsService.Subscribe<DeviceWarningsChanged>(OnDeviceWarningsChanged);

            _globalEventsService.SendMessage(new DeviceWarningsChanged(_device.DeviceGuid));
            base.OnNavigatedTo(navigationContext);
        }

        private void OnConnectionChangedEvent(ConnectionEvent ea)
        {
            if (ea.Ip == _device.Ip)
            {
                IsDeviceConnected = ea.IsConnected;
                (DisconnectDeviceCommand as IPresentationCommand)?.RaiseCanExecute();
                (ConnectDeviceCommand as IPresentationCommand)?.RaiseCanExecute();
                (SaveDeviceChangesCommand as IPresentationCommand)?.RaiseCanExecute();

            }
        }

        private void OnResetByFtpEvent(ResetByFtpEvent ea)
        {
            if (ea.Ip == _device.Ip && ea.DeviceGuid == _device.DeviceGuid && IsDeviceConnected)
            {
                ResetDeviceByFtp();
            }
        }


        #region Overrides of ViewModelBase

        protected override void OnDisposing()
        {
            _globalEventsService.Unsubscribe<ConnectionEvent>(OnConnectionChangedEvent);
            _globalEventsService.Unsubscribe<ResetByFtpEvent>(OnResetByFtpEvent);
            _globalEventsService.Unsubscribe<DeviceWarningsChanged>(OnDeviceWarningsChanged);
            base.OnDisposing();
        }

        #endregion
        public ICommand DisconnectDeviceCommand { get; }
        public ICommand ConnectDeviceCommand { get; }
        public ICommand DeleteDeviceCommand { get; }
        public ICommand NavigateToDetailsCommand { get; }
        public ICommand ResetDeviceViaFtpCommand { get; }
        public ICommand NavigateToConfigCommand { get; }
        public ICommand SaveDeviceChangesCommand { get; }

        public ICommand ExportCidDeviceCommand { get; }

    }
}
