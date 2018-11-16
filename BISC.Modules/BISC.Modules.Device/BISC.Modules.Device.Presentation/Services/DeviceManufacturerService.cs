using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;

namespace BISC.Modules.Device.Presentation.Services
{
    public class DeviceManufacturerService : IDeviceManufacturerService
    {
        public void GetManufacturerOfDevice(IDevice devise)
        {
            if (devise.Name.IndexOf("MR") == 0)
                devise.Manufacturer = DeviceKeys.DeviceManufacturer.BemnManufacturer;
            else
                devise.Manufacturer = DeviceKeys.DeviceManufacturer.UnknowManufacturer;
        }
    }
}