
using BISC.Modules.Connection.Infrastructure.DeviceConnection;
using BISC.Modules.Connection.Infrastructure.Factorys;
using BISC.Modules.Connection.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Connection.Model.Factorys
{
    public class DeviceConnactionFactory : IDeviceConnectionFactory
    {
        public IDeviceConnection GetDeviceConnection()
        {
            return new DeviceConnection();
        }

        public IDeviceConnection GetDeviceConnection(string IP)
        {
            return new DeviceConnection(IP);
        }
    }
}
