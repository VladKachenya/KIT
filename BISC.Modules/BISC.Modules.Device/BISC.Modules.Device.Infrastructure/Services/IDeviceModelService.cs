using System.Collections.Generic;
using BISC.Infrastructure.Global.Common;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.Device.Infrastructure.Services
{
    public interface IDeviceModelService
    {
        List<IDevice> GetDevicesFromModel(ISclModel sclModel);
        IDevice GetDeviceByName(ISclModel sclModel, string deviceName);
        OperationResult AddDeviceInModel(ISclModel sclModel, IDevice device,ISclModel modelFrom);
        OperationResult AddDeviceInModel(ISclModel sclModel, IDevice device);
        OperationResult DeleteDeviceFromModel(ISclModel sclModel, string deviceName);
        IDevice GetParentDevice(IModelElement childElement);
        string GetParentDeviceName(IModelElement childElement);


    }
}