using BISC.Infrastructure.Global.Services;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Reports.Infrastructure.Keys;
using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Modules.Reports.Infrastructure.Presentation.Factorys;
using BISC.Modules.Reports.Infrastructure.Presentation.ViewModels;
using BISC.Modules.Reports.Infrastructure.Services;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BISC.Modules.Reports.Presentation.ViewModels
{
    public class ReportsDetailsViewModel : NavigationViewModelBase
    {
        private string _regionName;
        private IDevice _device;
        private List<IReportControl> _reportControlsModel;
        private IReportsModelService _reportsModelService;
        private ISaveCheckingService _saveCheckingService;
        private IReportControlFactoryViewModel _reportControlFactoryViewModel;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly IUserInterfaceComposingService _userInterfaceComposingService;
        private ObservableCollection<IReportControlViewModel> _reportControlViewModels;
        private readonly ILoggingService _loggingService;
        private IReportControlSavingService _reportControlSavingService;


        #region Ctor
        public ReportsDetailsViewModel(ICommandFactory commandFactory, IReportsModelService reportsModelService, ISaveCheckingService saveCheckingService,
            IReportControlFactoryViewModel reportControlFactoryViewModel, IUserInterfaceComposingService userInterfaceComposingService, IConnectionPoolService connectionPoolService,
            ILoggingService loggingService, IReportControlSavingService reportControlSavingService) 
        {
            _reportsModelService = reportsModelService;
            _saveCheckingService = saveCheckingService;
            _reportControlFactoryViewModel = reportControlFactoryViewModel;
            _userInterfaceComposingService = userInterfaceComposingService;
            _connectionPoolService = connectionPoolService;
            _reportControlSavingService = reportControlSavingService;
            SaveСhangesCommand = commandFactory.CreatePresentationCommand(OnSaveСhangesCommand);
            AddNewReportCommand = commandFactory.CreatePresentationCommand(OnAddNewReportCommand);
            UndoChangesCommad = commandFactory.CreatePresentationCommand(OnUndoChanges);
            _loggingService = loggingService;
            ModelRegionKey = Guid.NewGuid().ToString();
        }
        #endregion

        #region private methods
        private async void OnSaveСhangesCommand()
        {
            _loggingService.LogUserAction($"Пользователь сохраняет изменения Report устройства {_device.Name}");
            await _reportControlSavingService.SaveReportControls(ReportControlViewModels.ToList(), _device, _connectionPoolService.GetConnection(_device.Ip).IsConnected);
            _reportControlsModel = _reportsModelService.GetAllReportControlsOfDevice(_device);
            ReportControlViewModels = _reportControlFactoryViewModel.GetReportControlsViewModel(_reportControlsModel, _device);
            ChangeTracker.AcceptChanges();
        }

        private void ResetAllCollections()
        {
            _reportControlsModel.Clear();
            ReportControlViewModels.Clear();
        }

        private void OnAddNewReportCommand()
        {
            ReportControlViewModels.Add(_reportControlFactoryViewModel.GetReportControlViewModel(_device));
        }

        private void OnUndoChanges()
        {
            foreach (var element in ReportControlViewModels)
                element.UpdateViewModel();
            ChangeTracker.AcceptChanges();
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
        public ICommand UndoChangesCommad { get; }
        public ICommand TestCommand { get; }
        public ICommand AddNewReportCommand { get; }
        public string ModelRegionKey { get; }

        #endregion

        #region override of NavigationViewModelBase
        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>(DeviceKeys.DeviceModelKey);
            _reportControlsModel = _reportsModelService.GetAllReportControlsOfDevice(_device);
            ReportControlViewModels = _reportControlFactoryViewModel.GetReportControlsViewModel(_reportControlsModel, _device);
            _regionName = navigationContext.BiscNavigationParameters
                .GetParameterByName<TreeItemIdentifier>(TreeItemIdentifier.Key).ItemId.ToString();
            _saveCheckingService.AddSaveCheckingEntity(new SaveCheckingEntity(ChangeTracker,
                $"Reports устройства {_device.Name}", SaveСhangesCommand, _regionName));
            ChangeTracker.SetTrackingEnabled(true);
            base.OnNavigatedTo(navigationContext);
        }

        public override void OnActivate()
        {

            _userInterfaceComposingService.SetCurrentSaveCommand(SaveСhangesCommand, $"Сохранить Report устройства { _device.Name}", _connectionPoolService.GetConnection(_device.Ip).IsConnected);
            _userInterfaceComposingService.AddGlobalCommand(AddNewReportCommand, $"Добавить Report {_device.Name}", IconsKeys.AddIconKey, false, true);
            _userInterfaceComposingService.AddGlobalCommand(UndoChangesCommad, $"Отменить изменения Reports {_device.Name}", IconsKeys.CloseCircleIconKey, false, true);

            //_globalEventsService.Subscribe<ConnectionEvent>(OnConnectionChanged);
            base.OnActivate();
        }

        public override void OnDeactivate()
        {
            _userInterfaceComposingService.ClearCurrentSaveCommand();
            _userInterfaceComposingService.DeleteGlobalCommand(AddNewReportCommand);
            _userInterfaceComposingService.DeleteGlobalCommand(UndoChangesCommad);
            //_globalEventsService.Unsubscribe<ConnectionEvent>(OnConnectionChanged);
            base.OnDeactivate();
        }

        protected override void OnDisposing()
        {
            _saveCheckingService.RemoveSaveCheckingEntityByOwner(_regionName);
            base.OnDisposing();
        }
        #endregion
    }
}
