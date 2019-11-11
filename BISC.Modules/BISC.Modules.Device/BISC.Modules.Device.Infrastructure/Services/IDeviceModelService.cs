using System;
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
        IDevice GetDeviceByGuid(ISclModel sclModel, Guid deviceGuid);
        OperationResult AddDeviceInModel(ISclModel sclModel, IDevice device, ISclModel modelFrom, bool isSubstationScl);
        OperationResult AddDeviceInModel(ISclModel sclModel, IDevice device);
        OperationResult DeleteDeviceFromModel(ISclModel sclModel, Guid deviceGuid);
        IDevice GetParentDevice(IModelElement childElement);
        string GetParentDeviceName(IModelElement childElement);


    }
}