using System.Collections.Generic;
using System.Linq;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Presentation.Events;

namespace BISC.Modules.Gooses.Presentation.Services
{
    public class GooseViewModelService : IGooseViewModelService
    {
        private readonly IGoosesModelService _goosesModelService;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly ILoggingService _loggingService;
        private readonly IDeviceWarningsService _deviceWarningsService;
        private readonly IGlobalEventsService _globalEventsService;

        public GooseViewModelService(
            IGoosesModelService goosesModelService, 
            IConnectionPoolService connectionPoolService, 
            ILoggingService loggingService, 
            IDeviceWarningsService deviceWarningsService, 
            IGlobalEventsService globalEventsService)
        {
            _goosesModelService = goosesModelService;
            _connectionPoolService = connectionPoolService;
            _loggingService = loggingService;
            _deviceWarningsService = deviceWarningsService;
            _globalEventsService = globalEventsService;
        }
        public void IncrementConfRevisionGooseControls(IDevice device, List<string> dataSetsNames)
        {
            // Выбираем все гусы с совподающими именами датасетов
            var chengedGooseControls = _goosesModelService.GetGooseControlsOfDevice(device)
                .Where(go => dataSetsNames.Any(n => go.DataSet == n)).ToList();
            foreach (var go in chengedGooseControls)
            {
                go.ConfRev++;
                _loggingService.LogMessage($"ConfRev GooseControl {go.Name} устройства {device.Name} увеличен", SeverityEnum.Info);
            }

            if (chengedGooseControls.Any())
            {
                _globalEventsService.SendMessage(new GooseConfRevisionChengEvent(device.DeviceGuid));
                // Выстовление варнингов сохранения если надо
                if (_connectionPoolService.GetConnection(device.Ip).IsConnected)
                {
                    _deviceWarningsService.SetWarningOfDevice(device.DeviceGuid,
                        GooseKeys.GooseWarningKeys.GooseControlUnsavedWarningTagKey,
                        "GooseControls не соответствуют устройству");
                }
            }
        }
    }
}