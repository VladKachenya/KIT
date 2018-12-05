using BISC.Modules.DataSets.Infrastructure.Keys;
using BISC.Modules.DataSets.Infrastructure.ViewModels;
using BISC.Modules.Device.Infrastructure.Model;
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
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Device.Infrastructure.Events;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Presentation.BaseItems.Events;

namespace BISC.Modules.DataSets.Presentation.ViewModels
{
    public class DataSetsTreeItemViewModel : NavigationViewModelBase, IDataSetsTreeItemViewModel
    {
        private readonly ITabManagementService _tabManagementService;
        private readonly IDeviceWarningsService _deviceWarningsService;
        private readonly IGlobalEventsService _globalEventsService;
        private IDevice _device;
        private bool _isReportWarning;

        private TreeItemIdentifier _dataSetDetailsIdentifier;
        #region C-tor
        public DataSetsTreeItemViewModel(ICommandFactory commandFactory, ITabManagementService tabManagementService, IDeviceWarningsService deviceWarningsService,
            IGlobalEventsService globalEventsService)
        {
            _tabManagementService = tabManagementService;
            _deviceWarningsService = deviceWarningsService;
            _globalEventsService = globalEventsService;
            NavigateToDetailsCommand = commandFactory.CreatePresentationCommand(OnNavigateToDetails);
            WarningsCollection = new ObservableCollection<string>();
        }
        #endregion


        #region private filds
        private void OnDeviceWarningsChanged(DeviceWarningsChanged deviceWarningsChanged)
        {
            if (deviceWarningsChanged.DeviceNameOfWarning != _device.Name) return;
            WarningsCollection.Clear();
            if (_deviceWarningsService.GetIsDeviceWarningRegistered(_device.Name, DatasetKeys.DataSetWarningKeys.DataSetLoadErrorWarningTagKey))
            {
                IsReportWarning = true;
                WarningsCollection.Add(_deviceWarningsService.GetWarningMassage(_device.Name, DatasetKeys.DataSetWarningKeys.DataSetLoadErrorWarningTagKey));
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

        #region publick interface
        public bool IsReportWarning
        {
            get => _isReportWarning;
            set { SetProperty(ref _isReportWarning, value); }
        }

        public ObservableCollection<string> WarningsCollection { get; }

        #endregion


        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>("IED");
            var treeItemIdentifier =
                navigationContext.BiscNavigationParameters.GetParameterByName<TreeItemIdentifier>(
                    TreeItemIdentifier.Key);
            _dataSetDetailsIdentifier = new TreeItemIdentifier(Guid.NewGuid(),treeItemIdentifier);
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
