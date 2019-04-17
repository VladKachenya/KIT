using BISC.Infrastructure.Global.Common;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.DataSets.Infrastructure.Factorys;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.DataSets.Infrastructure.ViewModels;
using BISC.Modules.DataSets.Model.Mappers;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Services;
using BISC.Modules.Reports.Infrastructure.Services;
using BISC.Presentation.Infrastructure.Commands;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BISC.Modules.DataSets.Presentation.Commands
{
    public class DatasetsSavingByMmsCommand : ISavingCommand
    {
        private ObservableCollection<IDataSetViewModel> _dataSetsToSave;
        private IDevice _device;

        private string _errorDeleteMessagePattern = $"Не удалось удалить DataSet {0} в устройстве: {1}";
        private string _successDeleteMessagePattern = $"DataSet {0} удален в устройстве: {1}";


        private readonly IDatasetModelService _datasetModelService;
        private readonly IInfoModelService _infoModelService;
        private readonly IDataSetFactory _dataSetFactory;
        private readonly IProjectService _projectService;
        private readonly ILoggingService _loggingService;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly ISclCommunicationModelService _sclCommunicationModelService;
        private readonly IBiscProject _biscProject;
        private readonly IReportsModelService _reportsModelService;

        public DatasetsSavingByMmsCommand(IDatasetModelService datasetModelService, IInfoModelService infoModelService, IDataSetFactory dataSetFactory,
            IProjectService projectService, ILoggingService loggingService, IConnectionPoolService connectionPoolService,
            ISclCommunicationModelService sclCommunicationModelService, IBiscProject biscProject, IReportsModelService reportsModelService)
        {
            _datasetModelService = datasetModelService;
            _infoModelService = infoModelService;
            _dataSetFactory = dataSetFactory;
            _projectService = projectService;
            _loggingService = loggingService;
            _connectionPoolService = connectionPoolService;
            _sclCommunicationModelService = sclCommunicationModelService;
            _biscProject = biscProject;
            _reportsModelService = reportsModelService;
        }


        public void Initialize(ObservableCollection<IDataSetViewModel> dataSetsToSave, IModelElement device)
        {
            _dataSetsToSave = dataSetsToSave;
            _device = device as IDevice;
        }

        private async Task DeleteDataSetInDevice(string lnName, string ldName, string datasetName)
        {
            var res = await _connectionPoolService
                .GetConnection(_sclCommunicationModelService.GetIpOfDevice(_device.Name,
                    _biscProject.MainSclModel.Value)).MmsConnection.DeleteDataSet(lnName, ldName, _device.Name, datasetName);
            if (!res.IsSucceed)
            {
                _loggingService.LogMessage(string.Format(_errorDeleteMessagePattern, datasetName, res.GetFirstError()), SeverityEnum.Warning);
            }
            else
            {
                _loggingService.LogMessage(string.Format(_successDeleteMessagePattern, datasetName, _device.Name), SeverityEnum.Info);
            }
        }

        public async Task<OperationResult<SavingCommandResultEnum>> SaveAsync()
        {
            try
            {

                var dataSetsExisting = _datasetModelService.GetAllDataSetOfDevice(_device);

                foreach (var dataSetExiting in dataSetsExisting)
                {
                    if (_dataSetsToSave.Any((model => model.EditableNamePart == dataSetExiting.Name)))
                    {
                        continue;
                    }

                    var ln = dataSetExiting.ParentModelElement as ILogicalNode;
                    var ldevice = ln.ParentModelElement as ILDevice;
                    if (_connectionPoolService.GetConnection(_device.Ip).IsConnected)
                    {
                        await DeleteDataSetInDevice(ln.Name, ldevice.Inst, dataSetExiting.Name);
                    }
                    ln.ChildModelElements.Remove(dataSetsExisting.First((set =>
                        set.Name == dataSetExiting.Name)));
                }

                foreach (var dataSetToSave in _dataSetsToSave)
                {
                    if (dataSetToSave.IsEditeble)
                    {
                        if (!dataSetToSave.ChangeTracker.GetIsModifiedRecursive()) { continue; }

                        var ldevice = _infoModelService.GetLDevicesFromDevices(_device)
                            .FirstOrDefault((lDevice => lDevice.Inst == dataSetToSave.SelectedParentLd));
                        var ln = ldevice.AlLogicalNodes.FirstOrDefault(
                            (node => node.Name == dataSetToSave.SelectedParentLn));


                        if (dataSetsExisting.Any((set => set.Name == dataSetToSave.EditableNamePart)))
                        {
                            if (_connectionPoolService.GetConnection(_device.Ip).IsConnected)
                            {
                                await DeleteDataSetInDevice(ln.Name, ldevice.Inst, dataSetToSave.EditableNamePart);
                            }
                            ln.ChildModelElements.Remove(dataSetsExisting.First((set =>
                                set.Name == dataSetToSave.EditableNamePart)));
                        }

                        IDataSet dataSet = _dataSetFactory.CreateDataSet(ln, dataSetToSave.EditableNamePart,
                            dataSetToSave.FcdaViewModels.Select((model => model.GetFcda())).ToList());
                        if (_connectionPoolService.GetConnection(_device.Ip).IsConnected)
                        {
                            var res = await _connectionPoolService
                                    .GetConnection(_sclCommunicationModelService.GetIpOfDevice(_device.Name,
                                        _biscProject.MainSclModel.Value)).MmsConnection.AddDataSet(ln.Name, ldevice.Inst, _device.Name, dataSetToSave.EditableNamePart, dataSet.FcdaList.ToDtos((_device as IDevice).Name));
                            if (!res.IsSucceed)
                            {
                                _loggingService.LogMessage($"Не удалось {dataSetToSave.EditableNamePart} добавить DataSet в устройстве: {res.GetFirstError()}", SeverityEnum.Warning);
                            }
                            else
                            {
                                _loggingService.LogMessage($"DataSet {dataSetToSave.EditableNamePart} добавлен в устройство: {_device.Name}", SeverityEnum.Info);
                            }
                        }
                    }
                }

                _projectService.SaveCurrentProject();
                _loggingService.LogMessage($"DataSets устройства {_device.Name} успешно сохранены",
                    SeverityEnum.Info);
            }
            catch (Exception e)
            {
                _loggingService.LogMessage($"DataSets устройства {_device.Name} сохранены c ошибкой: {e.Message}",
                    SeverityEnum.Warning);
            }
            return new OperationResult<SavingCommandResultEnum>(SavingCommandResultEnum.SavedOk);
        }

        public Task<bool> IsSavingByFtpNeeded()
        {
            return Task.FromResult(false);
        }

        public async Task<OperationResult> ValidateBeforeSave()
        {
            //var reportsWithDatasts = _reportsModelService.GetAllReportControlsOfDevice(_device).Where((control => _dataSetsToSave
            //      .Any((model =>
            //          model.ChangeTracker.GetIsModifiedRecursive() && model.EditableNamePart == control.DataSet))));
            //if (reportsWithDatasts.Any())
            //{
            //    string mes = Environment.NewLine;
            //    foreach (var reportControl in reportsWithDatasts)
            //    {
            //        mes += reportControl.Name + Environment.NewLine;
            //    }
            //    return new OperationResult($"Датасеты, которые вы хотите изменить используются в следующих отчетах: {mes}");
            //}
            return OperationResult.SucceedResult;
        }

        public Action RefreshViewModel { get; set; }
    }
}
