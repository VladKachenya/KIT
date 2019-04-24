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
        //private Timer _connectionCheckingTimer;
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

        public async Task OpenConnection(int tryNumber = 1)
        {
            if (Ip == null) throw new Exception("Ip is empty");
            for (int i = 0; i < tryNumber; i++)
            {
                IsConnected = await MmsConnection.TryOpenConnection(Ip);
                //IsConnected = await
                if (IsConnected) break;


                await Task.Delay(400);

            }
            StartCheckingConnection();
            //_connectionCheckingTimer = new Timer(CheckConnection, 5, 0, 2000);
        }

        private async void StartCheckingConnection()
        {
            while (IsConnected)
            {
                await CheckConnection(3);
                await Task.Delay(2000);
            }
        }

        private async Task CheckConnection(int state = 1)
        {
            var allowedNumberFailedRequests = state;
            var failedConnectionCounter = 0;
            while (true)
            {
                if (!await _pingService.GetPing(Ip) || !MmsConnection.CheckConnection())
                {
                    failedConnectionCounter++;
                }
                else
                    return;

                if (failedConnectionCounter >= allowedNumberFailedRequests)
                {
                    IsConnected = false;
                    if (!String.IsNullOrEmpty(Ip))
                        _loggingService.LogMessage($"Связь с [{Ip}] потеряна", SeverityEnum.Info);
                    _globalEventsService.SendMessage(new LossConnectionEvent(Ip));
                    return;
                }
                await Task.Delay(500);
            }
        }



        public async void StopConnection()
        {
            MmsConnection.StopConnection();
            await CheckConnection();
        }

        public IMmsConnectionFacade MmsConnection { get; }
    }
}