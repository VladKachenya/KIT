using BISC.Infrastructure.Global.Services;
using BISC.Modules.Device.Infrastructure.Events;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Reports.Infrastructure.Keys;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BISC.Modules.Reports.Presentation.ViewModels
{
    public class ReportsTreeItemViewModel : NavigationViewModelBase
    {
        private readonly ITabManagementService _tabManagementService;
        private readonly IGlobalEventsService _globalEventsService;
        private readonly IDeviceWarningsService _deviceWarningsService;
        private IDevice _device;

        private UiEntityIdentifier _reportsDetailsIdentifier;
        private bool _isReportWarning;

        #region C-tor
        public ReportsTreeItemViewModel(ICommandFactory commandFactory, ITabManagementService tabManagementService,
            IGlobalEventsService globalEventsService, IDeviceWarningsService deviceWarningsService)
        {
            _tabManagementService = tabManagementService;
            _globalEventsService = globalEventsService;
            _deviceWarningsService = deviceWarningsService;
            NavigateToDetailsCommand = commandFactory.CreatePresentationCommand(OnNavigateToDetails);
            WarningsCollection = new ObservableCollection<string>();
        }
        #endregion


        #region private filds
        private void OnNavigateToDetails()
        {
            BiscNavigationParameters biscNavigationParameters = new BiscNavigationParameters();
            biscNavigationParameters.AddParameterByName("IED", _device);
            _tabManagementService.NavigateToTab(ReportsKeys.ReportsPresentationKeys.ReportsDetailsView, biscNavigationParameters, $"Reports {_device.Name}", _reportsDetailsIdentifier);
        }
        #endregion

        public bool IsReportWarning
        {
            get => _isReportWarning;
            set { SetProperty(ref _isReportWarning, value); }
        }
        public ObservableCollection<string> WarningsCollection { get; }

        #region Implementation of IDataSetsTreeItemViewModel
        public ICommand NavigateToDetailsCommand { get; }

        #endregion

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>("IED");
            var treeItemIdentifier =
                navigationContext.BiscNavigationParameters.GetParameterByName<UiEntityIdentifier>(
                    UiEntityIdentifier.Key);
            _reportsDetailsIdentifier = new UiEntityIdentifier(Guid.NewGuid(), treeItemIdentifier);
            _globalEventsService.Subscribe<DeviceWarningsChanged>(OnDeviceWarningsChanged);
            base.OnNavigatedTo(navigationContext);
        }

        private void OnDeviceWarningsChanged(DeviceWarningsChanged deviceWarningsChanged)
        {
            if (deviceWarningsChanged.DeviceGuid != _device.DeviceGuid)
            {
                return;
            }

            WarningsCollection.Clear();
            if (_deviceWarningsService.GetIsDeviceWarningRegistered(_device.DeviceGuid, ReportsKeys.ReportsPresentationKeys.ReportsFtpIncostistancyWarningTag))
            {
                IsReportWarning = true;
                WarningsCollection.Add(_deviceWarningsService.GetWarningMassage(_device.DeviceGuid, ReportsKeys.ReportsPresentationKeys.ReportsFtpIncostistancyWarningTag));
            }
            if (_deviceWarningsService.GetIsDeviceWarningRegistered(_device.DeviceGuid, ReportsKeys.ReportsPresentationKeys.ReportsIncostistancyWarningTag))
            {
                IsReportWarning = true;
                WarningsCollection.Add(_deviceWarningsService.GetWarningMassage(_device.DeviceGuid, ReportsKeys.ReportsPresentationKeys.ReportsIncostistancyWarningTag));
            }
            if (_deviceWarningsService.GetIsDeviceWarningRegistered(_device.DeviceGuid, ReportsKeys.ReportsPresentationKeys.ReportsLoadErrorWarningTag))
            {
                IsReportWarning = true;
                WarningsCollection.Add(_deviceWarningsService.GetWarningMassage(_device.DeviceGuid, ReportsKeys.ReportsPresentationKeys.ReportsLoadErrorWarningTag));
            }

        }

        #region Overrides of ViewModelBase

        protected override void OnDisposing()
        {
            _globalEventsService.Unsubscribe<DeviceWarningsChanged>(OnDeviceWarningsChanged);
            base.OnDisposing();
        }

        #endregion
    }
}
