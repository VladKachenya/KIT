﻿using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Modules.Reports.Infrastructure.Services;
using BISC.Modules.Reports.Model.Services;
using BISC.Modules.Reports.Presentation.Commands;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.BaseItems.ViewModels.Behaviors;
using BISC.Presentation.Infrastructure.Commands;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Modules.Reports.Infrastructure.Events;
using BISC.Modules.Reports.Presentation.Interfaces.Factorys;
using BISC.Modules.Reports.Presentation.Interfaces.Services;
using BISC.Modules.Reports.Presentation.Interfaces.ViewModels;
using BISC.Presentation.Infrastructure.Keys;

namespace BISC.Modules.Reports.Presentation.ViewModels
{
    public class ReportsDetailsViewModel : NavigationViewModelBase
    {
        private string _regionName;
        private IDevice _device;
        private ObservableCollection<IReportControlViewModel> _reportControlViewModels;
        private List<IReportControl> _reportControlsModel;


        private readonly IReportsModelService _reportsModelService;
        private readonly ISaveCheckingService _saveCheckingService;
        private readonly IReportControlFactoryViewModel _reportControlFactoryViewModel;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly IUserInterfaceComposingService _userInterfaceComposingService;
        private readonly ILoggingService _loggingService;
        private readonly ReportsSavingCommand _reportsSavingCommand;
        private readonly IBiscProject _biscProject;
        private readonly ReportControlLoadingService _reportControlLoadingService;
        private readonly IReportViewModelService _reportViewModelService;
        private readonly IGlobalSavingService _globalSavingService;
        private readonly IGlobalEventsService _globalEventsService;
        private bool _isUpdateReports = true;
        private bool _isSaveСhanges = true;
        private IReportControlViewModel _selectedReport;

        #region Ctor
        public ReportsDetailsViewModel(
            ICommandFactory commandFactory,
            IReportsModelService reportsModelService,
            ISaveCheckingService saveCheckingService,
            IReportControlFactoryViewModel reportControlFactoryViewModel,
            IUserInterfaceComposingService userInterfaceComposingService,
            IConnectionPoolService connectionPoolService,
            ILoggingService loggingService,
            ReportsSavingCommand reportsSavingCommand,
            IBiscProject biscProject,
            ReportControlLoadingService reportControlLoadingService,
            IReportViewModelService reportViewModelService,
            IGlobalSavingService globalSavingService,
            IGlobalEventsService globalEventsService)
            : base(globalEventsService)
        {
            _reportsModelService = reportsModelService;
            _saveCheckingService = saveCheckingService;
            _reportControlFactoryViewModel = reportControlFactoryViewModel;
            _userInterfaceComposingService = userInterfaceComposingService;
            _connectionPoolService = connectionPoolService;
            _reportsSavingCommand = reportsSavingCommand;
            _biscProject = biscProject;
            _reportControlLoadingService = reportControlLoadingService;
            _reportViewModelService = reportViewModelService;
            _globalSavingService = globalSavingService;
            _globalEventsService = globalEventsService;

            SaveСhangesCommand = commandFactory.CreatePresentationCommand(OnSaveСhangesCommand, () => _isSaveСhanges);
            AddNewReportCommand = commandFactory.CreatePresentationCommand(OnAddNewReportCommand, IsAddNewReport);
            UpdateReportsCommad = commandFactory.CreatePresentationCommand(OnUpdateReports, () => _isUpdateReports);
            DeleteReportCommand = commandFactory.CreatePresentationCommand<object>(OnDeleteReport);
            _loggingService = loggingService;
            _globalEventsService.Subscribe<ReportConfRevisionChengEvent>(OnConfRevisionChenging);
            ModelRegionKey = Guid.NewGuid().ToString();
        }

        private void OnDeleteReport(object obj)
        {
            if (obj is IReportControlViewModel reportControlViewModel)
            {
                ReportControlViewModels.Remove(reportControlViewModel);
                AddNewReportCommand.RaiseCanExecute();
            }
        }

        #endregion

        #region private methods
        private async void OnSaveСhangesCommand()
        {
            try
            {
                _isSaveСhanges = false;
                BlockViewModelBehavior.SetBlock("Сохранение отчетов", true);
                (SaveСhangesCommand as IPresentationCommand)?.RaiseCanExecute();
                _loggingService.LogUserAction($"Сохранение изменений Report устройства {_device.Name}");
                var savingRes = await _globalSavingService.SaveСhangesToRegion(_regionName);
                if (savingRes.IsSaved)
                {
                    await UpdateReports();
                }
            }
            finally
            {
                _isSaveСhanges = true;
                BlockViewModelBehavior.Unlock();
                (SaveСhangesCommand as IPresentationCommand)?.RaiseCanExecute();
            }
        }



        private void OnAddNewReportCommand()
        {
            _loggingService.LogUserAction($"Добавление Report в устройство {_device.Name}");
            ReportControlViewModels.Add(
                _reportControlFactoryViewModel.CreateReportViewModel(
                    ReportControlViewModels.Select((model => model.ReportID)).ToList(), _device));
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
                await UpdateReports();
            }
            catch (Exception e)
            {
                _loggingService.LogMessage("Ошибка обновления Reports", SeverityEnum.Warning);
                BlockViewModelBehavior.Unlock();
            }
            finally
            {
                _isUpdateReports = true;
                (UpdateReportsCommad as IPresentationCommand)?.RaiseCanExecute();
            }
        }




        private async Task UpdateReports()
        {
            try
            {
                BlockViewModelBehavior.SetBlock("Обновление данных", true);
                UpdateCurentChengeTracker();
            }
            finally
            {
                BlockViewModelBehavior.Unlock();
            }
        }

        private void UpdateCurentChengeTracker()
        {
            UpdateViewModels();
            _saveCheckingService.RemoveSaveCheckingEntityByOwner(_regionName);
            _saveCheckingService.RemoveSaveCheckingEntityByOwner(_regionName);
            _saveCheckingService.AddSaveCheckingEntity(new SaveCheckingEntity(ChangeTracker,
                $"Reports устройства {_device.Name}", _reportsSavingCommand, _device.DeviceGuid, _regionName));
            AddNewReportCommand.RaiseCanExecute();
            ChangeTracker.AcceptChanges();
            ChangeTracker.SetTrackingEnabled(true);
        }

        private void UpdateViewModels()
        {
            _reportControlsModel = _reportsModelService.GetAllReportControlsOfDevice(_device);
            ReportControlViewModels = _reportViewModelService.SortReportViewModels(_reportControlFactoryViewModel.GetReportControlsViewModel(_reportControlsModel, _device));
            foreach (var reportControlViewModel in ReportControlViewModels)
            {
                reportControlViewModel.IsEditable = IsEditable;
            }
        }
        #endregion

        #region public interface

        public ObservableCollection<IReportControlViewModel> ReportControlViewModels
        {
            get => _reportControlViewModels;
            protected set
            {
                SetProperty(ref _reportControlViewModels, value);
                _reportsSavingCommand.Initialize(ref _reportControlViewModels, _device);
                _reportsSavingCommand.RefreshViewModel =
                    async () => { await UpdateReports(); };
            }
        }

        public IReportControlViewModel SelectedReport
        {
            get => _selectedReport;
            set
            {
                SetProperty(ref _selectedReport, value, true);
                if (SelectedReport != null) ((ViewModelBase)SelectedReport).IsWarning = false;
            }
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
                .GetParameterByName<UiEntityIdentifier>(UiEntityIdentifier.Key).ItemId.ToString();
            SetIsReadOnly(navigationContext.BiscNavigationParameters.GetParameterByName<bool>(KeysForNavigation
                    .NavigationParameter.IsReadOnly));
            IsEditable = !IsReadOnly;
            await UpdateReports();
            base.OnNavigatedTo(navigationContext);
        }

        public override void OnActivate()
        {
            if (!IsReadOnly)
            {
                _userInterfaceComposingService.SetCurrentSaveCommand(SaveСhangesCommand, $"Сохранить Report устройства { _device.Name}", false);
                _userInterfaceComposingService.AddGlobalCommand(UpdateReportsCommad, $"Обновить Report-ы {_device.Name}", IconsKeys.UpdateIconKey, false, true);
                _userInterfaceComposingService.AddGlobalCommand(AddNewReportCommand, $"Добавить Report {_device.Name}", IconsKeys.AddIconKey, false, true);
            }

            base.OnActivate();
        }

        private async void OnConfRevisionChenging(ReportConfRevisionChengEvent chengEvent)
        {
            if (_device.DeviceGuid == chengEvent.DeviceGuid)
            {
                await UpdateReports();
            }
        }

        public override void OnDeactivate()
        {
            _userInterfaceComposingService.ClearCurrentSaveCommand();
            _userInterfaceComposingService.DeleteGlobalCommand(AddNewReportCommand);
            _userInterfaceComposingService.DeleteGlobalCommand(UpdateReportsCommad);
            base.OnDeactivate();
        }

        protected override void OnDisposing()
        {
            _saveCheckingService.RemoveSaveCheckingEntityByOwner(_regionName);
            _globalEventsService.Unsubscribe<ReportConfRevisionChengEvent>(OnConfRevisionChenging);
            ReportControlViewModels.ToList().ForEach(element => (element as IDisposable).Dispose());
            base.OnDisposing();
        }
        #endregion
    }
}
