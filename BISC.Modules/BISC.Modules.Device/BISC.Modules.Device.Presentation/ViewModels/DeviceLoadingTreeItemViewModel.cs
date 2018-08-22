using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Loading;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Navigation;

namespace BISC.Modules.Device.Presentation.ViewModels
{
    public class DeviceLoadingTreeItemViewModel : NavigationViewModelBase
    {
        private readonly IDeviceLoadingService _deviceLoadingService;
        private string _deviceName;
        private BiscNavigationContext _navigationContext;
        private int _currentProgress;
        private int _totalProgress;
        private bool _isIntermetiateProgress;

        public DeviceLoadingTreeItemViewModel(IDeviceLoadingService deviceLoadingService)
        {
            _deviceLoadingService = deviceLoadingService;
        }

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
            set => SetProperty(ref _isIntermetiateProgress,value);
        }

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _navigationContext = navigationContext;
            IDevice device = _navigationContext.BiscNavigationParameters
                .GetParameterByName<IDevice>(DeviceKeys.DeviceModelKey);
            DeviceName = device.Name;
            LoadDeviceAsync(device);
            base.OnNavigatedTo(navigationContext);
        }

        private async void LoadDeviceAsync(IDevice device)
        {
            IsIntermetiateProgress = true;
            await _deviceLoadingService.LoadElements(device, new Progress<DeviceLoadingEvent>(OnDeviceLoadingEvent));
        }

        private void OnDeviceLoadingEvent(DeviceLoadingEvent deviceLoadingEvent)
        {
            IsIntermetiateProgress = false;
            TotalProgress = deviceLoadingEvent.FullItemsCount;
            CurrentProgress = deviceLoadingEvent.CurrentItemsCount;
        }
    }
}
