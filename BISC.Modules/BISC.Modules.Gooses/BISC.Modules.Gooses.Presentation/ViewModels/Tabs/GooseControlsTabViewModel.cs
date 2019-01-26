using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Events;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Events;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Model.Services;
using BISC.Modules.Gooses.Presentation.Commands;
using BISC.Modules.Gooses.Presentation.Factories;
using BISC.Modules.Gooses.Presentation.ViewModels.GooseControls;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.BaseItems.ViewModels.Behaviors;
using BISC.Presentation.Infrastructure.Commands;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Tabs
{
    public class GooseControlsTabViewModel : NavigationViewModelBase
    {
        private readonly GooseControlViewModelFactory _gooseControlViewModelFactory;
        private readonly IGoosesModelService _goosesModelService;
        private readonly ISaveCheckingService _saveCheckingService;
        private readonly ICommandFactory _commandFactory;
        private readonly ILoggingService _loggingService;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly IGlobalEventsService _globalEventsService;
        private readonly IUserInterfaceComposingService _userInterfaceComposingService;
        private readonly GoosesLoadingService _goosesLoadingService;
        private readonly IBiscProject _biscProject;
        private readonly IDeviceWarningsService _deviceWarningsService;
        private readonly IDeviceReconnectionService _deviceReconnectionService;
        private IDevice _device;
        private GooseControlsSavingCommand _gooseControlsSavingCommand;
        private readonly IGlobalSavingService _globalSavingService;
        private ObservableCollection<GooseControlViewModel> _gooseControlViewModels;
        private string _regionName;
        private bool _isUpdateGooses = true;
        private bool _isSaveChanges = true;

        public GooseControlsTabViewModel(GooseControlViewModelFactory gooseControlViewModelFactory, IGoosesModelService goosesModelService,
            ISaveCheckingService saveCheckingService, ICommandFactory commandFactory, ILoggingService loggingService,
            IConnectionPoolService connectionPoolService, IGlobalEventsService globalEventsService,
            IUserInterfaceComposingService userInterfaceComposingService, GoosesLoadingService goosesLoadingService,
            IBiscProject biscProject, IDeviceWarningsService deviceWarningsService, IDeviceReconnectionService deviceReconnectionService,
            GooseControlsSavingCommand gooseControlsSavingCommand, IGlobalSavingService globalSavingService)
        {
            _gooseControlViewModelFactory = gooseControlViewModelFactory;
            _goosesModelService = goosesModelService;
            _saveCheckingService = saveCheckingService;
            _commandFactory = commandFactory;
            _loggingService = loggingService;
            _connectionPoolService = connectionPoolService;
            _globalEventsService = globalEventsService;
            _userInterfaceComposingService = userInterfaceComposingService;
            _goosesLoadingService = goosesLoadingService;
            _biscProject = biscProject;
            _deviceWarningsService = deviceWarningsService;
            _deviceReconnectionService = deviceReconnectionService;
            _gooseControlsSavingCommand = gooseControlsSavingCommand;
            _globalSavingService = globalSavingService;
            SaveCommand = commandFactory.CreatePresentationCommand(OnSaveChangesCommand, () => _isSaveChanges);
            DeleteGooseCommand = commandFactory.CreatePresentationCommand<object>(OnDeleteGoose);
            AddGooseCommand = commandFactory.CreatePresentationCommand(OnAddGooseCommand, IsAddGoose);
            UpdateGoosesCommand = commandFactory.CreatePresentationCommand(OnUpdateGooses, () => _isUpdateGooses);
        }

        private async void OnUpdateGooses()
        {
            _isUpdateGooses = false;
            (UpdateGoosesCommand as IPresentationCommand)?.RaiseCanExecute();
            try
            {
                await UpdateGooses(true);
                _loggingService.LogUserAction($"Пользователь обновил состояние Goose CB (устройство {_device.Name})");
            }
            catch (Exception e)
            {
                _loggingService.LogMessage("Goose update error", SeverityEnum.Warning);

            }
            finally
            {
                _isUpdateGooses = true;
                (UpdateGoosesCommand as IPresentationCommand)?.RaiseCanExecute();
            }
        }

        private async Task UpdateGooses(bool updatefromDevice)
        {
            BlockViewModelBehavior.SetBlock("Обновление данных", true);
            if (updatefromDevice && _connectionPoolService.GetConnection(_device.Ip).IsConnected)
            {
                await _goosesLoadingService.EstimateProgress(_device);
                await _goosesLoadingService.Load(_device, null, _biscProject.MainSclModel.Value,
                    new CancellationToken());
            }
            UpdateViewModels();
            AddGooseCommand.RaiseCanExecute();
            ChangeTracker.AcceptChanges();
            ChangeTracker.SetTrackingEnabled(true);
            BlockViewModelBehavior.Unlock();
        }


        private void OnAddGooseCommand()
        {
            _loggingService.LogUserAction($"Пользователь добавляет Goose CB (устройство {_device.Name})");

            GooseControlViewModels.Add(_gooseControlViewModelFactory.CreateGooseControlViewModel(_device, GooseControlViewModels.Where(goose => goose.IsDynamic).Select(goose => goose.Name).ToList()));
            AddGooseCommand.RaiseCanExecute();
        }

        private bool IsAddGoose()
        {
            //var isDynamicGooses = GooseControlViewModels.Where(gse => gse.IsDynamic);
            //if (isDynamicGooses.Count() >= 10) return false;
            return true;
        }

        private void OnDeleteGoose(object obj)
        {
            var gooseCbVm = obj as GooseControlViewModel;
            if (gooseCbVm == null)
            {
                return;
            }

            _loggingService.LogUserAction($"Пользователь удаляет Goose CB {gooseCbVm.Name} (устройство {_device.Name})");
            GooseControlViewModels.Remove(gooseCbVm);
            AddGooseCommand.RaiseCanExecute();
        }

        private async void OnSaveChangesCommand()
        {
            _isSaveChanges = false;
            (SaveCommand as IPresentationCommand)?.RaiseCanExecute();
            try
            {
                _loggingService.LogUserAction(
                    $"Пользователь сохряняет изменения в Goose CB устройства {_device.Name})");
                _gooseControlsSavingCommand.Initialize(GooseControlViewModels, _device);
                BlockViewModelBehavior.SetBlock("Сохранение блоков управления Goose", true);
                var savingResult = await _globalSavingService.SaveСhangesToRegion(_regionName);
                if (savingResult.IsSaved)
                {
                    UpdateViewModels();
                    ChangeTracker.AcceptChanges();
                }
            }
            finally
            {
                BlockViewModelBehavior.Unlock();
                _isSaveChanges = true;
                (SaveCommand as IPresentationCommand)?.RaiseCanExecute();
            }
        }


        #region Overrides of NavigationViewModelBase

        protected override async void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>("IED");
            _regionName = navigationContext.BiscNavigationParameters
                .GetParameterByName<TreeItemIdentifier>(TreeItemIdentifier.Key).ItemId.ToString();
            await UpdateGooses(false);
            _gooseControlsSavingCommand.Initialize(GooseControlViewModels, _device);
            _saveCheckingService.AddSaveCheckingEntity(new SaveCheckingEntity(ChangeTracker, $"Блоки управления GOOSE {_device.Name}", _gooseControlsSavingCommand,
                _device.Name, _regionName));
            base.OnNavigatedTo(navigationContext);
        }

        private void UpdateViewModels()
        {
            var gooseControls = _goosesModelService.GetGooseControlsOfDevice(_device);
            GooseControlViewModels = new ObservableCollection<GooseControlViewModel>(_gooseControlViewModelFactory.CreateGooseControlViewModel(_device, gooseControls));
        }


        public ObservableCollection<GooseControlViewModel> GooseControlViewModels
        {
            get => _gooseControlViewModels;
            set { SetProperty(ref _gooseControlViewModels, value); }
        }
        public ICommand DeleteGooseCommand { get; }
        public IPresentationCommand AddGooseCommand { get; }

        public ICommand UpdateGoosesCommand { get; }


        public ICommand SaveCommand { get; }
        public override void OnActivate()
        {
            _userInterfaceComposingService.SetCurrentSaveCommand(SaveCommand, $"Сохранить блоки управления GOOSE устройства { _device.Name}", _connectionPoolService.GetConnection(_device.Ip).IsConnected);
            _userInterfaceComposingService.AddGlobalCommand(UpdateGoosesCommand, $"Обновить GOOSE { _device.Name}", IconsKeys.UpdateIconKey, false, true);
            _userInterfaceComposingService.AddGlobalCommand(AddGooseCommand, $"Добавить блок управления GOOSE в устройство { _device.Name}", IconsKeys.AddIconKey, false, true);
            _globalEventsService.Subscribe<ConnectionEvent>(OnConnectionChanged);
            base.OnActivate();
        }

        private void OnConnectionChanged(ConnectionEvent connectionEvent)
        {
            if (connectionEvent.Ip == _device.Ip)
            {
                _userInterfaceComposingService.SetCurrentSaveCommand(SaveCommand, $"Сохранить блоки управления GOOSE устройства { _device.Name}", connectionEvent.IsConnected);
            }
        }

        public override void OnDeactivate()
        {
            _userInterfaceComposingService.DeleteGlobalCommand(AddGooseCommand);
            _userInterfaceComposingService.ClearCurrentSaveCommand();
            _userInterfaceComposingService.DeleteGlobalCommand(UpdateGoosesCommand);
            _globalEventsService.Unsubscribe<ConnectionEvent>(OnConnectionChanged);
            base.OnDeactivate();
        }

        #endregion
    }
}
