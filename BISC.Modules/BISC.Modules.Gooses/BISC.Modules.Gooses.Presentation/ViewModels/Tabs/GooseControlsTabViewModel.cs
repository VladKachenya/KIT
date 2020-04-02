using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Events;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Model.Services.LoadingServices;
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
using BISC.Modules.Gooses.Presentation.Events;
using BISC.Presentation.Infrastructure.Keys;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Tabs
{
    public class GooseControlsTabViewModel : NavigationViewModelBase
    {
        private readonly GooseControlViewModelFactory _gooseControlViewModelFactory;
        private readonly IGoosesModelService _goosesModelService;
        private readonly ISaveCheckingService _saveCheckingService;
        private readonly ILoggingService _loggingService;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly IGlobalEventsService _globalEventsService;
        private readonly IUserInterfaceComposingService _userInterfaceComposingService;
        private readonly GoosesLoadingService _goosesLoadingService;
        private readonly IBiscProject _biscProject;
        private IDevice _device;
        private GooseControlsProjectSavingCommand _gooseControlsSavingCommand;
        private readonly IGlobalSavingService _globalSavingService;
        private ObservableCollection<GooseControlViewModel> _gooseControlViewModels;
        private string _regionName;
        private bool _isUpdateGooses = true;
        private bool _isSaveChanges = true;

        public GooseControlsTabViewModel(GooseControlViewModelFactory gooseControlViewModelFactory, IGoosesModelService goosesModelService,
            ISaveCheckingService saveCheckingService, ICommandFactory commandFactory, ILoggingService loggingService,
            IConnectionPoolService connectionPoolService, IGlobalEventsService globalEventsService,
            IUserInterfaceComposingService userInterfaceComposingService, GoosesLoadingService goosesLoadingService,
            IBiscProject biscProject, GooseControlsProjectSavingCommand gooseControlsSavingCommand, IGlobalSavingService globalSavingService)
        :base(globalEventsService)
        {
            _gooseControlViewModelFactory = gooseControlViewModelFactory;
            _goosesModelService = goosesModelService;
            _saveCheckingService = saveCheckingService;
            _loggingService = loggingService;
            _connectionPoolService = connectionPoolService;
            _globalEventsService = globalEventsService;
            _userInterfaceComposingService = userInterfaceComposingService;
            _goosesLoadingService = goosesLoadingService;
            _biscProject = biscProject;
            _gooseControlsSavingCommand = gooseControlsSavingCommand;
            _globalSavingService = globalSavingService;
            SaveCommand = commandFactory.CreatePresentationCommand(OnSaveChangesCommand, () => _isSaveChanges);
            DeleteGooseCommand = commandFactory.CreatePresentationCommand<object>(OnDeleteGoose);
            AddGooseCommand = commandFactory.CreatePresentationCommand(OnAddGooseCommand, IsAddGoose);
            UpdateGoosesCommand = commandFactory.CreatePresentationCommand(OnUpdateGooses, () => _isUpdateGooses);
            _globalEventsService.Subscribe<GooseConfRevisionChengEvent>(OnConfRevisionChenging);
        }

        #region public interfase

        public ObservableCollection<GooseControlViewModel> GooseControlViewModels
        {
            get => _gooseControlViewModels;
            set
            {
                SetProperty(ref _gooseControlViewModels, value);
                _gooseControlsSavingCommand.Initialize(GooseControlViewModels, _device);
                _gooseControlsSavingCommand.RefreshViewModel = async () => await UpdateGooses(false);
            }
        }
        public ICommand DeleteGooseCommand { get; }
        public IPresentationCommand AddGooseCommand { get; }

        public ICommand UpdateGoosesCommand { get; }


        public ICommand SaveCommand { get; }

        #endregion

        private async void OnUpdateGooses()
        {
            _isUpdateGooses = false;
            (UpdateGoosesCommand as IPresentationCommand)?.RaiseCanExecute();
            try
            {
                await UpdateGooses(false);
                _loggingService.LogUserAction($"Вкладка Goose CB устройства {_device.Name} обновлена");
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
            _saveCheckingService.RemoveSaveCheckingEntityByOwner(_regionName);
            _saveCheckingService.AddSaveCheckingEntity(new SaveCheckingEntity(ChangeTracker, $"Блоки управления GOOSE {_device.Name}", _gooseControlsSavingCommand,
                _device.DeviceGuid, _regionName));
            ChangeTracker.AcceptChanges();
            ChangeTracker.SetTrackingEnabled(true);
            BlockViewModelBehavior.Unlock();
        }


        private void OnAddGooseCommand()
        {
            _loggingService.LogUserAction($"Добавлен goose control устройства {_device.Name}");

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

            _loggingService.LogUserAction($"Goose control {gooseCbVm.Name} устройства {_device.Name} удалён.");
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
                    $"Изменения goose control устройства {_device.Name} сохранены в проект.");
                BlockViewModelBehavior.SetBlock("Сохранение блоков управления Goose", true);
                var savingResult = await _globalSavingService.SaveСhangesToRegion(_regionName);
                if (savingResult.IsSaved)
                {
                    await UpdateGooses(false);
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
                .GetParameterByName<UiEntityIdentifier>(UiEntityIdentifier.Key).ItemId.ToString();
            SetIsReadOnly(navigationContext.BiscNavigationParameters.GetParameterByName<bool>(KeysForNavigation.NavigationParameter.IsReadOnly));
            await UpdateGooses(false);

            base.OnNavigatedTo(navigationContext);
        }

        private void UpdateViewModels()
        {
            var gooseControls = _goosesModelService.GetGooseControlsOfDevice(_device);
            GooseControlViewModels = new ObservableCollection<GooseControlViewModel>(_gooseControlViewModelFactory.CreateGooseControlViewModel(_device, gooseControls));
            foreach (var gooseControlViewModel in GooseControlViewModels)
            {
                gooseControlViewModel.SetIsReadOnly(IsReadOnly);
            }
        }

        public override void OnActivate()
        {
            if (!IsReadOnly)
            {
                _userInterfaceComposingService.SetCurrentSaveCommand(SaveCommand, $"Сохранить блоки управления GOOSE устройства { _device.Name}", false);
                _userInterfaceComposingService.AddGlobalCommand(UpdateGoosesCommand, $"Обновить GOOSE { _device.Name}", IconsKeys.UpdateIconKey, false, true);
                _userInterfaceComposingService.AddGlobalCommand(AddGooseCommand, $"Добавить блок управления GOOSE в устройство { _device.Name}", IconsKeys.AddIconKey, false, true);
            }
            //_globalEventsService.Subscribe<ConnectionEvent>(OnConnectionChanged);
            base.OnActivate();
        }

        //private void OnConnectionChanged(ConnectionEvent connectionEvent)
        //{
        //    if (connectionEvent.Ip == _device.Ip)
        //    {
        //        _userInterfaceComposingService.SetCurrentSaveCommand(SaveCommand, $"Сохранить блоки управления GOOSE устройства { _device.Name}", connectionEvent.IsConnected);
        //    }
        //}

        private async void OnConfRevisionChenging(GooseConfRevisionChengEvent chengEvent)
        {
            if (_device.DeviceGuid == chengEvent.DeviceGuid )
            {
                await UpdateGooses(false);
            }
        }

        public override void OnDeactivate()
        {
            _userInterfaceComposingService.DeleteGlobalCommand(AddGooseCommand);
            _userInterfaceComposingService.ClearCurrentSaveCommand();
            _userInterfaceComposingService.DeleteGlobalCommand(UpdateGoosesCommand);
            base.OnDeactivate();
        }

        protected override void OnDisposing()
        {
            _globalEventsService.Unsubscribe<GooseConfRevisionChengEvent>(OnConfRevisionChenging);
        }

        #endregion
    }
}
