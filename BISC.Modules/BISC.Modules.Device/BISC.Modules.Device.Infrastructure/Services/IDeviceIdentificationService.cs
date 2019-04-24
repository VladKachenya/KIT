using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.Device.Infrastructure.Services
{
    public interface IDeviceIdentificationService
    {
        void ChengeDeviceIp(IDevice device, string settableIp, ISclModel sclModel = null);
        void ChengeDeviceName(IDevice device, string settableName);

    }
}