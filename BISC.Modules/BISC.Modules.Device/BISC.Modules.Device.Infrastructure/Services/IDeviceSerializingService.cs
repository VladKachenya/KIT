using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.Device.Infrastructure.Services
{
    public interface IDeviceSerializingService
    {
        void SerializeCidSingleDevice(IDevice device,string filePath);
    }
}