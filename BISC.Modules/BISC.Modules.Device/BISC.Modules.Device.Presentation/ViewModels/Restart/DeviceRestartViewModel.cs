using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Connection.Infrastructure.Events;
using BISC.Modules.Device.Infrastructure.Events;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Loading.Events;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Device.Presentation.Interfaces.Services;
using BISC.Modules.Device.Presentation.Services.Helpers;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Device.Presentation.ViewModels.Restart
{
    public class DeviceRestartViewModel : NavigationViewModelBase
    {
        private readonly IGlobalEventsService _globalEventsService;
        private readonly INavigationService _navigationService;
        private readonly IDeviceAddingService _deviceAddingService;
        private readonly IDeviceWarningsService _deviceWarningsService;
        private readonly IInjectionContainer _injectionContainer;
        private readonly ITreeManagementService _treeManagementService;
        private readonly IDeviceReconnectionService _deviceReconnectionService;
        private int _totalProgress;
        private int _currentProgress;
        private bool _isIntermetiateProgress;
        private bool _isRestartingInProgress;
        private bool _haveConflicts;
        private string _deviceName;
        private RestartDeviceContext _restartDeviceContext;
        private List<IElementConflictResolver> _elementConflictResolvers;

        public DeviceRestartViewModel(IGlobalEventsService globalEventsService, ICommandFactory commandFactory, INavigationService navigationService
            ,IDeviceAddingService deviceAddingService,IDeviceWarningsService deviceWarningsService,IInjectionContainer injectionContainer,
            ITreeManagementService treeManagementService,IDeviceReconnectionService deviceReconnectionService)
        {
            _globalEventsService = globalEventsService;
            _navigationService = navigationService;
            _deviceAddingService = deviceAddingService;
            _deviceWarningsService = deviceWarningsService;
            _injectionContainer = injectionContainer;
            _treeManagementService = treeManagementService;
            _deviceReconnectionService = deviceReconnectionService;
            CancelLoadingCommand = commandFactory.CreatePresentationCommand(OnCancel);
            ResolveConflictsCommand = commandFactory.CreatePresentationCommand(OnResolveConflicts);
            _elementConflictResolvers = injectionContainer.ResolveAll(typeof(IElementConflictResolver))
                .Cast<IElementConflictResolver>().ToList();
        }

        private async void OnResolveConflicts()
        {
            BiscNavigationParameters biscNavigationParameters = new BiscNavigationParameters().AddParameterByName(DeviceKeys.DeviceConflictContextKey, _restartDeviceContext.DeviceConflictContext);
            await _navigationService.NavigateViewToGlobalRegion(DeviceKeys.DeviceConflictsViewKey, biscNavigationParameters);
            var hasConflics = false;
            if (_restartDeviceContext.DeviceConflictContext.IsRestartNeeded)
            {
               await _deviceReconnectionService.RestartDevice(_restartDeviceContext.Device,
                    _restartDeviceContext.UiEntityIdentifier);
                return;
            }
            _elementConflictResolvers.ForEach((resolver =>
            {
                if (resolver.GetIfConflictsExists(_restartDeviceContext.Device.DeviceGuid,
                    _restartDeviceContext.DeviceConflictContext.SclModelDevice, _restartDeviceContext.DeviceConflictContext.SclModelProject))
                {
                    hasConflics = true;
                }
            }));
            if (!hasConflics)
            {
                _treeManagementService.DeleteTreeItem(_restartDeviceContext.UiEntityIdentifier);

                _deviceAddingService.AddDeviceToTree(_restartDeviceContext.Device);
                _deviceWarningsService.ClearDeviceWarningsOfDevice(_restartDeviceContext.Device.DeviceGuid);
            }
        }

        private void OnCancelEvent(LoadErrorEvent loadErrorEvent)
        {
            if(loadErrorEvent.Ip != _restartDeviceContext.Device.Ip || loadErrorEvent.DeviceGuid != _restartDeviceContext.Device.DeviceGuid) return;
            _treeManagementService.DeleteTreeItem(_restartDeviceContext.UiEntityIdentifier);
            _deviceAddingService.AddDeviceToTree(_restartDeviceContext.Device);
        }

        private void OnCancel()
        {
            _restartDeviceContext.Cts.Cancel();
            _treeManagementService.DeleteTreeItem(_restartDeviceContext.UiEntityIdentifier);
            _deviceAddingService.AddDeviceToTree(_restartDeviceContext.Device);
        }

        #region Overrides of NavigationViewModelBase

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            _restartDeviceContext = navigationContext.BiscNavigationParameters.GetParameterByName<RestartDeviceContext>(DeviceKeys.RestartDeviceContextKey);
            DeviceName = _restartDeviceContext.Device.Name;
            HaveConflicts = false;
            IsRestartingInProgress = true;
            IsIntermetiateProgress = true;
        }

        #endregion


        public string DeviceName
        {
            get => _deviceName;
            set => SetProperty(ref _deviceName, value);
        }

        public bool HaveConflicts
        {
            get => _haveConflicts;
            set => SetProperty(ref _haveConflicts, value);
        }

        public ICommand CancelLoadingCommand { get; }
        public ICommand ResolveConflictsCommand { get; }


        public bool IsRestartingInProgress
        {
            get => _isRestartingInProgress;
            set => SetProperty(ref _isRestartingInProgress, value);
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
        public override void OnDeactivate()
        {
            _globalEventsService.Unsubscribe<DeviceLoadingEvent>(OnDeviceLoadingEvent);
            _globalEventsService.Unsubscribe<LoadErrorEvent>(OnCancelEvent);

            base.OnDeactivate();
        }

        private void OnDeviceLoadingEvent(DeviceLoadingEvent deviceLoadingEvent)
        {
            if (_restartDeviceContext.Device.DeviceGuid != deviceLoadingEvent.DeviceGuid) return;
            if (deviceLoadingEvent.TotalProgressCount.HasValue)
            {
                TotalProgress = deviceLoadingEvent.TotalProgressCount.Value;
            }
            if (deviceLoadingEvent.CurrentProgressCount.HasValue)
            {
                CurrentProgress = deviceLoadingEvent.CurrentProgressCount.Value;
                IsIntermetiateProgress = false;
            }
            if (deviceLoadingEvent.IsFinished.HasValue)
            {
                IsRestartingInProgress = !deviceLoadingEvent.IsFinished.Value;
                HaveConflicts = _restartDeviceContext.HaveConflicts;
            }
        }

        public override void OnActivate()
        {
            _globalEventsService.Subscribe<DeviceLoadingEvent>(OnDeviceLoadingEvent);
            _globalEventsService.Subscribe<LoadErrorEvent>(OnCancelEvent);
            base.OnActivate();
        }

    }
}
