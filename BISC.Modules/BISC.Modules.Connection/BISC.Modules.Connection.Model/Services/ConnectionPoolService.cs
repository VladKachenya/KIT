using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Connection.Infrastructure.Connection;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Connection.Model.Connection;

namespace BISC.Modules.Connection.Model.Services
{
    public class ConnectionPoolService : IConnectionPoolService
    {
        private readonly Func<IDeviceConnection> _deviceConnectionCreator;
        private List<IDeviceConnection> _deviceConnections;

        public ConnectionPoolService(Func<IDeviceConnection> deviceConnectionCreator)
        {
            _deviceConnectionCreator = deviceConnectionCreator;
            _deviceConnections = new List<IDeviceConnection>();
        }

        public IDeviceConnection GetConnection(string ip)
        {
            IDeviceConnection deviceConnection;
            deviceConnection = _deviceConnections.FirstOrDefault((connection => connection.Ip == ip));
            if (deviceConnection != null) return deviceConnection;
            deviceConnection=_deviceConnectionCreator();
            deviceConnection.Ip = ip;
            _deviceConnections.Add(deviceConnection);
            return deviceConnection;
        }
    }
}
