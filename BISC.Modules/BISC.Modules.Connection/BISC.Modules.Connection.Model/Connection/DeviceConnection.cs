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
using BISC.Modules.Connection.Infrastructure.Services;

namespace BISC.Modules.Connection.Model.Connection
{
    public class DeviceConnection : IDeviceConnection
    {
        private readonly IGlobalEventsService _globalEventsService;
        private readonly ILoggingService _loggingService;
        private readonly IPingService _pingService;
        private Timer _connectionCheckingTimer;
        private bool _isConnected;

        public DeviceConnection(IMmsConnectionFacade mmsConnectionFacade, IGlobalEventsService globalEventsService, ILoggingService loggingService, IPingService pingService)
        {
            _globalEventsService = globalEventsService;
            _loggingService = loggingService;
            MmsConnection = mmsConnectionFacade;
            _pingService = pingService;
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
            _connectionCheckingTimer = new Timer(CheckConnection, null, 0, 1500);
        }

        private async void CheckConnection(object state)
        {
            SemaphoreSlim semaphoreSlim = new SemaphoreSlim(0);
            if (await _pingService.GetPing(Ip) == false || !MmsConnection.CheckConnection())
            {
                _connectionCheckingTimer?.Dispose();
                IsConnected = false;

                if (!String.IsNullOrEmpty(Ip))
                    _loggingService.LogMessage($"Связь с [{Ip}] потеряна", SeverityEnum.Info);
            }
            semaphoreSlim.Release();
        }



        public void StopConnection()
        {
            MmsConnection.StopConnection();
            CheckConnection(null);
        }

        public IMmsConnectionFacade MmsConnection { get; }
    }
}