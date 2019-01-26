using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Device.Infrastructure.Events;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Tree
{
    public class GooseGroupTreeItemViewModel : NavigationViewModelBase
    {
        private readonly ITabManagementService _tabManagementService;
        private readonly IDeviceWarningsService _deviceWarningsService;
        private readonly IGlobalEventsService _globalEventsService;
        private IDevice _device;

        private TreeItemIdentifier _subscriptionIdentifier;
        private TreeItemIdentifier _matrixIdentifier;
        private TreeItemIdentifier _gooseEditIdentifier;
        private bool _isReportWarning;


        public GooseGroupTreeItemViewModel(ICommandFactory commandFactory, ITabManagementService tabManagementService, IDeviceWarningsService deviceWarningsService,IGlobalEventsService globalEventsService)
        {
            _tabManagementService = tabManagementService;
            _deviceWarningsService = deviceWarningsService;
            _globalEventsService = globalEventsService;
            NavigateToGooseControlsCommand = commandFactory.CreatePresentationCommand(NavigateToGooseControls);
            NavigateToSubscriptionCommand = commandFactory.CreatePresentationCommand(OnNavigateToSubscription);
            NavigateToMatrixCommand = commandFactory.CreatePresentationCommand(OnNavigateToMatrix);
            WarningsCollection=new ObservableCollection<string>();
        }
        private void OnDeviceWarningsChanged(DeviceWarningsChanged deviceWarningsChanged)
        {
            if (deviceWarningsChanged.DeviceNameOfWarning != _device.Name) return;
            WarningsCollection.Clear();
            if (_deviceWarningsService.GetIsDeviceWarningRegistered(_device.Name, GooseKeys.GooseWarningKeys.GooseSavedFtpKey))
            {
                IsReportWarning = true;
                WarningsCollection.Add(_deviceWarningsService.GetWarningMassage(_device.Name, GooseKeys.GooseWarningKeys.GooseSavedFtpKey));
            }
            if (_deviceWarningsService.GetIsDeviceWarningRegistered(_device.Name, GooseKeys.GooseWarningKeys.ErrorGettingGooseOutOfDeviceKey))
            {
                IsReportWarning = true;
                WarningsCollection.Add(_deviceWarningsService.GetWarningMassage(_device.Name, GooseKeys.GooseWarningKeys.ErrorGettingGooseOutOfDeviceKey));
            }

        }
        private void OnNavigateToMatrix()
        {
            BiscNavigationParameters biscNavigationParameters = new BiscNavigationParameters();
            biscNavigationParameters.AddParameterByName("IED", _device);
            _tabManagementService.NavigateToTab(GooseKeys.GoosePresentationKeys.GooseMatrixTabKey, biscNavigationParameters, $"Goose матрица {_device.Name}", _matrixIdentifier);
        }

        private void OnNavigateToSubscription()
        {
            BiscNavigationParameters biscNavigationParameters=new BiscNavigationParameters();
            biscNavigationParameters.AddParameterByName("IED", _device);
            _tabManagementService.NavigateToTab(GooseKeys.GoosePresentationKeys.GooseSubscriptionTabKey, biscNavigationParameters, $"Подписка {_device.Name}", _subscriptionIdentifier);
        }

        private void NavigateToGooseControls()
        {
            BiscNavigationParameters biscNavigationParameters = new BiscNavigationParameters();
            biscNavigationParameters.AddParameterByName("IED", _device);
            _tabManagementService.NavigateToTab(GooseKeys.GoosePresentationKeys.GooseControlsTabKey, biscNavigationParameters, $"Блоки управления GOOSE {_device.Name}", _gooseEditIdentifier);
        }

        public ICommand NavigateToGooseControlsCommand { get; }
        public ICommand NavigateToSubscriptionCommand { get; }
        public ICommand NavigateToMatrixCommand { get; }
        public bool IsReportWarning
        {
            get => _isReportWarning;
            set { SetProperty(ref _isReportWarning, value); }
        }
        public ObservableCollection<string> WarningsCollection { get; }
        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>("IED");
            var treeItemIdentifier =
                navigationContext.BiscNavigationParameters.GetParameterByName<TreeItemIdentifier>(
                    TreeItemIdentifier.Key);
            _subscriptionIdentifier=new TreeItemIdentifier(Guid.NewGuid(),treeItemIdentifier);
            _matrixIdentifier = new TreeItemIdentifier(Guid.NewGuid(), treeItemIdentifier);
            _gooseEditIdentifier = new TreeItemIdentifier(Guid.NewGuid(), treeItemIdentifier);
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