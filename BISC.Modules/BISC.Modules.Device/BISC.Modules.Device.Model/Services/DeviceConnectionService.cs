using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;

namespace BISC.Modules.Device.Model.Services
{
   public class DeviceConnectionService: IDeviceConnectionService
    {
        public Task<IDevice> ConnectDevice(string ip)
        {
            throw new NotImplementedException();
        }

        public Task ConnectExistingDevice()
        {
            throw new NotImplementedException();
        }
    }
}
