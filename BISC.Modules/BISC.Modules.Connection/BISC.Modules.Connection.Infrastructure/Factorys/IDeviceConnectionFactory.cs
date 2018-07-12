using BISC.Modules.Connection.Infrastructure.DeviceConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Connection.Infrastructure.Factorys
{
    public interface IDeviceConnectionFactory
    {
        IDeviceConnection GetDeviceConnection();
        IDeviceConnection GetDeviceConnection(string IP);
    }
}
