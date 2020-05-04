using System.Collections.Generic;
using System.Linq;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Reports.Infrastructure.Events;
using BISC.Modules.Reports.Infrastructure.Keys;
using BISC.Modules.Reports.Infrastructure.Services;

namespace BISC.Modules.Reports.Model.Services
{
    public class ReportConfRevisionService : IReportConfRevisionService
    {
        private readonly IReportsModelService _reportsModelService;
        private readonly ILoggingService _loggingService;
        private readonly IGlobalEventsService _globalEventsService;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly IDeviceWarningsService _deviceWarningsService;

        public ReportConfRevisionService(
            IReportsModelService reportsModelService,
            ILoggingService loggingService,
            IGlobalEventsService globalEventsService,
            IConnectionPoolService connectionPoolService,
            IDeviceWarningsService deviceWarningsService
            )
        {
            _reportsModelService = reportsModelService;
            _loggingService = loggingService;
            _globalEventsService = globalEventsService;
            _connectionPoolService = connectionPoolService;
            _deviceWarningsService = deviceWarningsService;
        }

        public void IncrementConfRevisionReportControl(IDevice device, List<string> dataSetsNames)
        {
            var chengedReportControls = _reportsModelService.GetAllReportControlsOfDevice(device)
                .Where(re => dataSetsNames.Any(dsN => re.DataSet == dsN)).ToList();
            foreach (var report in chengedReportControls)
            {
                report.ConfRev++;
                _loggingService.LogMessage($"ConfRev ReportControl {report.Name} устройства {device.Name} увеличен", SeverityEnum.Info);
            }

            if (chengedReportControls.Any())
            {
                _globalEventsService.SendMessage(new ReportConfRevisionChengEvent(device.DeviceGuid));
                if (_connectionPoolService.GetConnection(device.Ip).IsConnected)
                {
                    _deviceWarningsService.SetWarningOfDevice(device.DeviceGuid,
                        ReportsKeys.ReportsPresentationKeys.ReportsUnsavedWarningTag,
                        "ReportControls не соответствуют устройству");
                }
            }
        }
    }
}