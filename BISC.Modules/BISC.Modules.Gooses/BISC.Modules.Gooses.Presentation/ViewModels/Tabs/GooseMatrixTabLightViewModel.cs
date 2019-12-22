using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Model.Services.LoadingServices;
using BISC.Modules.Gooses.Presentation.Commands;
using BISC.Modules.Gooses.Presentation.Interfaces.Factories;
using BISC.Modules.Gooses.Presentation.Interfaces.GooseSubscriptionLight;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Commands;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Tabs
{
    public class GooseMatrixTabLightViewModel : NavigationViewModelBase
    {
        private readonly IBiscProject _biscProject;
        private readonly ILoggingService _loggingService;
        private readonly GooseInputModelInfosLoadingService _gooseInputModelInfosLoadingService;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly GooseSubscriptionMatrixSavingCommand _gooseSubscriptionMatrixSavingCommand;
        private readonly ISaveCheckingService _saveCheckingService;
        private readonly IGooseControlBlockViewModelFactory _gooseControlBlockViewModelFactory;
        private readonly GooseMatrixLoadingService _gooseMatrixLoadingService;
        private readonly IGooseMatrixLightViewModelFactory _gooseMatrixLightViewModelFactory;
        private readonly IGlobalSavingService _globalSavingService;
        private readonly IUserInterfaceComposingService _userInterfaceComposingService;
        private IDevice _device;
        private string _regionName;
        private bool _isInitialized = false;
        private bool _isCommandEnabled = true;

        private List<GooseControlBlockViewModel> _gooseControlBlockViewModels;
        private List<IGoInViewModel> _goInViewModels;
        private List<IGooseDataReferenceViewModel> _gooseDataReferenceViewModels;
        private IGoInViewModel _selectedGoInViewModel;
        private IGooseDataReferenceViewModel _selectedGooseDataReferenceViewModel;


        public GooseMatrixTabLightViewModel(
            IBiscProject biscProject,
            ILoggingService loggingService,
            IGlobalEventsService globalEventsService,
            GooseInputModelInfosLoadingService gooseInputModelInfosLoadingService,
            IConnectionPoolService connectionPoolService,
            GooseSubscriptionMatrixSavingCommand gooseSubscriptionMatrixSavingCommand,
            ISaveCheckingService saveCheckingService,
            IGooseControlBlockViewModelFactory gooseControlBlockViewModelFactory,
            GooseMatrixLoadingService gooseMatrixLoadingService,
            ICommandFactory commandFactory,
            IGooseMatrixLightViewModelFactory gooseMatrixLightViewModelFactory,
            IGlobalSavingService globalSavingService,
            IUserInterfaceComposingService userInterfaceComposingService) 
            : base(globalEventsService)
        {
            _biscProject = biscProject;
            _loggingService = loggingService;
            _gooseInputModelInfosLoadingService = gooseInputModelInfosLoadingService;
            _connectionPoolService = connectionPoolService;
            _gooseSubscriptionMatrixSavingCommand = gooseSubscriptionMatrixSavingCommand;
            _saveCheckingService = saveCheckingService;
            _gooseControlBlockViewModelFactory = gooseControlBlockViewModelFactory;
            _gooseMatrixLoadingService = gooseMatrixLoadingService;
            _gooseMatrixLightViewModelFactory = gooseMatrixLightViewModelFactory;
            _globalSavingService = globalSavingService;
            _userInterfaceComposingService = userInterfaceComposingService;
            ApplySubscriptionCommand = commandFactory.CreatePresentationCommand(OnApplySubscription);
            ClearSubscriptionCommand = commandFactory.CreatePresentationCommand(OnClearSubscrioption);
            SaveCommand = commandFactory.CreatePresentationCommand(OnSave, () => _isCommandEnabled);
            UpdateCommand = commandFactory.CreatePresentationCommand(OnUpdateExecute, () => _isCommandEnabled);
        }

        public List<IGoInViewModel> GoInViewModels
        {
            get => _goInViewModels;
            set
            {
                SetProperty(ref _goInViewModels, value);
                _gooseSubscriptionMatrixSavingCommand.Initialize(_device, _gooseControlBlockViewModels, GoInViewModels);
                _gooseSubscriptionMatrixSavingCommand.RefreshViewModel = ResetChangeTracker;
            }
        }

        public IGoInViewModel SelectedGoInViewModel
        {
            get => _selectedGoInViewModel;
            set
            {
                _selectedGoInViewModel = value;
                OnPropertyChanged();
                //SelectedGooseDataReferenceViewModel = GooseDataReferenceViewModels.FirstOrDefault();
            }
        }

        public List<IGooseDataReferenceViewModel> GooseDataReferenceViewModels
        {
            get => _gooseDataReferenceViewModels;
            set
            {
                SetProperty(ref _gooseDataReferenceViewModels, value);
            }
        }

        public IGooseDataReferenceViewModel SelectedGooseDataReferenceViewModel
        {
            get => _selectedGooseDataReferenceViewModel;
            set
            {
                _selectedGooseDataReferenceViewModel = value;
                OnPropertyChanged();
            }
        }

        public ICommand ApplySubscriptionCommand { get; }
        public ICommand ClearSubscriptionCommand { get; }

        public ICommand UpdateCommand { get; }
        public ICommand SaveCommand { get; }



        private async void OnUpdateExecute()
        {
            SetEnableCommands(false);
            try
            {
                await UpdateGooseMatrix(false);
            }
            catch (Exception e)
            {
                _loggingService.LogException(e);
            }
            finally
            {
                SetEnableCommands(true);
            }
        }

        private async void OnSave()
        {
            try
            {
                SetEnableCommands(false);
                await SaveGooseMatrix();
                ResetChangeTracker();
            }
            catch (Exception e)
            {
                _loggingService.LogException(e);
            }
            finally
            {
                await UpdateGooseMatrix(false);
                SetEnableCommands(true);
            }
        }

        private void SetEnableCommands(bool isEnable)
        {
            _isCommandEnabled = isEnable;
            (SaveCommand as IPresentationCommand)?.RaiseCanExecute();
            (UpdateCommand as IPresentationCommand)?.RaiseCanExecute();
        }

        private void OnApplySubscription()
        {
            SelectedGoInViewModel.GooseDataReferenceViewModel = SelectedGooseDataReferenceViewModel;
            if (SelectedGooseDataReferenceViewModel == GooseDataReferenceViewModels.First())
            {
                SelectedGoInViewModel.EnableState = false;
                SelectedGoInViewModel.EnableQuality = false;
                SelectedGoInViewModel.EnableGooseMonitoring = false;
                return;
            }
            SelectedGoInViewModel.EnableState = true;
            if (String.IsNullOrWhiteSpace(SelectedGooseDataReferenceViewModel.DataSetReferenceQuality) ||
                SelectedGooseDataReferenceViewModel.DataSetReferenceQuality.ToLower() == "нет")
            {
                SelectedGoInViewModel.EnableQuality = false;
                SelectedGoInViewModel.EnableGooseMonitoring = false;
                return;
            }
            SelectedGoInViewModel.EnableQuality = true;
            SelectedGoInViewModel.EnableGooseMonitoring = true;
        }

        private void OnClearSubscrioption()
        {
            SelectedGoInViewModel.GooseDataReferenceViewModel = GooseDataReferenceViewModels.First();
            SelectedGoInViewModel.EnableState = false;
            SelectedGoInViewModel.EnableQuality = false;
            SelectedGoInViewModel.EnableGooseMonitoring = false;
        }

        protected override async void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            // Так как метод асинхронный то поток синхронно выполняется до первого await. После чего управление переходит к следующему оператору.
            //Необходимо вызвать OnNavigatedTo до первого await.
            base.OnNavigatedTo(navigationContext);
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>("IED");
            _regionName =
                navigationContext.BiscNavigationParameters.GetParameterByName<UiEntityIdentifier>(
                    UiEntityIdentifier.Key).ItemId.ToString();
            await UpdateGooseMatrix(false);
        }

        private async Task SaveGooseMatrix()
        {
            BlockViewModelBehavior.SetBlock("Сохранение Goose матрицы...", true);
            try
            {
                await _globalSavingService.SaveСhangesToRegion(_regionName, false);
            }
            catch (Exception e)
            {
                _loggingService.LogException(e);
            }
            finally
            {
                BlockViewModelBehavior.Unlock();
            }
        }

        private async Task UpdateGooseMatrix(bool isFromDevice)
        {
            BlockViewModelBehavior.SetBlock("Обновление данных...", false);
            try
            {
                if (isFromDevice && _connectionPoolService.GetConnection(_device.Ip).IsConnected)
                {
                    await _gooseMatrixLoadingService.Load(_device, null, _biscProject.MainSclModel.Value,
                        new CancellationToken());
                    await _gooseInputModelInfosLoadingService.Load(_device, null, _biscProject.MainSclModel.Value,
                        new CancellationToken());
                }

                _isInitialized = false;
                //GooseControlBlockViewModels?.Clear();
                _gooseControlBlockViewModels = (await _gooseControlBlockViewModelFactory.BuildGooseControlBlockViewModels(_biscProject.MainSclModel.Value, _device)).Item;
                var lightMatrix = _gooseMatrixLightViewModelFactory.CreateGooseMatrixLightViewModel(_gooseControlBlockViewModels);
                GooseDataReferenceViewModels = lightMatrix.Item2;
                SelectedGooseDataReferenceViewModel = GooseDataReferenceViewModels.FirstOrDefault();
                GoInViewModels = lightMatrix.Item1;
                SelectedGoInViewModel = GoInViewModels.FirstOrDefault();
                _isInitialized = true;
                ResetChangeTracker();
            }
            catch (Exception e)
            {
                _loggingService.LogException(e);
                throw;
            }
            finally
            {
                BlockViewModelBehavior.Unlock();
            }
        }

        private void ResetChangeTracker()
        {
            _saveCheckingService.RemoveSaveCheckingEntityByOwner(_regionName);
            _saveCheckingService.AddSaveCheckingEntity(
                new SaveCheckingEntity(ChangeTracker, $"Матрица GOOSE устройства {_device.Name}",
                    _gooseSubscriptionMatrixSavingCommand, _device.DeviceGuid, _regionName));
            ChangeTracker.AcceptChanges();
            ChangeTracker.SetTrackingEnabled(true);
        }
        #region Overrides of ViewModelBase

        public override void OnActivate()
        {

            _userInterfaceComposingService.SetCurrentSaveCommand(SaveCommand, $"Сохранить подписки GOOSE устройства {_device.Name}", false);
            _userInterfaceComposingService.AddGlobalCommand(UpdateCommand, $"Обновить GOOSE-подписки устройства {_device.Name}", IconsKeys.UpdateIconKey, false, true);
            base.OnActivate();
        }

        public override void OnDeactivate()
        {
            _userInterfaceComposingService.ClearCurrentSaveCommand();
            _userInterfaceComposingService.DeleteGlobalCommand(UpdateCommand);

            base.OnDeactivate();
        }

        protected override void OnDisposing()
        {
            _saveCheckingService.RemoveSaveCheckingEntityByOwner(_regionName);
        }

        #endregion

    }

}