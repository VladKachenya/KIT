using BISC.Infrastructure.Global.Common;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Common;
using BISC.Modules.Connection.Infrastructure.Connection.Dto;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Services;
using BISC.Modules.Reports.Infrastructure.Factorys;
using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Modules.Reports.Infrastructure.Presentation.ViewModels;
using BISC.Modules.Reports.Infrastructure.Services;
using BISC.Presentation.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Reports.Infrastructure.Keys;
using BISC.Presentation.BaseItems.ViewModels;

namespace BISC.Modules.Reports.Presentation.Commands
{
    public class ReportsSavingCommand : ISavingCommand
    {
        private readonly IInfoModelService _infoModelService;
        private readonly ILoggingService _loggingService;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly IProjectService _projectService;
        private readonly IReportsModelService _reportsModelService;
        private readonly IDataSetModelService _dataSetModelService;
        private readonly IDeviceWarningsService _deviceWarningsService;
        private readonly IReportControlNameService _reportControlNameService;
        private readonly IReportControlsFactory _IReportControlsFactory;

        private ObservableCollection<IReportControlViewModel> _reportsToSave;

        private IDevice _device;
        private Func<bool> _isSavingInDevice;

        public Action RefreshViewModel { get; set; }
        //private Action<bool> _fineshSaving;

        public ReportsSavingCommand(IInfoModelService infoModelService, ILoggingService loggingService,
            IConnectionPoolService connectionPoolService,
            IProjectService projectService, IReportsModelService reportModelService,
            IDataSetModelService dataSetModelService,IDeviceWarningsService deviceWarningsService, 
            IReportControlNameService reportControlNameService)
        {
            _infoModelService = infoModelService;
            _loggingService = loggingService;
            _connectionPoolService = connectionPoolService;
            _projectService = projectService;
            _reportsModelService = reportModelService;
            _dataSetModelService = dataSetModelService;
            _deviceWarningsService = deviceWarningsService;
            _reportControlNameService = reportControlNameService;
        }

        internal void Initialize(ref ObservableCollection<IReportControlViewModel> reportsToSave, IDevice device)
        {
            //_fineshSaving = fineshSaving;
            _reportsToSave = reportsToSave;
            _device = device;
        }

        private List<IReportControl> GetReportsToDelete()
        {
            List<IReportControl> reportControlsInDevice =
                _reportsModelService.GetAllReportControlsOfDevice(_device);
            var reportsToDelete = new List<IReportControl>();
            foreach (var report in reportControlsInDevice)
            {
                if (!_reportsToSave.Any(el => el.Name == report.Name))
                {
                    reportsToDelete.Add(report);
                }
            }

            return reportsToDelete;
        }

        public async Task<OperationResult<SavingCommandResultEnum>> SaveAsync()
        {
            try
            {
                List<IReportControl> reportControlsInDevice =
                    _reportsModelService.GetAllReportControlsOfDevice(_device);
                foreach (var reportControlInDevise in reportControlsInDevice)
                {
                    var ln = reportControlInDevise.ParentModelElement as ILogicalNode;
                    var ldevice = ln.ParentModelElement as ILDevice;
                    var reportToSave =
                        _reportsToSave.FirstOrDefault(element => element.Name == reportControlInDevise.Name);
                    if (reportToSave != null && !reportControlInDevise.IsDynamic)
                    {
                        if (!reportToSave.ChangeTracker.GetIsModifiedRecursive())
                        {
                            continue;
                        }

                        if (!reportToSave.IsDynamic)
                        {
                            if (reportControlsInDevice.Any(rep => rep.Name == reportToSave.Name))
                            {
                                if (_isSavingInDevice())
                                {
                                    //выполнение коммуникации с устройством
                                    for (int i = 1; i < reportToSave.ReportEnabledViewModel.Max + 1; i++)
                                    {
                                        var res = await SaveReportMms(reportToSave, reportControlInDevise, _device,
                                            ldevice.Inst, ln.Name, i.ToString("D2"));
                                        if (!res.IsSucceed)
                                        {
                                            _loggingService.LogMessage(res.GetFirstError(), SeverityEnum.Warning);
                                        }
                                        else
                                        {
                                            _loggingService.LogMessage(
                                                $"Запись Отчета {reportToSave.Name} в устройство {_device.Name} по MMS прошла успешно",
                                                SeverityEnum.Info);
                                            MapViewModelToModel(reportControlInDevise, reportToSave);
                                        }
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

                var resSavingDynamicReports =
                    await SaveDynamicReports(_reportsToSave, _device, reportControlsInDevice);
                //_projectService.SaveCurrentProject();
                if (_connectionPoolService.GetConnection(_device.Ip).IsConnected)
                {
                    _deviceWarningsService.SetWarningOfDevice(_device.DeviceGuid,
                        ReportsKeys.ReportsPresentationKeys.ReportsUnsavedWarningTag,
                        "Reports не соответствуют устройству");
                }

                if (resSavingDynamicReports.IsSucceed)
                {
                    _loggingService.LogMessage($"Reports устройства {_device.Name} успешно сохранены",
                        SeverityEnum.Info);
                    //_fineshSaving?.Invoke(await IsSavingByFtpNeeded());
                    return new OperationResult<SavingCommandResultEnum>(SavingCommandResultEnum.SavedOk);
                }
                else
                {
                    _loggingService.LogMessage($"Reports устройства {_device.Name} сохранены с ошибкой",
                        SeverityEnum.Warning);
                    return new OperationResult<SavingCommandResultEnum>(SavingCommandResultEnum.SavedWithErrors, false,
                        resSavingDynamicReports.GetFirstError());
                }
            }
            catch (Exception e)
            {
                _loggingService.LogMessage($"Reports устройства {_device.Name} сохранены c ошибкой: {e.Message}",
                    SeverityEnum.Warning);
                return new OperationResult<SavingCommandResultEnum>(SavingCommandResultEnum.SavedWithErrors, false,
                    e.Message);
            }
            finally
            {
                RefreshViewModel?.Invoke();
            }
        }

        //public async Task<bool> IsSavingByFtpNeeded()
        //{
        //    if (!_isSavingInDevice())
        //    {
        //        return false;
        //    }

        //    List<IReportControl> reportControlsInDevice = _reportsModelService.GetAllReportControlsOfDevice(_device);
        //    List<IReportControlViewModel> reportsToSaveDynamic =
        //        _reportsToSave.Where((model => model.IsDynamic)).ToList();

        //    List<IReportControl> reportControlsInDeviceToDelete =
        //        reportControlsInDevice.Where((model => model.IsDynamic)).ToList();
        //    if (!reportsToSaveDynamic.Any(model => model.ChangeTracker.GetIsModifiedRecursive()) &&
        //        !GetReportsToDelete().Any())
        //    {
        //        return false;
        //    }

        //    if (!reportsToSaveDynamic.Any() && !GetReportsToDelete().Any())
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        public async Task<OperationResult> ValidateBeforeSave()
        {
            var warnings = new List<string>();

            var reportControlNames = new List<string>();
            foreach (var name in _reportsToSave.Where(rc => rc.IsDynamic).Select(rc => rc.Name))
            {
                if (!reportControlNames.Contains(name))
                {
                    reportControlNames.Add(name);
                }
            }

            foreach (var name in reportControlNames)
            {
                var reportControls = _reportsToSave.Where(el => el.IsDynamic && el.Name == name);
                if (reportControls.Count() > 1)
                {
                    foreach (var reportViewModel in reportControls)
                    {
                        ((ViewModelBase)reportViewModel).IsWarning = true;
                    }

                    var mess = $"Имеется несколько ReportControl с именем {name}";
                    _loggingService.LogMessage(mess, SeverityEnum.Warning);
                    warnings.Add(mess);
                }

                if (!_reportControlNameService.GetIsDynamic(name))
                {
                    var reportControl = _reportsToSave.First(rc => rc.Name == name && rc.IsDynamic);
                    ((ViewModelBase)reportControl).IsWarning = true;
                    var mess = $"Имя {name} зарезервированное.";
                    _loggingService.LogMessage(mess, SeverityEnum.Warning);
                    warnings.Add(mess);
                }
            }
            if (warnings.Any())
            {
                return new OperationResult(warnings);
            }
            return OperationResult.SucceedResult;
        }

        private async Task<OperationResult> SaveDynamicReports(
            ObservableCollection<IReportControlViewModel> reportsToSave, IDevice device, List<IReportControl> reportControlsInDevice)
        {
            List<IReportControlViewModel> reportsToSaveDynamic =
                reportsToSave.Where((model => model.IsDynamic)).ToList();

            List<IReportControl> reportControlsInDeviceToDelete =
                reportControlsInDevice.Where((model => model.IsDynamic)).ToList();
            if (!reportsToSaveDynamic.Any(model => model.ChangeTracker.GetIsModifiedRecursive()) &&
                reportControlsInDeviceToDelete.Count == reportsToSaveDynamic.Count)
            {
                return OperationResult.SucceedResult;
            }

            List<IReportControl> reportControlsToSave =
                reportsToSaveDynamic.Select((model => model.GetUpdatedModel())).ToList();
            if (!reportsToSaveDynamic.Any() && !reportControlsInDeviceToDelete.Any())
            {
                return OperationResult.SucceedResult;
            }

            _reportsModelService.DeleteReportsFromDevice(device, reportControlsInDeviceToDelete);
            _reportsModelService.AddReportsToDevice(device, reportControlsToSave);

            return new OperationResult<SavingResultEnum>(SavingResultEnum.SavedInFile);
        }


        private void MapViewModelToModel(IReportControl reportControl, IReportControlViewModel reportControlViewModel)
        {
            reportControl.Name = reportControlViewModel.Name;
            reportControl.RptID = reportControlViewModel.ReportID;
            reportControl.Buffered = reportControlViewModel.IsBuffered;
            reportControl.BufTime = reportControlViewModel.BufferTime;
            reportControl.DataSet = reportControlViewModel.SelectidDataSetName;
            reportControl.IntgPd = reportControlViewModel.IntegrutyPeriod;
            reportControl.ConfRev = reportControlViewModel.ConfigurationRevision;
            reportControl.GiBool = reportControlViewModel.GiBool;
            reportControl.OptFields.Value = reportControlViewModel.OprionalFildsViewModel.GetUpdatedModel();
            reportControl.RptEnabled.Value = reportControlViewModel.ReportEnabledViewModel.GetUpdatedModel();
            reportControl.TrgOps.Value = reportControlViewModel.TriggerOptionsViewModel.GetUpdatedModel();
        }

        private async Task<OperationResult> SaveReportMms(IReportControlViewModel reportToSave,
            IReportControl reportControl,
            IDevice device, string ldInst, string lnName, string rptInst)
        {
            string fc = reportToSave.IsBuffered ? "BR" : "RP";

            string rptPath = lnName + "$" + fc + "$" + reportControl.Name + rptInst;
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
                var dataSet = _dataSetModelService.GetAllDataSetOfDevice(device)
                    .FirstOrDefault((set => set.Name == reportToSave.SelectidDataSetName));

                string dspath = String.Empty;

                if (dataSet != null)
                {
                    dspath = device.Name +
                             ldInst + "/"
                             + lnName + "$" + dataSet.Name;
                }


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

            if (reportControl.RptID.Substring(0, reportControl.RptID.Length - 2) != reportToSave.ReportID)
            {
                savingResult = await _connectionPoolService.GetConnection(device.Ip).MmsConnection
                    .WriteReportDataAsync(device.Name + ldInst, rptPath, "RptID",
                        reportToSave.ReportID);
            }

            // Тут необходимо ещё ConfRevision

            //   if (reportControl.ConfRev != reportToSave.ConfigurationRevision)
            //{
            //	savingResult = await _connectionPoolService.GetConnection(device.Ip).MmsConnection
            //		.WriteReportDataAsync(device.Name + ldInst, rptPath, "ConfRev",
            //			reportToSave.ConfigurationRevision);
            //}

            if (!savingResult.IsSucceed)
            {
                return savingResult;
            }

            return OperationResult.SucceedResult;
        }
    }
}