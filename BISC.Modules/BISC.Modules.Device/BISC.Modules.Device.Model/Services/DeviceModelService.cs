using System;
using BISC.Infrastructure.Global.Common;
using BISC.Model.Global.Common;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Project.Communication;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using System.Collections.Generic;
using System.Linq;
using BISC.Modules.Gooses.Infrastructure.Services;

namespace BISC.Modules.Device.Model.Services
{
    public class DeviceModelService : IDeviceModelService
    {
        private readonly ISclCommunicationModelService _sclCommunicationModelService;
        private readonly IDataTypeTemplatesModelService _dataTypeTemplatesModelService;


        public DeviceModelService(
            ISclCommunicationModelService sclCommunicationModelService, 
            IDataTypeTemplatesModelService dataTypeTemplatesModelService)
        {
            _sclCommunicationModelService = sclCommunicationModelService;
            _dataTypeTemplatesModelService = dataTypeTemplatesModelService;
        }

        void SetSclModelForModelElement(IModelElement element, ISclModel sclModel)
        {
            if (element.ParentModelElement == null)
            {
                return;
            }
            else if (element.ParentModelElement is ISclModel)
            {
                element.ParentModelElement = sclModel;
                return;
            }
            else
            {
                SetSclModelForModelElement(element.ParentModelElement, sclModel);
            }
        }


        public List<IDevice> GetDevicesFromModel(ISclModel sclModel)
        {
            var devices = new List<IDevice>();
            foreach (var modelChildModelElement in sclModel.ChildModelElements)
            {
                if (modelChildModelElement.ElementName == DeviceKeys.DeviceModelKey)
                {
                    devices.Add(modelChildModelElement as IDevice);
                }
            }
            return devices;
        }

        public IDevice GetDeviceByGuid(ISclModel sclModel, Guid deviceGuid)
        {
            return GetDevicesFromModel(sclModel).
                FirstOrDefault((device => device.DeviceGuid == deviceGuid));
        }

        public IDevice GetDeviceByIp(ISclModel sclModel, string deviceIp)
        {
            return GetDevicesFromModel(sclModel).
                FirstOrDefault((device => device.Ip == deviceIp));
        }

        public OperationResult AddDeviceInModel(ISclModel sclModel, IDevice device, ISclModel modelFrom, bool isSubstationScl)
        {
            if (GetIsDeviceExists(sclModel, device.DeviceGuid))
            {
                return new OperationResult($"Устройство с именем {device.Name} уже существует в модели");
            }

            if (string.IsNullOrEmpty(device.Ip))
            {
                device.Ip = _sclCommunicationModelService.GetIpOfDevice(device.Name, modelFrom);
            }

            if (!isSubstationScl)
            {
                _dataTypeTemplatesModelService.MergeDataTypeTemplates(sclModel, modelFrom);
            }
            else
            {
                _dataTypeTemplatesModelService.MergeDataTypeTemplatesOfDevice(sclModel, modelFrom, device);
            }

            _sclCommunicationModelService.AddConnectedAccessPoint(sclModel, FindConnectedAccessPointOfTheDevice(modelFrom, device.Name));
            sclModel.ChildModelElements.Add(device);

            //It is too important
            SetSclModelForModelElement(device, sclModel);

            //Adding Goose subscription to device
            //var biscProject = sclModel.GetFirstParentOfType<IBiscProject>();
            //if (biscProject != null)
            //{
            //    // Устанавливаем полученные Goose подписки из sclMode в наш текущий проект
            //    _goosesModelService.SetGooseInputModelInfosToProject(_biscProject, device,
            //        _goosesModelService.GetGooseInputModelInfos(device, biscProject));
            //    // Устанавливаем полученную Goose матрицн из sclMode в наш текущий проект
            //    _gooseMatrixFtpService.SetGooseMatrixFtpForDevice(device,
            //        _gooseMatrixFtpService.GetGooseMatrixFtpForDevice(device, biscProject));
            //}

            return OperationResult.SucceedResult;
        }

        public OperationResult AddDeviceInModel(ISclModel sclModel, IDevice device)
        {
            if (GetIsDeviceExists(sclModel, device.DeviceGuid))
            {
                return new OperationResult($"Устройство с именем {device.Name} уже существует в модели");
            }
            _sclCommunicationModelService.AddDefaultConnectedAccessPointForDevice(sclModel, device.Name, device.Ip);
            sclModel.ChildModelElements.Add(device);
            device.ParentModelElement = sclModel;
            return OperationResult.SucceedResult;
        }


        private IConnectedAccessPoint FindConnectedAccessPointOfTheDevice(ISclModel modelFrom, string deviceName)
        {
            IConnectedAccessPoint connectedAccessPointFinded = null;
            foreach (var childModelElement in modelFrom.ChildModelElements)
            {
                if (!(childModelElement is ISclCommunicationModel communicationModel))
                {
                    continue;
                }

                foreach (var subNetwork in communicationModel.SubNetworks)
                {
                    foreach (var connectedAccessPoint in subNetwork.ConnectedAccessPoints)
                    {
                        if (connectedAccessPoint.IedName == deviceName)
                        {
                            connectedAccessPointFinded = connectedAccessPoint;
                        }
                    }
                }
            }

            return connectedAccessPointFinded;
        }

        private bool GetIsDeviceExists(ISclModel sclModel, Guid deviceGuid)
        {
            var isExists = false;
            foreach (var modelChildModelElement in sclModel.ChildModelElements)
            {
                if (modelChildModelElement.ElementName == DeviceKeys.DeviceModelKey)
                {
                    if ((modelChildModelElement as IDevice)?.DeviceGuid == deviceGuid)
                    {
                        isExists = true;
                    }
                }
            }

            return isExists;
        }
        public OperationResult DeleteDeviceFromModel(ISclModel sclModel, Guid deviceGuid)
        {
            if (!GetIsDeviceExists(sclModel, deviceGuid))
            {
                return new OperationResult($"Удаляемого устройства не существет в модели");
            };

            List<ILDevice> devicesToRemove = new List<ILDevice>();
            List<ILDevice> devicesToLeave = new List<ILDevice>();

            var device = GetDevicesFromModel(sclModel).First((device1 => device1.DeviceGuid == deviceGuid));
            _sclCommunicationModelService.DeleteAccessPoint(sclModel, device.Name);

            device.FillChildModelElements(devicesToRemove);
            sclModel.FillChildModelElements(devicesToLeave);
            devicesToLeave = devicesToLeave.Where((lDevice => !devicesToRemove.Any((remove => lDevice == remove))))
                .ToList();

            _dataTypeTemplatesModelService.FilterDataTypeTemplates(sclModel.ChildModelElements.First((element => element is IDataTypeTemplates)) as IDataTypeTemplates, devicesToRemove, devicesToLeave);
            sclModel.ChildModelElements.Remove(device);
            return OperationResult.SucceedResult;
        }



        public IDevice GetParentDevice(IModelElement childElement)
        {
            var parientElement = childElement.ParentModelElement;
            if (parientElement == null)
            {
                return null;
            }
            else if (parientElement is IDevice)
            {
                return parientElement as IDevice;
            }
            else
            {
                return GetParentDevice(parientElement);
            }
        }

        public string GetParentDeviceName(IModelElement childElement)
        {
            return GetParentDevice(childElement)?.Name;
        }
    }
}