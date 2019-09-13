using BISC.Infrastructure.Global.Services;
using BISC.Modules.Device.Infrastructure.Events;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Commands;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using BISC.Modules.Device.Infrastructure.Keys;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Tree
{
    public class GooseGroupTreeItemViewModel : NavigationViewModelBase
    {
        private readonly ITabManagementService _tabManagementService;
        private readonly IDeviceWarningsService _deviceWarningsService;
        private readonly IGlobalEventsService _globalEventsService;
        private IDevice _device;

        private UiEntityIdentifier _matrixIdentifier;
        private UiEntityIdentifier _gooseEditIdentifier;
        private bool _isReportWarning;

        private bool _isNavigateToMatrixCommandEneble = true;


        public GooseGroupTreeItemViewModel(ICommandFactory commandFactory, ITabManagementService tabManagementService,
            IDeviceWarningsService deviceWarningsService, IGlobalEventsService globalEventsService)
            : base(null)
        {
            _tabManagementService = tabManagementService;
            _deviceWarningsService = deviceWarningsService;
            _globalEventsService = globalEventsService;

            NavigateToGooseControlsCommand = commandFactory.CreatePresentationCommand(NavigateToGooseControls);
            NavigateToMatrixCommand = commandFactory.CreatePresentationCommand(OnNavigateToMatrix, () => _isNavigateToMatrixCommandEneble);
            WarningsCollection = new ObservableCollection<string>();
        }
        private void OnDeviceWarningsChanged(DeviceWarningsChanged deviceWarningsChanged)
        {
            if (deviceWarningsChanged.DeviceGuid != _device.DeviceGuid)
            {
                return;
            }

            WarningsCollection.Clear();
            if (_deviceWarningsService.GetIsDeviceWarningRegistered(_device.DeviceGuid, GooseKeys.GooseWarningKeys.GooseSavedFtpKey))
            {
                IsReportWarning = true;
                WarningsCollection.Add(_deviceWarningsService.GetWarningMassage(_device.DeviceGuid, GooseKeys.GooseWarningKeys.GooseSavedFtpKey));
            }
            if (_deviceWarningsService.GetIsDeviceWarningRegistered(_device.DeviceGuid, GooseKeys.GooseWarningKeys.ErrorGettingGooseOutOfDeviceKey))
            {
                IsReportWarning = true;
                WarningsCollection.Add(_deviceWarningsService.GetWarningMassage(_device.DeviceGuid, GooseKeys.GooseWarningKeys.ErrorGettingGooseOutOfDeviceKey));
            }
            if (_deviceWarningsService.GetIsDeviceWarningRegistered(_device.DeviceGuid, GooseKeys.GooseWarningKeys.GooseControlUnsavedWarningTagKey))
            {
                IsReportWarning = true;
                WarningsCollection.Add(_deviceWarningsService.GetWarningMassage(_device.DeviceGuid, GooseKeys.GooseWarningKeys.GooseControlUnsavedWarningTagKey));
            }
            if (_deviceWarningsService.GetIsDeviceWarningRegistered(_device.DeviceGuid, GooseKeys.GooseWarningKeys.GooseSubscriptionUnsavedWarningTagKey))
            {
                IsReportWarning = true;
                WarningsCollection.Add(_deviceWarningsService.GetWarningMassage(_device.DeviceGuid, GooseKeys.GooseWarningKeys.GooseSubscriptionUnsavedWarningTagKey));
            }

        }
        private void OnNavigateToMatrix()
        {
            BiscNavigationParameters biscNavigationParameters = new BiscNavigationParameters();
            biscNavigationParameters.AddParameterByName("IED", _device);
            _tabManagementService.NavigateToTab(GooseKeys.GoosePresentationKeys.GooseMatrixTabKey, biscNavigationParameters, $"Goose матрица {_device.Name}", _matrixIdentifier);
        }

        private void NavigateToGooseControls()
        {
            BiscNavigationParameters biscNavigationParameters = new BiscNavigationParameters();
            biscNavigationParameters.AddParameterByName("IED", _device);
            _tabManagementService.NavigateToTab(GooseKeys.GoosePresentationKeys.GooseControlsTabKey, biscNavigationParameters, $"Блоки управления GOOSE {_device.Name}", _gooseEditIdentifier);
        }

        public ICommand NavigateToGooseControlsCommand { get; }
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
            if (!(_device.Type == DeviceKeys.DeviceTypes.MR761 ||
                  _device.Type == DeviceKeys.DeviceTypes.MR762 ||
                  _device.Type == DeviceKeys.DeviceTypes.MR763 ||
                  _device.Type == DeviceKeys.DeviceTypes.MR771 ||
                  _device.Type == DeviceKeys.DeviceTypes.MR801 && _device.RevisionDetails.CompareVersionTo(23, 10) >= 0 ||
                  _device.Type == DeviceKeys.DeviceTypes.MR761OBR && _device.RevisionDetails.CompareVersionTo(24, 0) >= 0 ||
                  _device.Type == DeviceKeys.DeviceTypes.MR5))
            {
                _isNavigateToMatrixCommandEneble = false;
                (NavigateToMatrixCommand as IPresentationCommand)?.RaiseCanExecute();
            }

            var treeItemIdentifier =
                navigationContext.BiscNavigationParameters.GetParameterByName<UiEntityIdentifier>(
                    UiEntityIdentifier.Key);
            _matrixIdentifier = new UiEntityIdentifier(Guid.NewGuid(), treeItemIdentifier);
            _gooseEditIdentifier = new UiEntityIdentifier(Guid.NewGuid(), treeItemIdentifier);
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