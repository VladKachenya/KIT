using System;
using BISC.Model.Infrastructure;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;

namespace BISC.Modules.Device.Model.Services
{
    public class DeviceSerializingService : IDeviceSerializingService
    {
        public DeviceSerializingService(IModelElementsRegistryService modelElementsRegistryService)
        {

        }

        #region Implementation of IDeviceSerializingService

        public string SerializeCidSingleDevice(IDevice device,string filePath)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}