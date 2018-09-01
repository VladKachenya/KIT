using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture.Elements;

namespace BISC.Modules.InformationModel.Infrastucture.Services
{
    public interface IInfoModelService
    {
        void AddOrReplaceLDevice(IDeviceAccessPoint deviceAccessPoint,ILDevice lDevice);
        void InitializeInfoModel(IModelElement device, string deviceName);
    }
}