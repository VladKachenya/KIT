using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Device.Infrastructure.Loading.Events;
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
        private CancellationTokenSource _cts;
        private string _ip;


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
            DeviceName = navigationContext.BiscNavigationParameters.GetParameterByName<string>("name");
            _cts = navigationContext.BiscNavigationParameters.GetParameterByName<CancellationTokenSource>("cts");
            _ip = navigationContext.BiscNavigationParameters.GetParameterByName<string>("ip");
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
           if(_ip!=deviceLoadingEvent.Ip)return;
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
