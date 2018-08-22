using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Project.Communication;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;

namespace BISC.Modules.Device.Model.Services
{
    public class DeviceModelService : IDeviceModelService
    {
        private readonly ISclCommunicationModelService _sclCommunicationModelService;

        public DeviceModelService(ISclCommunicationModelService sclCommunicationModelService)
        {
            _sclCommunicationModelService = sclCommunicationModelService;
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

        public OperationResult AddDeviceInModel(ISclModel sclModel, IDevice device,ISclModel modelFrom)
        {
            if (GetIsDeviceExists(sclModel, device))
            {
                return new OperationResult($"Устройство с именем {device.Name} уже существует в модели");
            }
            _sclCommunicationModelService.AddConnectedAccessPoint(sclModel,FindConnectedAccessPointOfTheDevice(modelFrom,device.Name));
            sclModel.ChildModelElements.Add(device);
            return OperationResult.SucceedResult;
        }

        public OperationResult AddDeviceInModel(ISclModel sclModel, IDevice device)
        {
            if (GetIsDeviceExists(sclModel, device))
            {
                return new OperationResult($"Устройство с именем {device.Name} уже существует в модели");
            }
            _sclCommunicationModelService.AddDefaultConnectedAccessPointForDevice(sclModel,device.Name,device.Ip);
            sclModel.ChildModelElements.Add(device);
            return OperationResult.SucceedResult;
        }


        private IConnectedAccessPoint FindConnectedAccessPointOfTheDevice(ISclModel modelFrom,string deviceName)
        {
            IConnectedAccessPoint connectedAccessPointFinded = null;
            foreach (var childModelElement in modelFrom.ChildModelElements)
            {
                if (!(childModelElement is ISclCommunicationModel communicationModel)) continue;
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

        private bool GetIsDeviceExists(ISclModel sclModel, IDevice device)
        {
            var isExists = false;
            foreach (var modelChildModelElement in sclModel.ChildModelElements)
            {
                if (modelChildModelElement.ElementName == DeviceKeys.DeviceModelKey)
                {
                    if ((modelChildModelElement as IDevice).Name == device.Name)
                    {
                        isExists = true;
                    }
                }
            }

            return isExists;
        }
        public OperationResult DeleteDeviceFromModel(ISclModel sclModel, IDevice device)
        {
            if (!GetIsDeviceExists(sclModel, device))
            {
                return new OperationResult($"Устройство с именем {device.Name} не существует в модели");
            };
            sclModel.ChildModelElements.Remove(device);
            return OperationResult.SucceedResult;

        }
    }
}