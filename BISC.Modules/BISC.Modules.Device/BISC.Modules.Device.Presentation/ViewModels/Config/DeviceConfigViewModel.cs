using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Presentation.BaseItems.Annotations;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.BaseItems.ViewModels.Behaviors;
using BISC.Presentation.Infrastructure.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Modules.Device.Infrastructure.Model.Config;
using BISC.Modules.Device.Presentation.Interfaces.UserControls;
using BISC.Presentation.Infrastructure.Commands;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Device.Presentation.ViewModels.Config
{
    public class DeviceConfigViewModel : NavigationViewModelBase
    {
        private readonly ISclCommunicationModelService _sclCommunicationModelService;
        private readonly IDeviceFtpConfigService _deviceFtpConfigService;
        private readonly ILoggingService _loggingService;
        private readonly IUserInterfaceComposingService _userInterfaceComposingService;
        private readonly IUserInteractionService _userInteractionService;
        private readonly IProjectManagementService _projectManagementService;
        private readonly Func<IDeviceTechnicalKeyViewModel> _deviceTechnicalKeyViewModelFactory;
        private string _staticMacAddress;
        private IDevice _device;
        private string _switchMode;
        private string _version;
        private IDeviceTechnicalKeyViewModel _techKeyViewModelModel;
        private MacFiltersViewModel _macFilterAViewModel;
        private MacFiltersViewModel _macFilterBViewModel;
        private IDeviceFtpConfig _deviceConfig;

        public DeviceConfigViewModel(
            [CanBeNull] ISclCommunicationModelService sclCommunicationModelService,
            Func<MacFiltersViewModel> macFiltersViewModelFunc,
            IDeviceFtpConfigService deviceFtpConfigService,
            ILoggingService loggingService,
            ICommandFactory commandFactory,
            IUserInterfaceComposingService userInterfaceComposingService,
            IGlobalEventsService globalEventsService,
            IUserInteractionService userInteractionService,
            IProjectManagementService projectManagementService,
            Func<IDeviceTechnicalKeyViewModel> deviceTechnicalKeyViewModelFactory)
            : base(globalEventsService)
        {
            _sclCommunicationModelService = sclCommunicationModelService;
            _deviceFtpConfigService = deviceFtpConfigService;
            _loggingService = loggingService;
            _userInterfaceComposingService = userInterfaceComposingService;
            _userInteractionService = userInteractionService;
            _projectManagementService = projectManagementService;
            _deviceTechnicalKeyViewModelFactory = deviceTechnicalKeyViewModelFactory;
            UpdateConfigCommand = commandFactory.CreatePresentationCommand(OnLoadConfig, CanExecuteLoadConfig);
            SwitchModeList = new List<string>(new[] { "HSR", "PRP", "NORMAL", "BLOCK" });
            MacFilterAViewModel = macFiltersViewModelFunc();
            MacFilterBViewModel = macFiltersViewModelFunc();
            BlockViewModelBehavior = new BlockViewModelBehavior();
            SaveConfigCommand = commandFactory.CreatePresentationCommand(OnSave, CanExecuteLoadConfig);
        }

        private async void OnSave()
        {
            if(!TechKeyViewModel.IsValid) return;
            var res = await _userInteractionService.ShowOptionToUser("Запись файла конфигурации устройства",
                "После записи файла конфигурации устройство будет удалено из проекта" + Environment.NewLine + "Желаете записать файл конфигурации?",
                new List<string>() { "Ок", "Отмена" });
            if (res == 1)
            {
                return;
            }
            BlockViewModelBehavior.SetBlock("Сохранение Config", true);
            await _deviceFtpConfigService.SaveDeviceFtpConfig(_device.Ip, GetResultedConfig());
            BlockViewModelBehavior.Unlock();
            (SaveConfigCommand as IPresentationCommand)?.RaiseCanExecute();
            (UpdateConfigCommand as IPresentationCommand)?.RaiseCanExecute();
            _projectManagementService.DeleteDeviceFromProject(_device.DeviceGuid);
        }

        private IDeviceFtpConfig GetResultedConfig()
        {
            _deviceConfig.FilterAMacList = MacFilterAViewModel.GetResultList();
            _deviceConfig.FilterBMacList = MacFilterBViewModel.GetResultList();
            _deviceConfig.MacAddress = StaticMacAddress;
            _deviceConfig.TechKey = TechKeyViewModel.TechKey;
            _deviceConfig.Version = Version;
            _deviceConfig.SwitchMode = SwitchMode;
            return _deviceConfig;
        }

        private bool CanExecuteLoadConfig()
        {
            return !BlockViewModelBehavior.IsBlocked;
        }

        private async void OnLoadConfig()
        {
            await LoadAsync();
        }


        public string StaticMacAddress
        {
            get => _staticMacAddress;
            set => SetProperty(ref _staticMacAddress, value);
        }

        public string SwitchMode
        {
            get => _switchMode;
            set => SetProperty(ref _switchMode, value);
        }

        public string Version
        {
            get => _version;
            set => SetProperty(ref _version, value);
        }

        public IDeviceTechnicalKeyViewModel TechKeyViewModel
        {
            get => _techKeyViewModelModel;
            set => SetProperty(ref _techKeyViewModelModel, value);
        }
        public List<string> SwitchModeList { get; }

        public MacFiltersViewModel MacFilterAViewModel
        {
            get => _macFilterAViewModel;
            set => SetProperty(ref _macFilterAViewModel, value);
        }

        public MacFiltersViewModel MacFilterBViewModel
        {
            get => _macFilterBViewModel;
            set => SetProperty(ref _macFilterBViewModel, value);
        }

        #region Overrides of NavigationViewModelBase

        private async Task LoadAsync()
        {
            BlockViewModelBehavior.SetBlock("Загрузка", true);

            try
            {
                var deviceCfgResult = await _deviceFtpConfigService.ReadDeviceFtpConfig(_device.Ip);
                if (deviceCfgResult.IsSucceed)
                {
                    TechKeyViewModel = _deviceTechnicalKeyViewModelFactory();
                    _deviceConfig = deviceCfgResult.Item;
                    StaticMacAddress = deviceCfgResult.Item.MacAddress;
                    Version = deviceCfgResult.Item.Version;
                    TechKeyViewModel.SetTechKey(deviceCfgResult.Item.TechKey);
                    SwitchMode = deviceCfgResult.Item.SwitchMode;
                    MacFilterAViewModel.SetResultList(deviceCfgResult.Item.FilterAMacList, "FILTERA");
                    MacFilterBViewModel.SetResultList(deviceCfgResult.Item.FilterBMacList, "FILTERB");
                }
                else
                {
                    _loggingService.LogMessage(deviceCfgResult.ErrorList.FirstOrDefault(), SeverityEnum.Critical);
                    BlockViewModelBehavior.SetBlock(
                        $"Ошибка загрузки: {Environment.NewLine} {deviceCfgResult.GetFirstError()}", false);
                }

            }
            catch (Exception e)
            {
                _loggingService.LogException(e);
                BlockViewModelBehavior.SetBlock("Ошибка загрузки", false);
            }
            finally
            {
                BlockViewModelBehavior.Unlock();
                (SaveConfigCommand as IPresentationCommand)?.RaiseCanExecute();
                (UpdateConfigCommand as IPresentationCommand)?.RaiseCanExecute();
            }
        }
        protected override async void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _device =
                    navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>(DeviceKeys.DeviceModelKey);
            await LoadAsync();
            base.OnNavigatedTo(navigationContext);
        }

        public override void OnActivate()
        {
            _userInterfaceComposingService.AddGlobalCommand(UpdateConfigCommand, $"Обновить Config { _device.Name}", IconsKeys.UpdateIconKey, false, true);
            _userInterfaceComposingService.SetCurrentSaveCommand(SaveConfigCommand, $"Сохранить Config { _device.Name}", true);

            base.OnActivate();
        }

        public override void OnDeactivate()
        {
            _userInterfaceComposingService.DeleteGlobalCommand(UpdateConfigCommand);
            _userInterfaceComposingService.ClearCurrentSaveCommand();
            base.OnDeactivate();
        }
        public ICommand SaveConfigCommand { get; }
        public ICommand UpdateConfigCommand { get; }

        #endregion
    }
}
