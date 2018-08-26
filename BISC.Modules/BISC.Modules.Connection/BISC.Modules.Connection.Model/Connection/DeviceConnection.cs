using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BISC.Modules.Connection.Infrastructure.Connection;

namespace BISC.Modules.Connection.Model.Connection
{
    public class DeviceConnection : IDeviceConnection
    {
        private Timer _connectionCheckingTimer;

        public DeviceConnection(IMmsConnectionFacade mmsConnectionFacade)
        {
            MmsConnection = mmsConnectionFacade;
        }

        public string Ip { get; set; }
        public bool IsConnected { get; private set; }

        public async Task OpenConnection()
        {
            if(Ip==null)throw new Exception("Ip is empty");
            IsConnected = await MmsConnection.TryOpenConnection(Ip);
            _connectionCheckingTimer = new Timer(CheckConnection, null, 0, 1000);
        }

        private void CheckConnection(object state)
        {
            if (!MmsConnection.CheckConnection())
            {
                _connectionCheckingTimer.Dispose();
                IsConnected = false;
            }
        }

        public void StopConnection()
        {
            MmsConnection.StopConnection();
            CheckConnection(null);
        }

        public IMmsConnectionFacade MmsConnection { get; }
    }
}