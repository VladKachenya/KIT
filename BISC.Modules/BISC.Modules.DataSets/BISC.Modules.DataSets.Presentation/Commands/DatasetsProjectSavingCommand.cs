using BISC.Infrastructure.Global.Common;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.DataSets.Infrastructure.Factorys;
using BISC.Modules.DataSets.Infrastructure.Keys;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.DataSets.Infrastructure.ViewModels;
using BISC.Modules.DataSets.Presentation.Services.Interfaces;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Services;
using BISC.Modules.Reports.Infrastructure.Services;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.ChangeTracker;
using BISC.Presentation.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BISC.Modules.Gooses.Presentation.Interfaces.Services;
using BISC.Modules.Reports.Infrastructure.Presentation.Services;

namespace BISC.Modules.DataSets.Presentation.Commands
{
    public class DatasetsProjectSavingCommand : ISavingCommand
    {

        private readonly IDatasetModelService _datasetModelService;
        private readonly IInfoModelService _infoModelService;
        private readonly IDataSetFactory _dataSetFactory;
        private readonly IProjectService _projectService;
        private readonly ILoggingService _loggingService;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly ISclCommunicationModelService _sclCommunicationModelService;
        private readonly IBiscProject _biscProject;
        //private readonly IFtpDataSetModelService _ftpDataSetModelService;

        private readonly IDeviceWarningsService _deviceWarningsService;

        private readonly IGooseViewModelService _gooseViewModelService;

        private readonly IReportVeiwModelService _reportVeiwModelService;

        private readonly IDataSetNameService _dataSetNameService;
        //private Action<bool> _fineshSaving;

        private IDevice _device;
        private ObservableCollection<IDataSetViewModel> _dataSetsToSave;
        private IChangeTracker[] _changeTrackers;

        //private string _errorDeleteMessagePattern = $"Не удалось удалить DataSet {0} в устройстве: {1}";
        //private string _successDeleteMessagePattern = $"DataSet {0} удален в устройстве: {1}";

        public DatasetsProjectSavingCommand(IDatasetModelService datasetModelService, IInfoModelService infoModelService,
            IDataSetFactory dataSetFactory, IProjectService projectService, ILoggingService loggingService, 
            IConnectionPoolService connectionPoolService, ISclCommunicationModelService sclCommunicationModelService, IBiscProject biscProject, 
            IDeviceWarningsService deviceWarningsService, IGooseViewModelService gooseViewModelService, IReportVeiwModelService reportVeiwModelService,
            IDataSetNameService dataSetNameService)
        {
            _datasetModelService = datasetModelService;
            _infoModelService = infoModelService;
            _dataSetFactory = dataSetFactory;
            _projectService = projectService;
            _loggingService = loggingService;
            _connectionPoolService = connectionPoolService;
            _sclCommunicationModelService = sclCommunicationModelService;
            _biscProject = biscProject;
            _deviceWarningsService = deviceWarningsService;
            _gooseViewModelService = gooseViewModelService;
            _reportVeiwModelService = reportVeiwModelService;
            _dataSetNameService = dataSetNameService;
        }

        #region Implementation of ISavingCommand

        public Action RefreshViewModel { get; set; }


        public void Initialize(ref ObservableCollection<IDataSetViewModel> dataSetsToSave, IModelElement device, params IChangeTracker[] changeTrackers)
        {
            _dataSetsToSave = dataSetsToSave;
            _device = device as IDevice;
            _changeTrackers = changeTrackers;
        }


        public async Task<OperationResult<SavingCommandResultEnum>> SaveAsync()
        {
            var dataSetNamesWithChengests = new List<string>();
            try
            {

                var dataSetsExisting = _datasetModelService.GetAllDataSetOfDevice(_device);

                // Проверка удалённых датасетов
                foreach (var dataSetExiting in dataSetsExisting)
                {
                    if (_dataSetsToSave.Any((model => model.EditableNamePart == dataSetExiting.Name)))
                    {
                        continue;
                    }
                    dataSetNamesWithChengests.Add(dataSetExiting.Name);
                    var ln = dataSetExiting.ParentModelElement as ILogicalNode;
                    ln?.ChildModelElements.Remove(dataSetsExisting.First((set =>
                        set.Name == dataSetExiting.Name)));
                }

                // Проверка модифицированных датасетов
                foreach (var dataSetToSave in _dataSetsToSave)
                {
                    if (dataSetToSave.IsEditeble)
                    {
                        if (!dataSetToSave.ChangeTracker.GetIsModifiedRecursive())
                        {
                            continue;
                        }
                        dataSetNamesWithChengests.Add(dataSetToSave.EditableNamePart);
                        var ldevice = _infoModelService.GetLDevicesFromDevices(_device)
                            .FirstOrDefault((lDevice => lDevice.Inst == dataSetToSave.SelectedParentLd));
                        var ln = ldevice.AlLogicalNodes.FirstOrDefault(
                            (node => node.Name == dataSetToSave.SelectedParentLn));

                        if (dataSetsExisting.Any((set => set.Name == dataSetToSave.EditableNamePart)))
                        {
                            ln.ChildModelElements.Remove(dataSetsExisting.First((set =>
                                set.Name == dataSetToSave.EditableNamePart)));
                        }

                        IDataSet dataSet = _dataSetFactory.CreateDataSet(ln, dataSetToSave.EditableNamePart,
                            dataSetToSave.FcdaViewModels.Select((model => model.GetFcda())).ToList());
                    }
                }

                _gooseViewModelService.IncrementConfRevisionGooseControls(_device, dataSetNamesWithChengests);
                _reportVeiwModelService.IncrementConfRevisionReportControl(_device, dataSetNamesWithChengests);
                //_projectService.SaveCurrentProject();
                if (_connectionPoolService.GetConnection(_device.Ip).IsConnected)
                {
                    _deviceWarningsService.SetWarningOfDevice(_device.DeviceGuid, DatasetKeys.DataSetWarningKeys.DataSetsUnsavedWarningTagKey, "Datasets не соответствуют устройству");
                }
                _loggingService.LogMessage($"DataSets устройства {_device.Name} успешно сохранены",
                    SeverityEnum.Info);
            }
            catch (Exception e)
            {
                _loggingService.LogMessage($"DataSets устройства {_device.Name} сохранены c ошибкой: {e.Message}",
                    SeverityEnum.Warning);

            }
            RefreshViewModel?.Invoke();

            return new OperationResult<SavingCommandResultEnum>(SavingCommandResultEnum.SavedOk);
        }

        public async Task<OperationResult> ValidateBeforeSave()
        {
            var warnings = new List<string>();

            // Валидация имён
            var dataSetsNames = new List<string>();
            foreach (var name in _dataSetsToSave.Where(ds => ds.IsEditeble).Select(ds => ds.EditableNamePart))
            {
                if (!dataSetsNames.Contains(name))
                {
                    dataSetsNames.Add(name);
                }
            }

            foreach (var name in dataSetsNames)
            {
                var dataSets = _dataSetsToSave.Where(el => el.EditableNamePart == name && el.IsEditeble);
                if (dataSets.Count() > 1)
                {
                    foreach (var dataSetViewModel in dataSets)
                    {
                        ((ViewModelBase)dataSetViewModel).IsWarning = true;
                    }

                    var mess = $"Имеется несколько DataSets с именем {name}";
                    _loggingService.LogMessage(mess, SeverityEnum.Warning);
                    warnings.Add(mess);
                }

                if (!_dataSetNameService.GetIsDynamic(name))
                {
                    var dataset = _dataSetsToSave.First(ds => ds.EditableNamePart == name && ds.IsEditeble);
                    ((ViewModelBase) dataset).IsWarning = true;
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

        #endregion
    }
}