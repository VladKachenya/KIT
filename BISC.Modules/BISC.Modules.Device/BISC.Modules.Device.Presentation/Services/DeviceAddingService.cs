using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
   public class DeviceAddingService: IDeviceAddingService
    {
        private readonly INavigationService _navigationService;
        private readonly IDeviceModelService _deviceModelService;
        private readonly IUserNotificationService _userNotificationService;
        private readonly IBiscProject _biscProject;
        private readonly ITreeManagementService _treeManagementService;

        public DeviceAddingService(INavigationService navigationService ,IDeviceModelService deviceModelService,
            IUserNotificationService userNotificationService,IBiscProject biscProject,ITreeManagementService
                treeManagementService)
        {
            _navigationService = navigationService;
            _deviceModelService = deviceModelService;
            _userNotificationService = userNotificationService;
            _biscProject = biscProject;
            _treeManagementService = treeManagementService;
        }

        public async void OpenDeviceAddingView()
        {
           await _navigationService.NavigateViewToGlobalRegion(DeviceKeys.DeviceAddingViewKey);
        }

        public void AddDevicesInProject(List<IDevice> devicesToAdd)
        {
            foreach (var device in devicesToAdd)
            {
               var res= _deviceModelService.AddDeviceInModel(_biscProject.MainSclModel, device);
                if (!res.IsSucceed)
                {
                    _userNotificationService.NotifyUserGlobal(res.GetFirstError());
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
            _treeManagementService.AddTreeItem(navigationParameters, DeviceKeys.DeviceTreeItemViewKey, null);
        }

        public void HandleModelElement(IModelElement modelElement)
        {
            var sclModel = modelElement as ISclModel;
           sclModel?.ChildModelElements.ForEach(element =>
           {
               if (element.ElementName == DeviceKeys.DeviceModelKey)
               {
                   AddDeviceToTree(element as IDevice);
               }
           });
        }
    }
}
