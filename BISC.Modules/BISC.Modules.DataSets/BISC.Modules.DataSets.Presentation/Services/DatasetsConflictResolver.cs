using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.DataSets.Infrastructure.Keys;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.DataSets.Infrastructure.ViewModels.Factorys;
using BISC.Modules.DataSets.Presentation.Commands;
using BISC.Modules.DataSets.Presentation.Services;
using BISC.Modules.DataSets.Presentation.ViewModels.Helpers;
using BISC.Modules.Device.Infrastructure.HelpClasses;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.InformationModel.Infrastucture.Services;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BISC.Modules.DataSets.Model.Services
{
    public class DatasetsConflictResolver : IElementConflictResolver
    {
        private readonly IDatasetModelService _datasetModelService;
        private readonly IDeviceModelService _deviceModelService;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly INavigationService _navigationService;
        private readonly IDatasetViewModelFactory _datasetViewModelFactory;
        private readonly IInfoModelService _infoModelService;
        private readonly DatasetsProjectSavingCommand _datasetsProjectSavingCommand;
        private readonly DataSetSavingService _dataSetSavingService;


        public DatasetsConflictResolver(IDatasetModelService datasetModelService, IDeviceModelService deviceModelService, IConnectionPoolService connectionPoolService,
            INavigationService navigationService, IDatasetViewModelFactory datasetViewModelFactory, IInfoModelService infoModelService,
            DatasetsProjectSavingCommand datasetsProjectSavingCommand, DataSetSavingService dataSetSavingService)
        {
            _datasetModelService = datasetModelService;
            _deviceModelService = deviceModelService;
            _connectionPoolService = connectionPoolService;
            _navigationService = navigationService;
            _datasetViewModelFactory = datasetViewModelFactory;
            _infoModelService = infoModelService;
            _datasetsProjectSavingCommand = datasetsProjectSavingCommand;
            _dataSetSavingService = dataSetSavingService;
        }

        #region Implementation of IElementConflictResolver

        public string ConflictName => "DataSets";
        public ConflictType ConflictType => ConflictType.ManualResolveNeeded;

        public bool GetIfConflictsExists(Guid deviceGuid, ISclModel sclModelInDevice, ISclModel sclModelInProject)
        {
            var deviceInsclModelInDevice = _deviceModelService.GetDeviceByGuid(sclModelInDevice, deviceGuid);
            var devicesclModelInProject = _deviceModelService.GetDeviceByGuid(sclModelInProject, deviceGuid);

            var datasetsInDevice = _datasetModelService.GetAllDataSetOfDevice(deviceInsclModelInDevice);
            var datasetsInProject = _datasetModelService.GetAllDataSetOfDevice(devicesclModelInProject);

            if (datasetsInProject.Count != datasetsInDevice.Count)
            {
                return true;
            }

            foreach (var datasetInDevice in datasetsInDevice)
            {
                var datasetInProject = datasetsInProject.FirstOrDefault((ds => ds.Name == datasetInDevice.Name));
                if (datasetInProject == null)
                {
                    return true;
                }

                if (!datasetInDevice.ModelElementCompareTo(datasetInProject))
                {
                    return true;
                }
            }
            return false;

        }

        public async Task<ResolvingResult> ResolveConflict(bool isFromDevice, Guid deviceGuid, ISclModel sclModelInDevice, ISclModel sclModelInProject)
        {
            var deviceInsclModelInDevice = _deviceModelService.GetDeviceByGuid(sclModelInDevice, deviceGuid);
            var devicesclModelInProject = _deviceModelService.GetDeviceByGuid(sclModelInProject, deviceGuid);

            var datasetsInDevice = _datasetModelService.GetAllDataSetOfDevice(deviceInsclModelInDevice);
            var datasetsInProject = _datasetModelService.GetAllDataSetOfDevice(devicesclModelInProject);

            var datasetsViewModelsInDevice = _datasetViewModelFactory.GetDataSetsViewModel(datasetsInDevice);
            var datasetsViewModelsInProject = _datasetViewModelFactory.GetDataSetsViewModel(datasetsInProject);

            foreach (var dataSetViewModel in datasetsViewModelsInProject)
            {
                dataSetViewModel.ChangeTracker.SetTrackingEnabled(true);
            }

            foreach (var dataSetViewModel in datasetsViewModelsInProject)
            {
                dataSetViewModel.ChangeTracker.SetTrackingEnabled(true);
            }

            var deviceOnlydatasetsViewModels = _datasetViewModelFactory.GetDataSetsViewModel(datasetsInDevice);
            var projectOnlydatasetsViewModels = _datasetViewModelFactory.GetDataSetsViewModel(datasetsInProject);

            foreach (var dataSetViewModel in deviceOnlydatasetsViewModels)
            {
                datasetsViewModelsInDevice?.FirstOrDefault(model => model.EditableNamePart == dataSetViewModel.EditableNamePart)?.ChangeTracker.SetModified();
            }

            foreach (var dataSetViewModel in projectOnlydatasetsViewModels)
            {
                datasetsViewModelsInProject?.FirstOrDefault(model => model.EditableNamePart == dataSetViewModel.EditableNamePart)?.ChangeTracker.SetModified();
            }

            if (isFromDevice)//подтянуть различия из устройства
            {
                _datasetModelService.DeleteAllDatasetsFromDevice(devicesclModelInProject);
                foreach (var dataSetToAdd in datasetsInDevice)
                {
                    _datasetModelService.AddDatasetToDevice(dataSetToAdd, devicesclModelInProject, _infoModelService.GetParentLDevice(dataSetToAdd).Inst, _infoModelService.GetFullNameOfLogicalNode(_infoModelService.GetParentLogicalNode(dataSetToAdd)));
                }
            }
            else
            {
                if (_connectionPoolService.GetConnection(deviceInsclModelInDevice.Ip).IsConnected)
                {
                    _datasetsProjectSavingCommand.Initialize(ref datasetsViewModelsInProject, deviceInsclModelInDevice, datasetsViewModelsInProject.Select(el => el.ChangeTracker).ToArray());
                    await _datasetsProjectSavingCommand.SaveAsync();
                    await _dataSetSavingService.Save(devicesclModelInProject);
                    return new ResolvingResult() { IsRestartNeeded = true };

                }
                return new ResolvingResult("Устройтво не отвечает");
            }
            return ResolvingResult.SucceedResult;
        }

        public void ShowConflicts(Guid deviceGuid, ISclModel sclModelInDevice, ISclModel sclModelInProject)
        {
            var deviceInsclModelInDevice = _deviceModelService.GetDeviceByGuid(sclModelInDevice, deviceGuid);
            var devicesclModelInProject = _deviceModelService.GetDeviceByGuid(sclModelInProject, deviceGuid);

            var datasetsInDevice = _datasetModelService.GetAllDataSetOfDevice(deviceInsclModelInDevice);
            var datasetsInProject = _datasetModelService.GetAllDataSetOfDevice(devicesclModelInProject);

            var projectOnlydatasets = GetProjectOnlyList(datasetsInDevice, datasetsInProject);
            var deviceOnlydatasets = GetDeviceOnlyList(datasetsInDevice, datasetsInProject);

            var datasetsInDeviceVms = _datasetViewModelFactory.GetDataSetsViewModel(datasetsInDevice);
            var datasetsInProjectVms = _datasetViewModelFactory.GetDataSetsViewModel(datasetsInProject);

            foreach (var datasetsInProjectVm in datasetsInProjectVms)
            {
                datasetsInProjectVm.ChangeTracker.AcceptChanges();
            }
            foreach (var datasetsInDeviceVm in datasetsInDeviceVms)
            {
                datasetsInDeviceVm.ChangeTracker.AcceptChanges();
            }

            projectOnlydatasets.ForEach((set => datasetsInProjectVms.FirstOrDefault((model => model.EditableNamePart == set.Name))?.ChangeTracker.SetModified()));
            deviceOnlydatasets.ForEach((set => datasetsInDeviceVms.FirstOrDefault((model => model.EditableNamePart == set.Name))?.ChangeTracker.SetModified()));

            _navigationService.OpenInWindow(DatasetKeys.DatasetViewModelKeys.DatasetConflictsWindow, $"Dataset конфликты в устройстве {deviceGuid}",
                new BiscNavigationParameters().AddParameterByName(DatasetKeys.DatasetViewModelKeys.DatasetsConflictContextKey,
                    new DatasetsConflictContext(datasetsInDeviceVms, datasetsInProjectVms)));

        }

        private List<IDataSet> GetDeviceOnlyList(List<IDataSet> datasetsInDevice, List<IDataSet> datasetsInProject)
        {
            List<IDataSet> deviceOnlyList = new List<IDataSet>();
            foreach (var dataSetInDevice in datasetsInDevice)
            {
                if (!datasetsInProject.Any((set => set.ModelElementCompareTo(dataSetInDevice))))
                {
                    deviceOnlyList.Add(dataSetInDevice);
                }
            }
            return deviceOnlyList;
        }

        private List<IDataSet> GetProjectOnlyList(List<IDataSet> datasetsInDevice, List<IDataSet> datasetsInProject)
        {
            List<IDataSet> projectOnlyList = new List<IDataSet>();
            foreach (var datasetInProject in datasetsInProject)
            {
                if (!datasetsInDevice.Any((set => set.ModelElementCompareTo(datasetInProject))))
                {
                    projectOnlyList.Add(datasetInProject);
                }
            }
            return projectOnlyList;
        }


        #endregion
    }
}
