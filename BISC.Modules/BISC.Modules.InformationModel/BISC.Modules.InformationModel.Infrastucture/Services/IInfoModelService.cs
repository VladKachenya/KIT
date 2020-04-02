using System;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using System.Collections.Generic;

namespace BISC.Modules.InformationModel.Infrastucture.Services
{
    public interface IInfoModelService
    {
        void AddOrReplaceLDevice(IDeviceAccessPoint deviceAccessPoint, ILDevice lDevice);
        void InitializeInfoModel(IModelElement device, string deviceName, ISclModel sclModel);
        bool ContainsDb(IModelElement modelElement);
        List<ILDevice> GetLDevicesFromDevices(IModelElement device);
        ILDevice GetZeroLDevicesOfDevice(IDevice device);
        ILDevice GetParentLDevice(IModelElement childElement);
        ILogicalNode GetParentLogicalNode(IModelElement childElement);
        List<ISettingControl> GetSettingControlsOfDevice(IModelElement device);
        string GetFullNameOfLogicalNode(ILogicalNode logicalNode);
        void UpdateLnTypesOfDevice(IDevice device, string newDeviceName);
        List<string> GetAllFcs(List<IDai> toList, List<ISdi> list);
        List<Tuple<string, IDai>> GetAllFcsWithDai(List<IDai> dais, List<ISdi> sdis);
    }
    
 
}