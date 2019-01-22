using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Events;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Reports.Infrastructure.Keys;
using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Modules.Reports.Infrastructure.Presentation.Factorys;
using BISC.Modules.Reports.Infrastructure.Presentation.Services;
using BISC.Modules.Reports.Infrastructure.Presentation.ViewModels;
using BISC.Modules.Reports.Infrastructure.Services;
using BISC.Modules.Reports.Model.Services;
using BISC.Modules.Reports.Presentation.Commands;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.BaseItems.ViewModels.Behaviors;
using BISC.Presentation.Infrastructure.Commands;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BISC.Modules.Reports.Presentation.ViewModels
{
    public class ReportsDetailsViewModel : NavigationViewModelBase
    {
        private string _regionName;
        private IDevice _device;

        private List<IReportControl> _reportControlsModel;
        private readonly ICommandFactory _commandFactory;
        private IReportsModelService _reportsModelService;
        private ISaveCheckingService _saveCheckingService;
        private IReportControlFactoryViewModel _reportControlFactoryViewModel;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly IUserInterfaceComposingService _userInterfaceComposingService;
        private ObservableCollection<IReportControlViewModel> _reportControlViewModels;
        private readonly ILoggingService _loggingService;
        private ReportsSavingCommand _reportsSavingCommand;
        private readonly IBiscProject _biscProject;
        private readonly ReportControlLoadingService _reportControlLoadingService;
        private readonly IUserNotificationService _userNotificationService;
        private readonly IUserInteractionService _userInteractionService;
        private readonly IDeviceWarningsService _deviceWarningsService;
        private readonly IDeviceReconnectionService _deviceReconnectionService;
        private readonly IGlobalEventsService _globalEventsService;
        private readonly IReportVeiwModelService _reportVeiwModelService;
        private bool _isUpdateReports = true;
        private bool _isSaveСhanges = true;




        #region Ctor
        public ReportsDetailsViewModel(ICommandFactory commandFactory, IReportsModelService reportsModelService, ISaveCheckingService saveCheckingService,
                IReportControlFactoryViewModel reportControlFactoryViewModel, IUserInterfaceComposingService userInterfaceComposingService, IConnectionPoolService connectionPoolService,
                ILoggingService loggingService, ReportsSavingCommand reportsSavingCommand, IBiscProject biscProject,
                ReportControlLoadingService reportControlLoadingService, IUserNotificationService userNotificationService, IUserInteractionService userInteractionService,
                IDeviceWarningsService deviceWarningsService, IDeviceReconnectionService deviceReconnectionService, IGlobalEventsService globalEventsService,
                IReportVeiwModelService reportVeiwModelService)
        //IReportsLoadingService reportsLoadingService, 
        {
            _commandFactory = commandFactory;
            _reportsModelService = reportsModelService;
            _saveCheckingService = saveCheckingService;
            _reportControlFactoryViewModel = reportControlFactoryViewModel;
            _userInterfaceComposingService = userInterfaceComposingService;
            _connectionPoolService = connectionPoolService;
            _reportsSavingCommand = reportsSavingCommand;
            _biscProject = biscProject;
            _reportControlLoadingService = reportControlLoadingService;
            _userNotificationService = userNotificationService;
            _userInteractionService = userInteractionService;
            _deviceWarningsService = deviceWarningsService;
            _deviceReconnectionService = deviceReconnectionService;
            _globalEventsService = globalEventsService;
            _reportVeiwModelService = reportVeiwModelService;
            //_reportsLoadingService = reportsLoadingService;
            SaveСhangesCommand = commandFactory.CreatePresentationCommand(OnSaveСhangesCommand, () => _isSaveСhanges);
            AddNewReportCommand = commandFactory.CreatePresentationCommand(OnAddNewReportCommand, IsAddNewReport);
            UpdateReportsCommad = commandFactory.CreatePresentationCommand(OnUpdateReports, () => _isUpdateReports);
            DeleteReportCommand = commandFactory.CreatePresentationCommand<object>(OnDeleteReport);
            _loggingService = loggingService;
            ModelRegionKey = Guid.NewGuid().ToString();
        }

        private void OnDeleteReport(object obj)
        {
            if (obj is IReportControlViewModel reportControlViewModel)
            {
                ReportControlViewModels.Remove(reportControlViewModel);
                _reportsSavingCommand.Initialize(ReportControlViewModels, _device, () => _connectionPoolService.GetConnection(_device.Ip).IsConnected);
                AddNewReportCommand.RaiseCanExecute();
            }
        }

        #endregion

        #region private methods
        private async void OnSaveСhangesCommand()
        {

            _isSaveСhanges = false;
            _loggingService.LogUserAction($"Пользователь сохраняет изменения Report устройства {_device.Name}");
            if (await _reportsSavingCommand.IsSavingByFtpNeeded())
            {
                await _deviceReconnectionService.ExecuteBeforeRestart(_device);
            }
            else
            {
                await SaveChanges();
            }
            _isSaveСhanges = true;



        }

        private async Task SaveChanges()
        {
            (SaveСhangesCommand as IPresentationCommand)?.RaiseCanExecute();
            try
            {
                BlockViewModelBehavior.SetBlock("Сохранение отчетов", true);
                var res = await _reportsSavingCommand.SaveAsync();
                //UpdateViewModels();
                //ChangeTracker.AcceptChanges();
            }
            finally
            {
                _isSaveСhanges = true;
                BlockViewModelBehavior.Unlock();
                (SaveСhangesCommand as IPresentationCommand)?.RaiseCanExecute();
            }
        }

        private void FineshSaving(bool isFtpSaving)
        {
            if (isFtpSaving)
            {
                if (_device.Manufacturer == DeviceKeys.DeviceManufacturer.BemnManufacturer)
                {
                    _deviceWarningsService.SetWarningOfDevice(_device.Name, ReportsKeys.ReportsPresentationKeys.ReportsFtpIncostistancyWarningTag, "Reports сохранены с использование FTP");
                    ShowFtpBlockMessageIfNeeded();
                }
            }
            else
            {
                BlockViewModelBehavior.Unlock();
            }
            UpdateCurentChengeTracker();
        }

        private void ShowFtpBlockMessageIfNeeded()
        {
            if (_deviceWarningsService.GetIsDeviceWarningRegistered(_device.Name,
                ReportsKeys.ReportsPresentationKeys.ReportsFtpIncostistancyWarningTag))
            {
                BlockViewModelBehavior.SetBlockWithOption(
                    "Для сохранения изменений по FTP требуется перезагрузка" + Environment.NewLine +
                    "Имеется несоответствие данных.", new UnlockCommandEntity("Все равно продолжить"),
                    new UnlockCommandEntity("Перезагрузить устройство", _commandFactory.CreatePresentationCommand(() => _globalEventsService.SendMessage(new ResetByFtpEvent { DeviceName = _device.Name, Ip = _device.Ip }))));
            }
        }


        private void OnAddNewReportCommand()
        {
            _loggingService.LogUserAction($"Пользователь добавил Report устройства {_device.Name}");
            ReportControlViewModels.Add(
                _reportControlFactoryViewModel.CreateReportViewModel(
                    ReportControlViewModels.Select((model => model.ReportID)).ToList(), _device));
            _reportsSavingCommand.Initialize(ReportControlViewModels, _device, () => _connectionPoolService.GetConnection(_device.Ip).IsConnected);
            AddNewReportCommand.RaiseCanExecute();
        }

        private bool IsAddNewReport()
        {
            if (ReportControlViewModels.Count() >= 30)
            {
                return false;
            }

            return true;
        }

        private async void OnUpdateReports()
        {
            _isUpdateReports = false;
            (UpdateReportsCommad as IPresentationCommand)?.RaiseCanExecute();
            try
            {
                await UpdateReports(true);
            }
            catch (Exception e)
            {
                _loggingService.LogMessage("Ошибка обновления Reports", SeverityEnum.Warning);
                BlockViewModelBehavior.Unlock();
            }
            finally
            {
                _isUpdateReports = true;
                _reportsSavingCommand.Initialize(ReportControlViewModels, _device, () => _connectionPoolService.GetConnection(_device.Ip).IsConnected);
                (UpdateReportsCommad as IPresentationCommand)?.RaiseCanExecute();
            }
        }




        private async Task UpdateReports(bool updateFromDevice)
        {
            BlockViewModelBehavior.SetBlock("Обновление данных", true);
            if (updateFromDevice && _connectionPoolService.GetConnection(_device.Ip).IsConnected)
            {
                await _reportControlLoadingService.EstimateProgress(_device);
                await _reportControlLoadingService.Load(_device, null, _biscProject.MainSclModel.Value, new CancellationToken());
            }

            UpdateCurentChengeTracker();
            BlockViewModelBehavior.Unlock();
        }

        private void UpdateCurentChengeTracker()
        {
            UpdateViewModels();
            _saveCheckingService.RemoveSaveCheckingEntityByOwner(_regionName);
            _saveCheckingService.RemoveSaveCheckingEntityByOwner(_regionName);
            _saveCheckingService.AddSaveCheckingEntity(new SaveCheckingEntity(ChangeTracker,
                $"Reports устройства {_device.Name}", _reportsSavingCommand, _device.Name, _regionName));
            AddNewReportCommand.RaiseCanExecute();
            ChangeTracker.AcceptChanges();
            ChangeTracker.SetTrackingEnabled(true);
            _reportsSavingCommand.Initialize(ReportControlViewModels, _device, () => _connectionPoolService.GetConnection(_device.Ip).IsConnected);
        }

        private void UpdateViewModels()
        {
            _reportControlsModel = _reportsModelService.GetAllReportControlsOfDevice(_device);
            ReportControlViewModels = _reportVeiwModelService.SortReportViewModels(_reportControlFactoryViewModel.GetReportControlsViewModel(_reportControlsModel, _device));
        }
        #endregion

        #region public interface
        public ObservableCollection<IReportControlViewModel> ReportControlViewModels
        {
            get => _reportControlViewModels;
            protected set => SetProperty(ref _reportControlViewModels, value);
        }


        public ICommand DeleteReportCommand { get; }
        public ICommand SaveСhangesCommand { get; }
        public ICommand UpdateReportsCommad { get; }
        public IPresentationCommand AddNewReportCommand { get; }
        public string ModelRegionKey { get; }

        #endregion

        #region override of NavigationViewModelBase

        protected override async void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>(DeviceKeys.DeviceModelKey);
            _regionName = navigationContext.BiscNavigationParameters
                .GetParameterByName<TreeItemIdentifier>(TreeItemIdentifier.Key).ItemId.ToString();
            await UpdateReports(false);
            base.OnNavigatedTo(navigationContext);
            //await _userInteractionService.ShowOptionToUser("Требуется перезапуск устройства",
            //    "Для дальнейшей работы необходимо перезапустить устройство", new List<string>() { }, "100");
        }

        public override void OnActivate()
        {
            //ShowFtpBlockMessageIfNeeded();
            _userInterfaceComposingService.SetCurrentSaveCommand(SaveСhangesCommand, $"Сохранить Report устройства { _device.Name}", _connectionPoolService.GetConnection(_device.Ip).IsConnected);
            _userInterfaceComposingService.AddGlobalCommand(UpdateReportsCommad, $"Обновить Report-ы {_device.Name}", IconsKeys.UpdateIconKey, false, true);
            _userInterfaceComposingService.AddGlobalCommand(AddNewReportCommand, $"Добавить Report {_device.Name}", IconsKeys.AddIconKey, false, true);
            //_globalEventsService.Subscribe<ConnectionEvent>(OnConnectionChanged);
            base.OnActivate();
        }

        public override void OnDeactivate()
        {
            _userInterfaceComposingService.ClearCurrentSaveCommand();
            _userInterfaceComposingService.DeleteGlobalCommand(AddNewReportCommand);
            _userInterfaceComposingService.DeleteGlobalCommand(UpdateReportsCommad);
            //_globalEventsService.Unsubscribe<ConnectionEvent>(OnConnectionChanged);
            base.OnDeactivate();
        }

        protected override void OnDisposing()
        {
            _saveCheckingService.RemoveSaveCheckingEntityByOwner(_regionName);
            ReportControlViewModels.ToList().ForEach(element => (element as IDisposable).Dispose());
            base.OnDisposing();
        }
        #endregion
    }
}
