using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Connection.Infrastructure.Connection;
using BISC.Modules.Connection.Infrastructure.Events;

namespace BISC.Modules.Connection.Model.Connection
{
    public class DeviceConnection : IDeviceConnection
    {
        private readonly IGlobalEventsService _globalEventsService;
        private readonly ILoggingService _loggingService;
        private Timer _connectionCheckingTimer;
        private bool _isConnected;

        public DeviceConnection(IMmsConnectionFacade mmsConnectionFacade, IGlobalEventsService globalEventsService, ILoggingService loggingService)
        {
            _globalEventsService = globalEventsService;
            _loggingService = loggingService;
            MmsConnection = mmsConnectionFacade;
        }

        public string Ip { get; set; }

        public bool IsConnected
        {
            get => _isConnected;
            private set
            {
                if (_isConnected != value)
                {
                    _globalEventsService.SendMessage(new ConnectionEvent(value, Ip));
                }
                _isConnected = value;
            }
        }

        public async Task OpenConnection()
        {
            if (Ip == null) throw new Exception("Ip is empty");
            IsConnected = await MmsConnection.TryOpenConnection(Ip);
            _connectionCheckingTimer = new Timer(CheckConnection, null, 0, 1000);
        }

        private void CheckConnection(object state)
        {
            if (!MmsConnection.CheckConnection())
            {
                _connectionCheckingTimer?.Dispose();
                IsConnected = false;

                if (!String.IsNullOrEmpty(Ip)) _loggingService.LogMessage($"Связь с [{Ip}] потеряна", SeverityEnum.Info);
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