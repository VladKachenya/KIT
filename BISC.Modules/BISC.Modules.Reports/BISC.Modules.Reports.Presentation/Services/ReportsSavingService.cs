using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Services;
using BISC.Modules.Reports.Infrastructure.Presentation.Services;
using BISC.Modules.Reports.Infrastructure.Presentation.ViewModels;
using BISC.Modules.Reports.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Reports.Presentation.Services
{
    public class ReportsSavingService : IReportsSavingService
    {
        #region private filds
        IInfoModelService _infoModelService;
        ILoggingService _loggingService;
        IConnectionPoolService _connectionPoolService;
        ISclCommunicationModelService _sclCommunicationModel;
        IBiscProject _biscProject;
        IProjectService _projectService;
        IReportsModelService _reportsModelService;
        #endregion

        #region Ctor
        public ReportsSavingService(IInfoModelService infoModelService, ILoggingService loggingService, IConnectionPoolService connectionPoolService, 
            ISclCommunicationModelService sclCommunicationModelService, IBiscProject biscProject, IProjectService projectService, IReportsModelService reportModelService)
        {
            _infoModelService = infoModelService;
            _loggingService = loggingService;
            _connectionPoolService = connectionPoolService;
            _sclCommunicationModel = sclCommunicationModelService;
            _biscProject = biscProject;
            _projectService = projectService;
            _reportsModelService = reportModelService;
        }
        #endregion

        #region Implementation of IReportsSavingService
        public Task SaveReports(List<IReportControlViewModel> reportsToSave, IModelElement device, bool isSavingInDevice)
        {
            try
            {
                // удаление 
                var reportControlsInDevise = _reportsModelService.GetAllReportControlsOfDevice(device);
                foreach (var reportControlInDevise in reportControlsInDevise)
                {
                    if (!reportsToSave.Any(element => element.ReportID == reportControlInDevise.RptID))
                    {
                        var ln = reportControlInDevise.ParentModelElement as ILogicalNode;
                        var ldevice = ln.ParentModelElement as IDevice;
                        if (isSavingInDevice)
                        {
                            //выполнить комуникацию с устройством
                        }
                        ln.ChildModelElements.Remove(reportControlsInDevise.First(element => (element.RptID == reportControlInDevise.RptID)));
                    }
                }

                foreach (IReportControlViewModel reportToSave in reportsToSave)
                {
                    if (!reportToSave.ChangeTracker.GetIsModifiedRecursive()) continue;
                    //var lDevice = _infoModelService.GetLDevicesFromDevices(device)
                    //    .FirstOrDefault((ld => ld.Inst == reportToSave.))
                }



            }
            catch
            {

            }
            throw new Exception();
        }
        #endregion
    }
}
