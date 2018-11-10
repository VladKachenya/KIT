using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Loading.Events;
using BISC.Modules.Device.Presentation.Services.Helpers;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Navigation;

namespace BISC.Modules.Device.Presentation.ViewModels.Restart
{
   public class DeviceRestartViewModel:NavigationViewModelBase
    {
        private readonly IGlobalEventsService _globalEventsService;
        private int _totalProgress;
        private int _currentProgress;
        private bool _isIntermetiateProgress;
        private bool _isRestartingInProgress;
        private bool _haveConflicts;
        private string _deviceName;
        private RestartDeviceEntity _restartDeviceEntity;

        public DeviceRestartViewModel(IGlobalEventsService globalEventsService,ICommandFactory commandFactory)
        {
            _globalEventsService = globalEventsService;
            CancelLoadingCommand = commandFactory.CreatePresentationCommand(OnCancel);
        }

        private void OnCancel()
        {
            
        }

        #region Overrides of NavigationViewModelBase

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            _restartDeviceEntity = navigationContext.BiscNavigationParameters.GetParameterByName<RestartDeviceEntity>(DeviceKeys.RestartDeviceEntityKey);
            DeviceName = _restartDeviceEntity.DeviceName;
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
            base.OnDeactivate();
        }

        private void OnDeviceLoadingEvent(DeviceLoadingEvent deviceLoadingEvent)
        {
           if(_restartDeviceEntity.Ip!=deviceLoadingEvent.Ip)return;
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
            }
        }

        public override void OnActivate()
        {
            _globalEventsService.Subscribe<DeviceLoadingEvent>(OnDeviceLoadingEvent);
            base.OnActivate();
        }

    }
}
