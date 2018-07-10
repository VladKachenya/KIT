using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;

namespace BISC.Modules.Device.Model.Services
{
    public class DeviceModelService : IDeviceModelService
    {
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

        public OperationResult AddDeviceInModel(ISclModel sclModel, IDevice device)
        {
            foreach (var modelChildModelElement in sclModel.ChildModelElements)
            {
                if (modelChildModelElement.ElementName == DeviceKeys.DeviceModelKey)
                {
                    if ((modelChildModelElement as IDevice).Name == device.Name)
                    {
                        return new OperationResult($"Устройство с именем {device.Name} уже существует в модели");
                    }
                }
            }
            sclModel.ChildModelElements.Add(device);
            return OperationResult.SucceedResult;
            
        }
    }
}