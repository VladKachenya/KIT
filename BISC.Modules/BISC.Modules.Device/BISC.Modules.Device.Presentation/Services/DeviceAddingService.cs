using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Device.Presentation.Interfaces.Services;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Infrastructure.UiFromModel;

namespace BISC.Modules.Device.Presentation.Services
{
    public class DeviceAddingService : IDeviceAddingService
    {
        private readonly INavigationService _navigationService;
        private readonly IDeviceModelService _deviceModelService;
        private readonly ILoggingService _loggingService;
        private readonly IBiscProject _biscProject;
        private readonly ITreeManagementService _treeManagementService;
        private readonly IUiFromModelElementRegistryService _uiFromModelElementRegistryService;

        public DeviceAddingService(INavigationService navigationService, IDeviceModelService deviceModelService,
            ILoggingService loggingService, IBiscProject biscProject, ITreeManagementService
                treeManagementService, IUiFromModelElementRegistryService uiFromModelElementRegistryService)
        {
            _navigationService = navigationService;
            _deviceModelService = deviceModelService;
            _loggingService = loggingService;
            _biscProject = biscProject;
            _treeManagementService = treeManagementService;
            _uiFromModelElementRegistryService = uiFromModelElementRegistryService;
        }

        public async void OpenDeviceAddingView()
        {
            await _navigationService.NavigateViewToGlobalRegion(DeviceKeys.DeviceAddingViewKey);
        }

        public void AddDevicesInProject(List<IDevice> devicesToAdd,ISclModel modelFrom)
        {
            foreach (var device in devicesToAdd)
            {
                var res = _deviceModelService.AddDeviceInModel(_biscProject.MainSclModel.Value, device,modelFrom);
                if (!res.IsSucceed)
                {
                    _loggingService.LogMessage(res.GetFirstError(),SeverityEnum.Warning);
                }
                else
                {
                    AddDeviceToTree(device);
                }
            }
        }

        private void AddDeviceToTree(IDevice device)
        {
            BiscNavigationParameters navigationParameters = new BiscNavigationParameters();
            navigationParameters.AddParameterByName(DeviceKeys.DeviceModelKey, device);
            var resultTreeItem =
                _treeManagementService.AddTreeItem(navigationParameters, DeviceKeys.DeviceTreeItemViewKey, null);
            _uiFromModelElementRegistryService.TryHandleModelElementInUiByKey(device, resultTreeItem,"IED");

        }

        public TreeItemIdentifier HandleModelElement(IModelElement modelElement, TreeItemIdentifier parentTreeId,string uiKey)
        {
            if(parentTreeId!=null)return null;
            var sclModel = modelElement as ISclModel;
            sclModel?.ChildModelElements.ForEach(element =>
            {
                if (element.ElementName == DeviceKeys.DeviceModelKey)
                {
                    AddDeviceToTree(element as IDevice);
                }
            });
            _uiFromModelElementRegistryService.TryHandleModelElementInUiByKey(modelElement, parentTreeId, "IED");
            return null;
        }
    }
}