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
using BISC.Infrastructure.Global.Common;
using BISC.Model.Infrastructure.Common;
using BISC.Modules.Connection.Infrastructure.Connection;
using BISC.Modules.Connection.Infrastructure.Connection.Dto;
using BISC.Modules.DataSets.Infrastructure.Services;

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
        private readonly IDatasetModelService _datasetModelService;
        private readonly IFtpReportModelService _ftpReportModelService;
        IReportControlsFactory _IReportControlsFactory;
        #endregion

        #region Ctor
        public ReportsSavingService(IInfoModelService infoModelService, ILoggingService loggingService, IConnectionPoolService connectionPoolService,
            ISclCommunicationModelService sclCommunicationModelService, IBiscProject biscProject, IProjectService projectService,
            IReportsModelService reportModelService, IDatasetModelService datasetModelService,IFtpReportModelService ftpReportModelService)
        {
            _infoModelService = infoModelService;
            _loggingService = loggingService;
            _connectionPoolService = connectionPoolService;
            _sclCommunicationModelService = sclCommunicationModelService;
            _biscProject = biscProject;
            _projectService = projectService;
            _reportsModelService = reportModelService;
            _datasetModelService = datasetModelService;
            _ftpReportModelService = ftpReportModelService;
        }
        #endregion

        #region Implementation of IReportsSavingService
        public async Task SaveReportsAsync(List<IReportControlViewModel> reportsToSave, IDevice device, bool isSavingInDevice)
        {
            IsLastSavingWasWithFTP = false;
            try
            {
                List<IReportControl> reportControlsInDevice = _reportsModelService.GetAllReportControlsOfDevice(device);
                foreach (var reportControlInDevise in reportControlsInDevice)
                {
                    var ln = reportControlInDevise.ParentModelElement as ILogicalNode;
                    var ldevice = ln.ParentModelElement as ILDevice;
                    var reportToSave = reportsToSave.FirstOrDefault(element => element.Name == reportControlInDevise.Name);
                    if (reportToSave != null || !reportControlInDevise.IsDynamic)
                    {
                        if (!reportToSave.ChangeTracker.GetIsModifiedRecursive()) continue;
                        if (!reportToSave.IsDynamic)
                        {
                            if (reportControlsInDevice.Any(rep => rep.Name == reportToSave.Name))
                            {
                                if (isSavingInDevice)
                                {
                                    //выполнение коммуникации с устройством
                                    var res = await SaveReportMms(reportToSave, reportControlInDevise, device,
                                        ldevice.Inst, ln.Name);
                                    if (!res.IsSucceed)
                                    {
                                        _loggingService.LogMessage(res.GetFirstError(), SeverityEnum.Warning);
                                    }
                                    else
                                    {
                                        _loggingService.LogMessage(
                                            $"Запись Отчета {reportToSave.Name} в устройство {device.Name} по MMS прошла успешно",
                                            SeverityEnum.Info);
                                        MapViewModelToModel(reportControlInDevise, reportToSave);
                                    }
                                }
                                else
                                {
                                    MapViewModelToModel(reportControlInDevise, reportToSave);
                                }
                            }
                            else
                            {
                                _loggingService.LogMessage($"Несовпадение статических моделей отчетов при сохранении",
                                    SeverityEnum.Warning);
                            }
                        }
                    }
                }
               var resSavingDynamicReports= await SaveDynamicReports(reportsToSave, device, isSavingInDevice, reportControlsInDevice);
          
                _projectService.SaveCurrentProject();
                if (resSavingDynamicReports.IsSucceed)
                {
                    _loggingService.LogMessage($"Reports устройства {(device as IDevice).Name} успешно сохранены",
                        SeverityEnum.Info);
                }
                else
                {
                    _loggingService.LogMessage($"Reports устройства {(device as IDevice).Name} сохранены с ошибкой",
                        SeverityEnum.Warning);
                }
            }
            catch (Exception e)
            {
                _loggingService.LogMessage($"Reports устройства {(device as IDevice).Name} сохранены c ошибкой: {e.Message}",
                    SeverityEnum.Warning);
            }
        }

        private async Task<OperationResult> SaveDynamicReports(List<IReportControlViewModel> reportsToSave, IDevice device,
            bool isSavingInDevice, List<IReportControl> reportControlsInDevice)
        {
            List<IReportControlViewModel> reportsToSaveDynamic =
                reportsToSave.Where((model => model.IsDynamic)).ToList();
            List<IReportControl> reportControlsInDeviceToDelete =
                reportControlsInDevice.Where((model => model.IsDynamic)).ToList();

            List<IReportControl> reportControlsToSave =
                reportsToSaveDynamic.Select((model => model.GetUpdatedModel())).ToList();
            if (isSavingInDevice)
            {
                var res = await _ftpReportModelService.WriteReportsToDevice(device.Ip, reportControlsToSave,
                    _infoModelService.GetZeroLDevicesFromDevices(device));
                if (res.IsSucceed)
                {
                    _loggingService.LogMessage($"Сохранение динамических отчетов по FTP прошло успешно {device.Name}",
                        SeverityEnum.Info);
                    _reportsModelService.DeleteReportsFromDevice(device, reportControlsInDeviceToDelete);
                    _reportsModelService.AddReportsToDevice(device, reportControlsToSave);
                    IsLastSavingWasWithFTP = true;
                }
                else
                {
                    _loggingService.LogMessage($"Сохранение динамических отчетов по FTP прошло с ошибкой {device.Name} {res.GetFirstError()}",
                        SeverityEnum.Critical);
                    return new OperationResult(res.GetFirstError());
                }
            }
            else
            {
                _reportsModelService.DeleteReportsFromDevice(device, reportControlsInDeviceToDelete);
                _reportsModelService.AddReportsToDevice(device, reportControlsToSave);
            }

            
            return OperationResult.SucceedResult;
        }

        public bool IsLastSavingWasWithFTP { get; set; }
        private void MapViewModelToModel(IReportControl reportControl, IReportControlViewModel reportControlViewModel)
        {
            reportControl.Name = reportControlViewModel.Name;
            reportControl.RptID = reportControlViewModel.ReportID;
            reportControl.Buffered = reportControlViewModel.IsBuffered;
            reportControl.BufTime = reportControlViewModel.BufferTime;
            reportControl.DataSet = reportControlViewModel.SelectidDataSetName;
            reportControl.IntgPd = reportControlViewModel.IntegrutyPeriod;
            reportControl.GiBool = reportControlViewModel.GiBool;
            reportControl.OptFields.Value = reportControlViewModel.OprionalFildsViewModel.GetUpdatedModel();
            reportControl.RptEnabled.Value = reportControlViewModel.ReportEnabledViewModel.GetUpdatedModel();
            reportControl.TrgOps.Value = reportControlViewModel.TriggerOptionsViewModel.GetUpdatedModel();
        }
        private async Task<OperationResult> SaveReportMms(IReportControlViewModel reportToSave, IReportControl reportControl,
            IDevice device, string ldInst, string lnName)
        {
            string fc = reportToSave.IsBuffered ? "BR" : "RP";

            string rptPath = lnName + "$" + fc + "$" + reportControl.Name;
            OperationResult savingResult = OperationResult.SucceedResult;
            if ((reportControl.OptFields.Value.TimeStamp != reportToSave.OprionalFildsViewModel.ReportTimeStamp) ||
                (reportControl.OptFields.Value.DataSet != reportToSave.OprionalFildsViewModel.DataSetName) ||
                (reportControl.OptFields.Value.ReasonCode != reportToSave.OprionalFildsViewModel.ReasonForInclusion) ||
                (reportControl.OptFields.Value.EntryID != reportToSave.OprionalFildsViewModel.EntruID) ||
                (reportControl.OptFields.Value.ConfigRef != reportToSave.OprionalFildsViewModel.ConfigRevision) ||
                (reportControl.OptFields.Value.BufOvfl != reportToSave.OprionalFildsViewModel.BufferOverflow) ||
                (reportControl.OptFields.Value.Segmentation != reportToSave.OprionalFildsViewModel.Segmentation) ||
                (reportControl.OptFields.Value.SeqNum != reportToSave.OprionalFildsViewModel.SequenceNumber) ||
                (reportControl.OptFields.Value.DataRef != reportToSave.OprionalFildsViewModel.DataReference))
            {

                IOptFields optFields = reportToSave.OprionalFildsViewModel.GetUpdatedModel();
                savingResult = await _connectionPoolService.GetConnection(device.Ip).MmsConnection
                    .WriteReportDataAsync(device.Name + ldInst, rptPath, "OptFlds",
                        optFields.ReportOptionsToBytes());

                if (!savingResult.IsSucceed)
                {
                    return savingResult;
                }
            }


            if ((reportControl.TrgOps.Value.Dchg != reportToSave.TriggerOptionsViewModel.DataChange)
                    || (reportControl.TrgOps.Value.Qchg != reportToSave.TriggerOptionsViewModel.QualityChange)
                    || (reportControl.TrgOps.Value.Dupd != reportToSave.TriggerOptionsViewModel.DataUpdate) ||
                    (reportControl.TrgOps.Value.Period != reportToSave.TriggerOptionsViewModel.Integrity)
                    || (reportControl.TrgOps.Value.Gi != reportToSave.TriggerOptionsViewModel.GenetralInterrogation))
            {
                ITrgOps trgOps = reportToSave.TriggerOptionsViewModel.GetUpdatedModel();
                savingResult = await _connectionPoolService.GetConnection(device.Ip).MmsConnection
                    .WriteReportDataAsync(device.Name + ldInst, rptPath, "TrgOps",
                        trgOps.TriggerOptionsToBytes());
                if (!savingResult.IsSucceed)
                {
                    return savingResult;
                }
            }

            if (reportControl.DataSet != reportToSave.SelectidDataSetName)
            {
                var dataSet = _datasetModelService.GetAllDataSetOfDevice(device).FirstOrDefault((set => set.Name == reportToSave.SelectidDataSetName));

                string dspath = device.Name +
                               ldInst + "/"
                                + lnName + "$" + dataSet.Name;


                savingResult = await _connectionPoolService.GetConnection(device.Ip).MmsConnection
                    .WriteReportDataAsync(device.Name + ldInst, rptPath, "DatSet",
                     dspath);
                if (!savingResult.IsSucceed)
                {
                    return savingResult;
                }
            }

            if (reportControl.GiBool != reportToSave.GiBool)
            {
                savingResult = await _connectionPoolService.GetConnection(device.Ip).MmsConnection
                    .WriteReportDataAsync(device.Name + ldInst, rptPath, "GI",
                        reportToSave.GiBool);
            }

         

            if (reportControl.BufTime != reportToSave.BufferTime)
            {
                savingResult = await _connectionPoolService.GetConnection(device.Ip).MmsConnection
                    .WriteReportDataAsync(device.Name + ldInst, rptPath, "BufTm",
                        reportToSave.BufferTime);
            }


            if (reportControl.IntgPd != reportToSave.IntegrutyPeriod)
            {
                savingResult = await _connectionPoolService.GetConnection(device.Ip).MmsConnection
                    .WriteReportDataAsync(device.Name + ldInst, rptPath, "IntgPd",
                        reportToSave.IntegrutyPeriod);
            }
            if (reportControl.RptID != reportToSave.ReportID)
            {
                savingResult = await _connectionPoolService.GetConnection(device.Ip).MmsConnection
                    .WriteReportDataAsync(device.Name + ldInst, rptPath, "RptID",
                        reportToSave.ReportID);
            }
            if (!savingResult.IsSucceed)
            {
                return savingResult;
            }
            return OperationResult.SucceedResult;
        }

        #endregion     
    }
}