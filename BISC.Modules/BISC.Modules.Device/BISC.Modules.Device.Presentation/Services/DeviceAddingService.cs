using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Device.Presentation.Interfaces.Services;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Infrastructure.UiFromModel;
using System.Collections.Generic;
using System.Linq;
using BISC.Model.Infrastructure.Services.Communication;

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
        private readonly ISclCommunicationModelService _communicationModelService;
        private readonly IUserInteractionService _userInteractionService;

        public DeviceAddingService(INavigationService navigationService, IDeviceModelService deviceModelService,
            ILoggingService loggingService, IBiscProject biscProject, ITreeManagementService treeManagementService,
            IUiFromModelElementRegistryService uiFromModelElementRegistryService, ISclCommunicationModelService communicationModelService)
        {
            _navigationService = navigationService;
            _deviceModelService = deviceModelService;
            _loggingService = loggingService;
            _biscProject = biscProject;
            _treeManagementService = treeManagementService;
            _uiFromModelElementRegistryService = uiFromModelElementRegistryService;
            _communicationModelService = communicationModelService;
        }

        public async void OpenDeviceAddingView()
        {
            await _navigationService.NavigateViewToGlobalRegion(DeviceKeys.DeviceAddingViewKey);
        }

        public void AddDevicesInProject(List<IDevice> devicesToAdd, ISclModel modelFrom)
        {
            foreach (var device in devicesToAdd)
            {
                var deviceNameInProject = _deviceModelService.GetDevicesFromModel(_biscProject.MainSclModel.Value);
                if (deviceNameInProject.Select(dev => dev.Name).Any(devN => devN == device.Name))
                {
                    var mes = $"Устройство с именем {device.Name} уже существует в проекте";
                    _loggingService.LogMessage(mes, SeverityEnum.Info);
                    continue;
                }

                if (deviceNameInProject.Any(d =>
                    _communicationModelService.GetIpOfDevice(d.Name, _biscProject.MainSclModel.Value) ==
                    _communicationModelService.GetIpOfDevice(device.Name, modelFrom)))
                {
                    var mes = $"Устройство с IP {_communicationModelService.GetIpOfDevice(device.Name, modelFrom)} уже существует в проекте";
                    _loggingService.LogMessage(mes, SeverityEnum.Info);
                    continue;
                }


                var res = _deviceModelService.AddDeviceInModel(_biscProject.MainSclModel.Value, device, modelFrom);
                if (!res.IsSucceed)
                {
                    _loggingService.LogMessage(res.GetFirstError(), SeverityEnum.Warning);
                }
                else
                {
                    AddDeviceToTree(device);
                }
            }
        }


        public UiEntityIdentifier HandleModelElement(IModelElement modelElement, UiEntityIdentifier parentTreeId, string uiKey)
        {
            if (parentTreeId != null)
            {
                return null;
            }

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

        public void AddDeviceToTree(IDevice device, int? index = null)
        {
            BiscNavigationParameters navigationParameters = new BiscNavigationParameters();
            navigationParameters.AddParameterByName(DeviceKeys.DeviceModelKey, device);
            var resultTreeItem =
                _treeManagementService.AddTreeItem(navigationParameters, DeviceKeys.DeviceTreeItemViewKey, null, device.DeviceGuid.ToString(), index);
            _uiFromModelElementRegistryService.TryHandleModelElementInUiByKey(device, resultTreeItem, "IED");
        }

        public void AddDeviceToTreeIfMissing(IDevice device, int? index = null)
        {
            if (_treeManagementService.GetDeviceTreeItem(device.DeviceGuid) == null)
            {
                AddDeviceToTree(device, index);
            }
        }
    }
}