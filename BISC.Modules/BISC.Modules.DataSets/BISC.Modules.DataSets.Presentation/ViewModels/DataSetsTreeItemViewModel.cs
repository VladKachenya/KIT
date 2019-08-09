using BISC.Infrastructure.Global.Services;
using BISC.Modules.DataSets.Infrastructure.Keys;
using BISC.Modules.DataSets.Infrastructure.ViewModels;
using BISC.Modules.Device.Infrastructure.Events;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;
using System;
using System.Windows.Input;

namespace BISC.Modules.DataSets.Presentation.ViewModels
{
    public class DataSetsTreeItemViewModel : NavigationViewModelBase, IDataSetsTreeItemViewModel
    {
        private readonly ITabManagementService _tabManagementService;
        private readonly IDeviceWarningsService _deviceWarningsService;
        private readonly IGlobalEventsService _globalEventsService;
        private IDevice _device;
        private bool _isReportWarning;

        private UiEntityIdentifier _dataSetDetailsIdentifier;
        #region C-tor
        public DataSetsTreeItemViewModel(ICommandFactory commandFactory, ITabManagementService tabManagementService, IDeviceWarningsService deviceWarningsService,
            IGlobalEventsService globalEventsService)
            : base(null)
        {
            _tabManagementService = tabManagementService;
            _deviceWarningsService = deviceWarningsService;
            _globalEventsService = globalEventsService;
            NavigateToDetailsCommand = commandFactory.CreatePresentationCommand(OnNavigateToDetails);
        }
        #endregion


        #region private filds
        private void OnDeviceWarningsChanged(DeviceWarningsChanged deviceWarningsChanged)
        {
            if (deviceWarningsChanged.DeviceGuid != _device.DeviceGuid)
            {
                return;
            }

            WarningsCollection.Clear();
            if (_deviceWarningsService.GetIsDeviceWarningRegistered(_device.DeviceGuid, DatasetKeys.DataSetWarningKeys.DataSetLoadErrorWarningTagKey))
            {
                IsReportWarning = true;
                WarningsCollection.Add(_deviceWarningsService.GetWarningMassage(_device.DeviceGuid, DatasetKeys.DataSetWarningKeys.DataSetLoadErrorWarningTagKey));
            }
            if (_deviceWarningsService.GetIsDeviceWarningRegistered(_device.DeviceGuid, DatasetKeys.DataSetWarningKeys.DataSetsUnsavedWarningTagKey))
            {
                IsReportWarning = true;
                WarningsCollection.Add(_deviceWarningsService.GetWarningMassage(_device.DeviceGuid, DatasetKeys.DataSetWarningKeys.DataSetsUnsavedWarningTagKey));
            }
        }

        private void OnNavigateToDetails()
        {
            BiscNavigationParameters biscNavigationParameters = new BiscNavigationParameters();
            biscNavigationParameters.AddParameterByName("IED", _device);
            _tabManagementService.NavigateToTab(DatasetKeys.DatasetViewModelKeys.DataSetsDetailsView, biscNavigationParameters, $"DataSets {_device.Name}", _dataSetDetailsIdentifier);
        }
        #endregion




        #region Implementation of IDataSetsTreeItemViewModel
        public ICommand NavigateToDetailsCommand { get; }

        #endregion


        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>("IED");
            var treeItemIdentifier =
                navigationContext.BiscNavigationParameters.GetParameterByName<UiEntityIdentifier>(
                    UiEntityIdentifier.Key);
            _dataSetDetailsIdentifier = new UiEntityIdentifier(Guid.NewGuid(), treeItemIdentifier);
            _globalEventsService.Subscribe<DeviceWarningsChanged>(OnDeviceWarningsChanged);
            base.OnNavigatedTo(navigationContext);
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
