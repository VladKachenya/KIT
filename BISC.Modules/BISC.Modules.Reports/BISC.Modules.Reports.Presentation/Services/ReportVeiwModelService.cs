using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Reports.Infrastructure.Events;
using BISC.Modules.Reports.Infrastructure.Keys;
using BISC.Modules.Reports.Infrastructure.Presentation.Services;
using BISC.Modules.Reports.Infrastructure.Presentation.ViewModels;
using BISC.Modules.Reports.Infrastructure.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BISC.Modules.Reports.Presentation.Services
{
    public class ReportVeiwModelService : IReportVeiwModelService
    {
        private readonly IReportsModelService _reportsModelService;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly ILoggingService _loggingService;
        private readonly IDeviceWarningsService _deviceWarningsService;
        private readonly IGlobalEventsService _globalEventsService;

        public ReportVeiwModelService(IReportsModelService reportsModelService, IConnectionPoolService connectionPoolService,
            ILoggingService loggingService, IDeviceWarningsService deviceWarningsService, IGlobalEventsService globalEventsService)
        {
            _reportsModelService = reportsModelService;
            _connectionPoolService = connectionPoolService;
            _loggingService = loggingService;
            _deviceWarningsService = deviceWarningsService;
            _globalEventsService = globalEventsService;
        }
        #region Implementation of IReportVeiwModelService
        public ObservableCollection<IReportControlViewModel> SortReportViewModels(IEnumerable<IReportControlViewModel> reportControlViewModels)
        {
            var coll = reportControlViewModels.OrderBy(el => el.IsDynamic).ThenBy(el => !el.IsBuffered).
                ThenBy(el => el.Name.Trim(new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', })).
                ThenBy(el => TrimNamber(el.Name));
            return new ObservableCollection<IReportControlViewModel>(coll);
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

        #endregion

        #region private methods

        private int TrimNamber(string str)
        {
            var clearStr = str.Trim(new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', });
            var str1 = str.Trim();
            var intStr = str1.Substring(clearStr.Length);
            if (string.IsNullOrEmpty(intStr))
            {
                return 0;
            }
            int.TryParse(intStr, out var res);
            return res;
        }
        #endregion

    }
}