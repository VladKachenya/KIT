using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Device.Presentation.Interfaces;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Infrastructure.Tree;

namespace BISC.Modules.Device.Presentation.ViewModels.Tree
{
    public class DeviceTreeItemViewModel : NavigationViewModelBase, IDeviceTreeItemViewModel
    {
        private readonly IDeviceModelService _deviceModelService;
        private readonly IBiscProject _biscProject;
        private readonly ITreeManagementService _treeManagementService;
        private readonly ITabManagementService _tabManagementService;

        private string _deviceName;
        private IDevice _device;
        private TreeItemIdentifier _treeItemIdentifier;

        public DeviceTreeItemViewModel(ICommandFactory commandFactory, IDeviceModelService deviceModelService,
            IBiscProject biscProject, ITreeManagementService treeManagementService,ITabManagementService tabManagementService)
        {
            _deviceModelService = deviceModelService;
            _biscProject = biscProject;
            _treeManagementService = treeManagementService;
            _tabManagementService = tabManagementService;
            DeleteDeviceCommand = commandFactory.CreatePresentationCommand(OnDeleteDeviceExecute);
            NavigateToDetailsCommand = commandFactory.CreatePresentationCommand(OnNavigateToDetailsExecute);
        }

        private void OnNavigateToDetailsExecute()
        {
            BiscNavigationParameters biscNavigationParameters = new BiscNavigationParameters();
            biscNavigationParameters.AddParameterByName(DeviceKeys.DeviceModelKey, _device);
            _tabManagementService.NavigateToTab(DeviceKeys.DeviceDetailsViewKey,biscNavigationParameters,$"IED {_device.Name}",_treeItemIdentifier);
        }

        private void OnDeleteDeviceExecute()
        {
            var result = _deviceModelService.DeleteDeviceFromModel(_biscProject.MainSclModel, _device);
            if (result.IsSucceed)
            {
                _treeManagementService.DeleteTreeItem(_treeItemIdentifier);
            }
        }

        #region Implementation of IMainTreeItem


        public string DeviceName
        {
            get => _deviceName;
            set
            {
                _deviceName = value;
                OnPropertyChanged();
            }
        }

        public IDevice Device { get; set; }
        #endregion

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            IDevice device = navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>(DeviceKeys.DeviceModelKey);
            _treeItemIdentifier =
                navigationContext.BiscNavigationParameters.GetParameterByName<TreeItemIdentifier>(TreeItemIdentifier.Key);
            DeviceName = device.Name;
            _device = device;
            base.OnNavigatedTo(navigationContext);
        }

        public ICommand DeleteDeviceCommand { get; }
        public ICommand NavigateToDetailsCommand { get; }
    }
}
