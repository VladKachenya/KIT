using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.Connection.Infrastructure.Events;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Connection.Presentation.Interfaces.Factorys;
using BISC.Modules.Connection.Presentation.Interfaces.ViewModel;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Device.Presentation.Interfaces;
using BISC.Modules.Device.Presentation.Interfaces.Services;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using BISC.Infrastructure.Global.Common;
using BISC.Infrastructure.Global.Logging;
using BISC.Presentation.BaseItems.Common;
using BISC.Presentation.Infrastructure.Commands;
using BISC.Presentation.Infrastructure.Keys;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Device.Presentation.ViewModels
{
    public class DeviceDetailsViewModel : NavigationViewModelBase, IDeviceDetailsViewModel
    {

        private readonly ISclCommunicationModelService _sclCommunicationModel;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly IBiscProject _biscProject;
        private readonly IIpAddressViewModelFactory _ipAddressViewModelFactory;
        private readonly IGlobalEventsService _globalEventsService;
        private readonly INavigationService _navigationService;
        private readonly IDeviceIdentificationService _deviceIdentificationService;
        private readonly IDeviceIpChangingService _deviceIpChangingService;
        private readonly IUserInteractionService _userInteractionService;
        private readonly IUserInterfaceComposingService _userInterfaceComposingService;
        private readonly IConfigurationFileService _configurationFileService;
        private readonly ILoggingService _loggingService;
        private readonly ITreeManagementService _treeManagementService;

        private string _deviceName;
        private IDevice _device;
        private UiEntityIdentifier _uiEntityIdentifier;
        private string _deviceIp;
        private bool _isIpUnchangeable;
        private KeyValuePair<string, string> _selectiedConfigView;


        public DeviceDetailsViewModel(
            ISclCommunicationModelService sclCommunicationModel,
            IConnectionPoolService connectionPoolService, 
            IBiscProject biscProject,
            IIpAddressViewModelFactory ipAddressViewModelFactory, 
            IGlobalEventsService globalEventsService,
            ICommandFactory commandFactory,
            INavigationService navigationService,
            IDeviceIdentificationService deviceIdentificationService,
            IDeviceIpChangingService deviceIpChangingService, 
            IUserInteractionService userInteractionService, 
            IUserInterfaceComposingService userInterfaceComposingService,
            IConfigurationFileService configurationFileService,
            ILoggingService loggingService,
            ITreeManagementService treeManagementService)
            : base(globalEventsService)
        {
            _sclCommunicationModel = sclCommunicationModel;
            _connectionPoolService = connectionPoolService;
            _biscProject = biscProject;
            _ipAddressViewModelFactory = ipAddressViewModelFactory;
            _globalEventsService = globalEventsService;
            _navigationService = navigationService;
            _deviceIdentificationService = deviceIdentificationService;
            _deviceIpChangingService = deviceIpChangingService;
            _userInteractionService = userInteractionService;
            _userInterfaceComposingService = userInterfaceComposingService;
            _configurationFileService = configurationFileService;
            _loggingService = loggingService;
            _treeManagementService = treeManagementService;
            ModelRegionKey = Guid.NewGuid().ToString();

            ConfigKeysDictionary = new Dictionary<string, string>()
            {
                {KeysForNavigation.RegionNames.InfoModelTreeItemDetailsViewKey, "Информационная модель" },
                {KeysForNavigation.RegionNames.DataSetsDetailsViewKey, "Наборы данных" },
                {KeysForNavigation.RegionNames.ReportsDetailsViewKey, "Отчёты" },
                {KeysForNavigation.RegionNames.GooseControlsTabViewKey, "GOOSE" },
                {KeysForNavigation.RegionNames.GooseMatrixTabLightKey, "Подписки на GOOSE" }
            };

            ChengeIpCommand = commandFactory.CreatePresentationCommand(OnChengeIpCommand, () => !IsIpUnchangeable);
            SaveConfigurationToFileCommand = commandFactory.CreatePresentationCommand(OnSaveConfigurationToFile);
        }

        public string ModelRegionKey { get; }


        #region public methods

        public Dictionary<string, string> ConfigKeysDictionary { get; private set; }

        public KeyValuePair<string, string> SelectedConfigView
        {
            get => _selectiedConfigView;
            set
            {
                _navigationService.NavigateViewToRegion(value.Key, ModelRegionKey,
                    new BiscNavigationParameters().AddParameterByName("IED", _device)
                        .AddParameterByName(KeysForNavigation.NavigationParameter.IsFromDeviceDetails, true)
                        .AddParameterByName(KeysForNavigation.NavigationParameter.IsReadOnly, true)
                        .AddParameterByName(UiEntityIdentifier.Key, _uiEntityIdentifier));
                SetProperty(ref _selectiedConfigView, value, true);
            }
        }

        public bool IsBemnManufacturer { get; protected set; }
        public string DeviceName
        {
            get => _deviceName;
            set { SetProperty(ref _deviceName, value); }
        }

        public bool IsIpUnchangeable
        {
            get => _isIpUnchangeable;
            set
            {
                SetProperty(ref _isIpUnchangeable, value, true);
                (ChengeIpCommand as IPresentationCommand).RaiseCanExecute();
            }
        }

        public IIpAddressViewModel IpAddressViewModel { get; protected set; }

        public ICommand ChengeIpCommand { get; }
        public ICommand SaveConfigurationToFileCommand { get; }


        #endregion

        #region override methods
        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>(DeviceKeys.DeviceModelKey);
            _uiEntityIdentifier = navigationContext.BiscNavigationParameters
                .GetParameterByName<UiEntityIdentifier>(UiEntityIdentifier.Key);
            DeviceName = _device.Name;
            IsBemnManufacturer = (_device.Manufacturer == DeviceKeys.DeviceManufacturer.BemnManufacturer);
            string ip = _sclCommunicationModel.GetIpOfDevice(_device.Name, _biscProject.MainSclModel.Value);

            IsIpUnchangeable = _connectionPoolService.GetConnection(ip).IsConnected || !IsBemnManufacturer;
            IpAddressViewModel = string.IsNullOrWhiteSpace(ip) ? _ipAddressViewModelFactory.GetPingItemViewModel(isReadonly: IsIpUnchangeable) :
                _ipAddressViewModelFactory.GetPingItemViewModel(ip, IsIpUnchangeable);

            SelectedConfigView = ConfigKeysDictionary.First();

            _globalEventsService.Subscribe<LossConnectionEvent>(OnLostConnectionEvent);
            base.OnNavigatedTo(navigationContext);
        }

        public override void OnActivate()
        {
            _userInterfaceComposingService.AddGlobalCommand(SaveConfigurationToFileCommand, 
                $"Сохранить конфигурацию устройства {_device.Name} в файл", IconsKeys.FileImportIcon, false, true);
            base.OnActivate();
        }

        public override void OnDeactivate()
        {
            _userInterfaceComposingService.DeleteGlobalCommand(SaveConfigurationToFileCommand);
            base.OnDeactivate();
        }

        protected override void OnDisposing()
        {
            _globalEventsService.Unsubscribe<LossConnectionEvent>(OnLostConnectionEvent);
            _navigationService.DisposeRegionViewModel(ModelRegionKey);
            base.OnDisposing();
        }
        #endregion

        #region private methods

        private async void OnSaveConfigurationToFile()
        {
            Maybe<string> listOfPaths = FileHelper.SelectFilePathToSave(
                "Сохранение файла", ".conf", 
                "Config files (*.conf)|*.conf| All Files (*.*)|*.*", $"{_device.Name} config");
            if (!listOfPaths.Any())
            {
                return;
            }

            FileInfo savingFile;
            try
            {
                savingFile = new FileInfo(listOfPaths.GetFirstValue());
            }
            catch
            {
                _loggingService.LogMessage($"Возникла ошибка при пути {listOfPaths.GetFirstValue()}\nКонфигурация не может быть сохранена!", SeverityEnum.Warning);
                return;
            }

            if (savingFile.Exists)
            {
                savingFile.Delete();
            }

            var res = await _configurationFileService.SaveConfigurationToFile(_biscProject.MainSclModel.Value, _device,
                listOfPaths.GetFirstValue());

            if (res.IsSucceed)
            {
                _loggingService.LogMessage($"Конфигурация устройства {_device.Name} успешно сохранена в файл {listOfPaths.GetFirstValue()}", SeverityEnum.Info);
            }
            else
            {
                _loggingService.LogMessage($"Возникла ошибка при сохранении конфигурации устройства {_device.Name} в файл по пути {listOfPaths.GetFirstValue()}", SeverityEnum.Warning);
            }
        }

        private void OnLostConnectionEvent(LossConnectionEvent lossConnectionEvent)
        {
            if (!IsBemnManufacturer) { return; }
            if (lossConnectionEvent.Ip == _device.Ip)
            {
                IsIpUnchangeable = _connectionPoolService.GetConnection(_device.Ip).IsConnected || !IsBemnManufacturer;

                (IpAddressViewModel as ComplexViewModelBase)?.SetIsEditable(!_connectionPoolService.GetConnection(_device.Ip).IsConnected && IsBemnManufacturer);
            }
        }

        private async void OnChengeIpCommand()
        {
            try
            {
                _deviceIpChangingService.ChengeDeviceIp(_device, IpAddressViewModel.FullIp,
                    _treeManagementService.GetParentDeviceUiIdentifierOfDefault(_uiEntityIdentifier));
            }
            catch (Exception e)
            {
                if (e is ArgumentException)
                {
                    await _userInteractionService.ShowOptionToUser("Ошибка смены IP устройства", e.Message,
                        new List<string>() { "Ок" });
                }
            }
        }


        #endregion

    }
}
