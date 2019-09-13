using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Model.Services;
using BISC.Modules.Gooses.Presentation.Commands;
using BISC.Modules.Gooses.Presentation.Interfaces.Factories;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.BaseItems.ViewModels.Behaviors;
using BISC.Presentation.Infrastructure.Commands;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Modules.Gooses.Model.Services.LoadingServices;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Tabs
{
    public class GooseSubscriptionTabViewModel : NavigationViewModelBase
    {
        private readonly IDeviceModelService _deviceModelService;
        private readonly IBiscProject _biscProject;
        private readonly ICommandFactory _commandFactory;
        private readonly IUserInterfaceComposingService _userInterfaceComposingService;
        private readonly ISaveCheckingService _saveCheckingService;
        private readonly IGlobalSavingService _globalSavingService;
        private readonly IGooseSubscriptionDataTableFactory _dataTableFactory;
        private readonly GooseInputModelInfosLoadingService _gooseInputModelInfosLoadingService;
        private readonly ILoggingService _loggingService;
        private readonly IConnectionPoolService _connectionPoolService;
        private List<IDevice> _devices;
        private DataTable _gooseSubscriptionTable;
        private GooseSubscriptionSavingCommand _gooseSubscriptionSavingCommand;
        private string _regionName;

        private bool _isEneble = true;

        public GooseSubscriptionTabViewModel(
            IDeviceModelService deviceModelService, 
            IBiscProject biscProject, 
            IGoosesModelService goosesModelService,
            ICommandFactory commandFactory, 
            IUserInterfaceComposingService userInterfaceComposingService, 
            ISaveCheckingService saveCheckingService,
            GooseSubscriptionSavingCommand gooseSubscriptionSavingCommand, 
            IGlobalSavingService globalSavingService, 
            IGooseSubscriptionDataTableFactory dataTableFactory,
            GooseInputModelInfosLoadingService gooseInputModelInfosLoadingService, 
            ILoggingService loggingService, 
            IConnectionPoolService connectionPoolService)
            : base(null)
        {
            _deviceModelService = deviceModelService;
            _biscProject = biscProject;
            _commandFactory = commandFactory;
            _userInterfaceComposingService = userInterfaceComposingService;
            _saveCheckingService = saveCheckingService;
            _globalSavingService = globalSavingService;
            _dataTableFactory = dataTableFactory;
            _gooseInputModelInfosLoadingService = gooseInputModelInfosLoadingService;
            _loggingService = loggingService;
            _connectionPoolService = connectionPoolService;
            SaveChangesCommand = _commandFactory.CreatePresentationCommand(OnSaveChanges, () => _isEneble);
            UpdateTableCommand = _commandFactory.CreatePresentationCommand(OnUpdateTable, () => _isEneble);
            CheckChangesCommand = _commandFactory.CreatePresentationCommand(OnCheckChangesCommand);

            _gooseSubscriptionSavingCommand = gooseSubscriptionSavingCommand;
            BlockViewModelBehavior = new BlockViewModelBehavior();
        }

        public ICommand CheckChangesCommand { get; set; }
        private void OnCheckChangesCommand()
        {
            ChangeTracker.SetModified();
        }

        public ICommand UpdateTableCommand { get; set; }

        public ICommand SaveChangesCommand { get; }

        public DataView GooseSubscriptionTable => GooseSubcriptionDataTable?.DefaultView;

        public DataTable GooseSubcriptionDataTable
        {
            get => _gooseSubscriptionTable;
            set
            {
                SetProperty(ref _gooseSubscriptionTable, value);
                _gooseSubscriptionSavingCommand.Initialize(_gooseSubscriptionTable);
                _gooseSubscriptionSavingCommand.RefreshViewModel = async () => await LoadSubscriptionsTable(false);
                OnPropertyChanged(nameof(GooseSubscriptionTable));
            }
        }
        #region Overrides of NavigationViewModelBase

        protected override void OnDisposing()
        {
            _saveCheckingService.RemoveSaveCheckingEntityByOwner(_regionName);
            base.OnDisposing();
        }

        protected override async void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            BlockViewModelBehavior.SetBlock("Загрузка...", true);
            _devices = _deviceModelService.GetDevicesFromModel(_biscProject.MainSclModel.Value);
            _regionName = navigationContext.BiscNavigationParameters
                .GetParameterByName<UiEntityIdentifier>(UiEntityIdentifier.Key).ItemId.ToString();
            await LoadSubscriptionsTable(false);

            base.OnNavigatedTo(navigationContext);
            BlockViewModelBehavior.Unlock();
        }

        public override void OnActivate()
        {
            _userInterfaceComposingService.AddGlobalCommand(UpdateTableCommand, $"Обновить таблицу", IconsKeys.UpdateIconKey, false, true);
            _userInterfaceComposingService.SetCurrentSaveCommand(SaveChangesCommand, "Сохранить подписку GOOSE для всех устройств", false);
            base.OnActivate();
        }

        public override void OnDeactivate()
        {
            _userInterfaceComposingService.ClearCurrentSaveCommand();
            _userInterfaceComposingService.DeleteGlobalCommand(UpdateTableCommand);
            _userInterfaceComposingService.DeleteGlobalCommand(SaveChangesCommand);
            base.OnDeactivate();
        }
        #endregion


        #region private methods

        private void SetCommandsEneble(bool isEneble)
        {
            _isEneble = isEneble;
            (SaveChangesCommand as IPresentationCommand)?.RaiseCanExecute();
            (UpdateTableCommand as IPresentationCommand)?.RaiseCanExecute();
        }

        private async void OnSaveChanges()
        {
            SetCommandsEneble(false);
            try
            {
                BlockViewModelBehavior.SetBlock("Загрузка...", true);
                await _globalSavingService.SaveСhangesToRegion(_regionName);
            }
            finally
            {
                BlockViewModelBehavior.Unlock();
                SetCommandsEneble(true);
            }
        }

        private async void OnUpdateTable()
        {
            SetCommandsEneble(false);
            try
            {

                BlockViewModelBehavior.SetBlock("Загрузка...", true);
                await LoadSubscriptionsTable(false);
            }
            finally
            {
                BlockViewModelBehavior.Unlock();
                SetCommandsEneble(true);
            }
        }

        private async Task LoadSubscriptionsTable(bool updateFromDevice)
        {

            if (updateFromDevice)
            {
                await LoadSubscriptionsFromDevices();
            }

            _saveCheckingService.RemoveSaveCheckingEntityByOwner(_regionName);
            GooseSubcriptionDataTable = _dataTableFactory.GetGooseSubscriptionDataTableFactory();
            _saveCheckingService.AddSaveCheckingEntity(new SaveCheckingEntity(ChangeTracker,
                $"Подписка GOOSE для всех устройств", _gooseSubscriptionSavingCommand, Guid.NewGuid(), _regionName));
            ChangeTracker.AcceptChanges();
            ChangeTracker.SetTrackingEnabled(true);
        }

        private async Task LoadSubscriptionsFromDevices()
        {
            foreach (var device in _devices)
            {
                if (_connectionPoolService.GetConnection(device.Ip).IsConnected)
                {
                    try
                    {
                        await _gooseInputModelInfosLoadingService.EstimateProgress(device);
                        await _gooseInputModelInfosLoadingService.Load(device, null, _biscProject.MainSclModel.Value,
                            new CancellationToken());
                        _loggingService.LogMessage($"GooseInputModelInfo устройства {device.Name} вычитанны успешно",
                            SeverityEnum.Info);
                    }
                    catch (Exception e)
                    {
                        _loggingService.LogMessage($"Ошибка вычитывания GooseInputModelInfo устройства {device.Name}",
                            SeverityEnum.Warning);
                    }
                }
            }
        }

        #endregion
    }
}
