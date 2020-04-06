using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Saving;
using BISC.Modules.InformationModel.Infrastucture.Services;
using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Modules.Reports.Infrastructure.Services;

namespace BISC.Modules.Reports.Presentation.Services
{
    public class ReportsSavingService: IDeviceElementSavingService
    {
        private readonly IFtpReportModelService _ftpReportModelService;
        private readonly IInfoModelService _infoModelService;
        private readonly ILoggingService _loggingService;
        private readonly IReportsModelService _reportsModelService;

        public ReportsSavingService(
            IFtpReportModelService ftpReportModelService,
            IInfoModelService infoModelService,
            ILoggingService loggingService, 
            IReportsModelService reportsModelService)
        {
            _ftpReportModelService = ftpReportModelService;
            _infoModelService = infoModelService;
            _loggingService = loggingService;
            _reportsModelService = reportsModelService;
        }



        #region Implementation of IDeviceElementSavingService

        public int Priority => 5;
        public async Task<OperationResult> Save(IDevice device)
        {
            var reportControlsToSave =
                _reportsModelService.GetDynamicReports(device.DeviceGuid, device.GetFirstParentOfType<ISclModel>());
            var res = await _ftpReportModelService.WriteReportsToDevice(device.Ip, reportControlsToSave);
            if (res.IsSucceed)
            {
                return OperationResult.SucceedResult;
            }
            else
            {
                _loggingService.LogMessage(
                    $"Сохранение динамических отчетов по FTP прошло с ошибкой {device.Name} {res.GetFirstError()}",
                    SeverityEnum.Critical);
                return new OperationResult(res.GetFirstError());
            }
        }

        #endregion
    }
}
