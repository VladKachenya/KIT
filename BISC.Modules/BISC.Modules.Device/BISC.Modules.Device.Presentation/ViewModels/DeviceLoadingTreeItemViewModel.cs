using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Events;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Loading;
using BISC.Modules.Device.Infrastructure.Loading.Events;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Device.Presentation.Interfaces.Services;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Device.Presentation.ViewModels
{
    public class DeviceLoadingTreeItemViewModel : NavigationViewModelBase
    {
        private readonly IDeviceModelService _deviceModelService;
        private readonly IBiscProject _biscProject;
        private readonly IGlobalEventsService _globalEventsService;
        private readonly ITreeManagementService _treeManagementService;
        private string _deviceName;
        private BiscNavigationContext _navigationContext;
        private int _currentProgress;
        private int _totalProgress;
        private bool _isIntermetiateProgress;
        private IDevice _device;
        private CancellationTokenSource _cts;

        public DeviceLoadingTreeItemViewModel(IDeviceModelService deviceModelService, IBiscProject biscProject,
            IGlobalEventsService globalEventsService,ICommandFactory commandFactory,ITreeManagementService treeManagementService)
        {
            _deviceModelService = deviceModelService;
            _biscProject = biscProject;
            _globalEventsService = globalEventsService;
            _treeManagementService = treeManagementService;
            CancelLoadingCommand = commandFactory.CreatePresentationCommand(OnCancelLoading);
        }

        private void OnCancelLoading()
        {
            _cts.Cancel();
            _treeManagementService.DeleteTreeItem(
                _navigationContext.BiscNavigationParameters.GetParameterByName<TreeItemIdentifier>(TreeItemIdentifier
                    .Key));
        }

        private void OnCancelEvent(LoadErrorEvent loadErrorEvent)
        {
            if (loadErrorEvent.Ip != _device.Ip || loadErrorEvent.DeviceName != DeviceName) return;
            _treeManagementService.DeleteTreeItem(
                _navigationContext.BiscNavigationParameters.GetParameterByName<TreeItemIdentifier>(TreeItemIdentifier
                    .Key));
        }

        public ICommand CancelLoadingCommand { get; }
        public string DeviceName
        {
            get => _deviceName;
            set => SetProperty(ref _deviceName, value);
        }

        public int TotalProgress
        {
            get => _totalProgress;
            set => SetProperty(ref _totalProgress, value);
        }

        public int CurrentProgress
        {
            get => _currentProgress;
            set => SetProperty(ref _currentProgress, value);
        }

        public bool IsIntermetiateProgress
        {
            get => _isIntermetiateProgress;
            set => SetProperty(ref _isIntermetiateProgress, value);
        }

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _navigationContext = navigationContext;
            _device = _navigationContext.BiscNavigationParameters
                .GetParameterByName<IDevice>(DeviceKeys.DeviceModelKey);
            DeviceName = _device.Name;
            _cts = _navigationContext.BiscNavigationParameters.GetParameterByName<CancellationTokenSource>("cts");
            IsIntermetiateProgress = true;
            base.OnNavigatedTo(navigationContext);
        }

        private void OnDeviceLoadingEvent(DeviceLoadingEvent deviceLoadingEvent)
        {
            IsIntermetiateProgress = false;
           if(deviceLoadingEvent.Ip!=_device.Ip)return;
            if (deviceLoadingEvent.TotalProgressCount != null)
            {
                TotalProgress = deviceLoadingEvent.TotalProgressCount.Value;
            }

            if (deviceLoadingEvent.CurrentProgressCount != null)
                CurrentProgress = deviceLoadingEvent.CurrentProgressCount.Value;
            if (deviceLoadingEvent.DeviceName != null)
            {
                _device.Name = deviceLoadingEvent.DeviceName;
                DeviceName = _device.Name;
            }
        }

        #region Overrides of NavigationViewModelBase

        public override void OnDeactivate()
        {
            _globalEventsService.Unsubscribe<DeviceLoadingEvent>(OnDeviceLoadingEvent);
            base.OnDeactivate();
        }

        public override void OnActivate()
        {
            _globalEventsService.Subscribe<DeviceLoadingEvent>(OnDeviceLoadingEvent);
            base.OnActivate();
        }

        #endregion


        #region Overrides of ViewModelBase

        protected override void OnDisposing()
        {
         
            base.OnDisposing();
        }

        #endregion
    }
}
