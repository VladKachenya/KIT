using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Loading;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Device.Presentation.Interfaces.Services;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Device.Presentation.ViewModels
{
    public class DeviceLoadingTreeItemViewModel : NavigationViewModelBase
    {
        private readonly IDeviceLoadingService _deviceLoadingService;
        private readonly IDeviceModelService _deviceModelService;
        private readonly IBiscProject _biscProject;
        private readonly ITreeManagementService _treeManagementService;
        private string _deviceName;
        private BiscNavigationContext _navigationContext;
        private int _currentProgress;
        private int _totalProgress;
        private bool _isIntermetiateProgress;
        private IDevice _device;

        public DeviceLoadingTreeItemViewModel(IDeviceLoadingService deviceLoadingService,
            IDeviceModelService deviceModelService, IBiscProject biscProject,ITreeManagementService treeManagementService,IDeviceAddingService de)
        {
            _deviceLoadingService = deviceLoadingService;
            _deviceModelService = deviceModelService;
            _biscProject = biscProject;
            _treeManagementService = treeManagementService;
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
            set => SetProperty(ref _isIntermetiateProgress, value);
        }

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _navigationContext = navigationContext;
            _device = _navigationContext.BiscNavigationParameters
                .GetParameterByName<IDevice>(DeviceKeys.DeviceModelKey);
            DeviceName = _device.Name;
            LoadDeviceAsync(_device);
            base.OnNavigatedTo(navigationContext);
        }

        private async void LoadDeviceAsync(IDevice device)
        {
            IsIntermetiateProgress = true;
            await _deviceLoadingService.LoadElements(device, new Progress<DeviceLoadingEvent>(OnDeviceLoadingEvent));
            _treeManagementService.DeleteTreeItem(_navigationContext.BiscNavigationParameters.GetParameterByName<TreeItemIdentifier>(TreeItemIdentifier.Key));
            _treeManagementService.AddTreeItem(new BiscNavigationParameters(){new BiscNavigationParameter("IED",_device)}, DeviceKeys.DeviceTreeItemViewKey, null);
        }


        private void OnDeviceLoadingEvent(DeviceLoadingEvent deviceLoadingEvent)
        {
            IsIntermetiateProgress = false;
           if(deviceLoadingEvent.FullItemsCount!=null){ TotalProgress = deviceLoadingEvent.FullItemsCount.Value;}
            CurrentProgress = deviceLoadingEvent.CurrentItemsCount;
            if (deviceLoadingEvent.DeviceNameFinded != null)
            {
                _device.Name = deviceLoadingEvent.DeviceNameFinded;
                DeviceName = _device.Name;
                _deviceModelService.AddDeviceInModel(_biscProject.MainSclModel, _device);
            }
        }
    }
}
