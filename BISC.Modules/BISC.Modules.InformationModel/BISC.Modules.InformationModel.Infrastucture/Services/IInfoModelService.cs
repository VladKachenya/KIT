using System.Collections.Generic;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture.Elements;

namespace BISC.Modules.InformationModel.Infrastucture.Services
{
public interface IInfoModelService
    {
        void AddOrReplaceLDevice(IDeviceAccessPoint deviceAccessPoint,ILDevice lDevice);
        void InitializeInfoModel(IModelElement device, string deviceName,ISclModel sclModel);
        List<ILDevice> GetLDevicesFromDevices(IModelElement device);
        ILDevice GetZeroLDevicesOfDevice(IDevice device);
        ILDevice GetParentLDevice(IModelElement childElement);
        ILogicalNode GetParentLogicalNode(IModelElement childElement);
        List<ISettingControl> GetSettingControlsOfDevice(IModelElement device);
        string GetFullNameOfLogicalNode(ILogicalNode logicalNode);
    }
}