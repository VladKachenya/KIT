using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Events;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Model.Services;
using BISC.Modules.Gooses.Presentation.Factories;
using BISC.Modules.Gooses.Presentation.Services;
using BISC.Modules.Gooses.Presentation.ViewModels.GooseControls;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Commands;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Tabs
{
    public class GooseControlsTabViewModel : NavigationViewModelBase
    {
        private readonly GooseControlViewModelFactory _gooseControlViewModelFactory;
        private readonly IGoosesModelService _goosesModelService;
        private readonly ISaveCheckingService _saveCheckingService;
        private readonly ICommandFactory _commandFactory;
        private readonly ILoggingService _loggingService;
        private readonly GooseControlSavingService _gooseControlSavingService;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly IGlobalEventsService _globalEventsService;
        private readonly IUserInterfaceComposingService _userInterfaceComposingService;
        private readonly GoosesLoadingService _goosesLoadingService;
        private readonly IBiscProject _biscProject;
        private readonly IDeviceWarningsService _deviceWarningsService;
        private IDevice _device;
        private ObservableCollection<GooseControlViewModel> _gooseControlViewModels;
        private string _regionName;

        public GooseControlsTabViewModel(GooseControlViewModelFactory gooseControlViewModelFactory, IGoosesModelService goosesModelService,
            ISaveCheckingService saveCheckingService, ICommandFactory commandFactory, ILoggingService loggingService, GooseControlSavingService gooseControlSavingService,
            IConnectionPoolService connectionPoolService, IGlobalEventsService globalEventsService,
            IUserInterfaceComposingService userInterfaceComposingService, GoosesLoadingService goosesLoadingService,
            IBiscProject biscProject,IDeviceWarningsService deviceWarningsService
            )
        {
            _gooseControlViewModelFactory = gooseControlViewModelFactory;
            _goosesModelService = goosesModelService;
            _saveCheckingService = saveCheckingService;
            _commandFactory = commandFactory;
            _loggingService = loggingService;
            _gooseControlSavingService = gooseControlSavingService;
            _connectionPoolService = connectionPoolService;
            _globalEventsService = globalEventsService;
            _userInterfaceComposingService = userInterfaceComposingService;
            _goosesLoadingService = goosesLoadingService;
            _biscProject = biscProject;
            _deviceWarningsService = deviceWarningsService;
            SaveCommand = commandFactory.CreatePresentationCommand(OnSaveChangesCommand);
            DeleteGooseCommand = commandFactory.CreatePresentationCommand<object>(OnDeleteGoose);
            AddGooseCommand = commandFactory.CreatePresentationCommand(OnAddGooseCommand, IsAddGoose);
            UpdateGoosesCommand = commandFactory.CreatePresentationCommand(OnUpdateGooses);
        }

        private async void OnUpdateGooses()
        {
            await UpdateGooses(true);
            _loggingService.LogUserAction($"Пользователь обновил состояние Goose CB (устройство {_device.Name})");

        }

        private async Task UpdateGooses(bool updatefromDevice)
        {
            BlockViewModelBehavior.SetBlock("Обновление данных",true);
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

            GooseControlViewModels.Add(_gooseControlViewModelFactory.CreateGooseControlViewModel(_device));
            AddGooseCommand.RaiseCanExecute();
        }

        private bool IsAddGoose()
        {
            var isDynamicGooses = GooseControlViewModels.Where(gse => gse.IsDynamic);
            if (isDynamicGooses.Count() >= 10) return false;
            return true;
        }

        private void OnDeleteGoose(object obj)
        {
            var gooseCbVm = obj as GooseControlViewModel;
            if (gooseCbVm == null) return;
            _loggingService.LogUserAction($"Пользователь удаляет Goose CB {gooseCbVm.Name} (устройство {_device.Name})");
            GooseControlViewModels.Remove(gooseCbVm);
            AddGooseCommand.RaiseCanExecute();
        }

        private async void OnSaveChangesCommand()
        {
            BlockViewModelBehavior.SetBlock("Сохранение блоков управления Goose", true);
            _loggingService.LogUserAction($"Пользователь сохряняет изменения в Goose CB устройства {_device.Name})");
           var res= await _gooseControlSavingService.SaveGooseControls(GooseControlViewModels.ToList(), _device, _connectionPoolService.GetConnection(_device.Ip).IsConnected);
          
            UpdateViewModels();
            ChangeTracker.AcceptChanges();
            BlockViewModelBehavior.Unlock();
            if (res.IsSucceed && res.Item == SavingResultEnum.SavedUsingFtp)
            {
                _deviceWarningsService.SetWarningOfDevice(_device.Name,GooseKeys.GooseWarningKeys.GooseSavedFtpKey);
            }
            ShowFtpBlockMessageIfNeeded();
        }
        private void ShowFtpBlockMessageIfNeeded()
        {
            if (_deviceWarningsService.GetIsDeviceWarningRegistered(_device.Name,
                GooseKeys.GooseWarningKeys.GooseSavedFtpKey))
            {
                BlockViewModelBehavior.SetBlockWithOption(
                    "Для сохранения изменений по FTP требуется перезагрузка" + Environment.NewLine +
                    "Имеется несоответствие данных.", "Все равно продолжить");
            }
        }


        #region Overrides of NavigationViewModelBase

        protected override async void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>("IED");
            _regionName = navigationContext.BiscNavigationParameters
                .GetParameterByName<TreeItemIdentifier>(TreeItemIdentifier.Key).ItemId.ToString();
            await UpdateGooses(false);
            _saveCheckingService.AddSaveCheckingEntity(new SaveCheckingEntity(ChangeTracker, $"Блоки управления GOOSE {_device.Name}", SaveCommand,_device.Name, _regionName));
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
            ShowFtpBlockMessageIfNeeded();
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
