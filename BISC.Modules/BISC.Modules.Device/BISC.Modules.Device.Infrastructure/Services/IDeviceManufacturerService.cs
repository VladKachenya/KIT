using BISC.Modules.Device.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Device.Infrastructure.Services
{
    public interface IDeviceManufacturerService
    {
        void GetManufacturerOfDevice(IDevice devise);
    }
}
