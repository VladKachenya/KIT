using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Services;
using BISC.Modules.Reports.Infrastructure.Factorys;
using BISC.Modules.Reports.Infrastructure.Keys;
using BISC.Modules.Reports.Infrastructure.Model;
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
        ISclCommunicationModelService _sclCommunicationModelService;
        IBiscProject _biscProject;
        IProjectService _projectService;
        IReportsModelService _reportsModelService;
        IReportControlsFactory _IReportControlsFactory;
        #endregion

        #region Ctor
        public ReportsSavingService(IInfoModelService infoModelService, ILoggingService loggingService, IConnectionPoolService connectionPoolService, 
            ISclCommunicationModelService sclCommunicationModelService, IBiscProject biscProject, IProjectService projectService, IReportsModelService reportModelService)
        {
            _infoModelService = infoModelService;
            _loggingService = loggingService;
            _connectionPoolService = connectionPoolService;
            _sclCommunicationModelService = sclCommunicationModelService;
            _biscProject = biscProject;
            _projectService = projectService;
            _reportsModelService = reportModelService;
        }
        #endregion

        #region Implementation of IReportsSavingService
        public async Task SaveReportsAsync(List<IReportControlViewModel> reportsToSave, IDevice device, bool isSavingInDevice)
        {
            try
            {
                // удаление 
                var reportControlsInDevise = _reportsModelService.GetAllReportControlsOfDevice(device);
                foreach (var reportControlInDevise in reportControlsInDevise)
                {
                    if (!reportsToSave.Any(element => element.Name == reportControlInDevise.Name))
                    {
                        var ln = reportControlInDevise.ParentModelElement as ILogicalNode;
                        var ldevice = ln.ParentModelElement as IDevice;
                        if (isSavingInDevice)
                        {
                            await Task.Run(() => null);
                        }
                        ln.ChildModelElements.Remove(reportControlsInDevise.First(element => (element.Name == reportControlInDevise.Name)));
                    }
                }

                foreach (IReportControlViewModel reportToSave in reportsToSave)
                {
                    if (!reportToSave.ChangeTracker.GetIsModifiedRecursive()) continue;
                    var lDevice = _infoModelService.GetLDevicesFromDevices(device)
                        .FirstOrDefault((ld => ld.Inst == reportToSave.ParentLdName));
                    var lNode = lDevice.AlLogicalNodes
                        .FirstOrDefault(node => node.Name == reportToSave.ParentLnName);
                    // Тут надо разобратся
                    if (!reportToSave.IsDynamic)
                    {
                        if (reportControlsInDevise.Any(rep => rep.Name == reportToSave.Name))
                        {
                            if (isSavingInDevice)
                            {
                                //выполнение коммуникации с устройством
                            }
                        }
                    }
                    else
                    {
                    }
                    _reportsModelService.DeleteReportsFromDevice(device, new List<IReportControl> { reportToSave.Model });
                    reportToSave.UpdateModel();
                    _reportsModelService.AddReportsToDevice(device, new List<IReportControl> { reportToSave.Model });
                    //SetReportToLigicNode(lNode, reportToSave.Model, insertIndex);
                    //reportToSave.Model.ParentModelElement = lNode;
                }
                _projectService.SaveCurrentProject();
                _loggingService.LogMessage($"Reports устройства {(device as IDevice).Name} успешно сохранены",
                    SeverityEnum.Info);
            }
            catch (Exception e)
            {
                _loggingService.LogMessage($"Reports устройства {(device as IDevice).Name} сохранены c ошибкой: {e.Message}",
                    SeverityEnum.Warning);
            }
        }
        #endregion     
    }
}
