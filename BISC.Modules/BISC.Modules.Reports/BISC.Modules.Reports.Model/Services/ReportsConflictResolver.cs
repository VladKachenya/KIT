using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.HelpClasses;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Modules.Reports.Infrastructure.Services;

namespace BISC.Modules.Reports.Model.Services
{
   public class ReportsConflictResolver: IElementConflictResolver
    {
        private readonly IReportsModelService _reportsModelService;
        private readonly IDeviceModelService _deviceModelService;

        public ReportsConflictResolver(IReportsModelService reportsModelService,IDeviceModelService deviceModelService)
        {
            _reportsModelService = reportsModelService;
            _deviceModelService = deviceModelService;
        }






        #region Implementation of IElementConflictResolver

        public string ConflictName => "Report Controls";
        public bool GetIfConflictsExists(string deviceName, ISclModel sclModelInDevice, ISclModel sclModelInProject)
        {
            var deviceInsclModelInDevice = _deviceModelService.GetDeviceByName(sclModelInDevice, deviceName);
            var devicesclModelInProject = _deviceModelService.GetDeviceByName(sclModelInProject, deviceName);

            var reportsInDevice = _reportsModelService.GetAllReportControlsOfDevice(deviceInsclModelInDevice);
            var reportsInProject = _reportsModelService.GetAllReportControlsOfDevice(devicesclModelInProject);
            if (reportsInProject.Count != reportsInDevice.Count)
            {
                return true;
            }

            foreach (var reportInDevice in reportsInDevice)
            {
                var reportInProject = reportsInProject.FirstOrDefault((control =>control.Name == reportInDevice.Name));
                if (reportInProject == null) return true;
                if (!reportInProject.ModelElementCompareTo(reportInDevice))
                {
                    return true;
                }
            }




            return false;

        }

        public Task<ResolvingResult> ResolveConflict(bool isFromDevice, string deviceName, ISclModel sclModelInDevice, ISclModel sclModelInProject)
        {
            throw new NotImplementedException();
        }
        

        #endregion
    }
}
