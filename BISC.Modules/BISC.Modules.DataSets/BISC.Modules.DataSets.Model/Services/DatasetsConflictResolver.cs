using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.DataSets.Model.Mappers;
using BISC.Modules.Device.Infrastructure.HelpClasses;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.InformationModel.Infrastucture.Elements;

namespace BISC.Modules.DataSets.Model.Services
{
    public class DatasetsConflictResolver : IElementConflictResolver
    {
        private readonly IDatasetModelService _datasetModelService;
        private readonly IDeviceModelService _deviceModelService;
        private readonly IConnectionPoolService _connectionPoolService;

        public DatasetsConflictResolver(IDatasetModelService datasetModelService, IDeviceModelService deviceModelService, IConnectionPoolService connectionPoolService)
        {
            _datasetModelService = datasetModelService;
            _deviceModelService = deviceModelService;
            _connectionPoolService = connectionPoolService;
        }



        #region Implementation of IElementConflictResolver

        public string ConflictName => "DataSets";
        public bool GetIfConflictsExists(string deviceName, ISclModel sclModelInDevice, ISclModel sclModelInProject)
        {
            var deviceInsclModelInDevice = _deviceModelService.GetDeviceByName(sclModelInDevice, deviceName);
            var devicesclModelInProject = _deviceModelService.GetDeviceByName(sclModelInProject, deviceName);

            var datasetsInDevice = _datasetModelService.GetAllDataSetOfDevice(deviceInsclModelInDevice);
            var datasetsInProject = _datasetModelService.GetAllDataSetOfDevice(devicesclModelInProject);

            if (datasetsInProject.Count != datasetsInDevice.Count)
            {
                return true;
            }

            foreach (var datasetInDevice in datasetsInDevice)
            {
                var datasetInProject = datasetsInProject.FirstOrDefault((ds => ds.Name == datasetInDevice.Name));
                if (datasetInProject == null) return true;
                if (!datasetInDevice.ModelElementCompareTo(datasetInProject))
                {
                    return true;
                }
            }
            return false;

        }

        public async Task<ResolvingResult> ResolveConflict(bool isFromDevice, string deviceName, ISclModel sclModelInDevice, ISclModel sclModelInProject)
        {
            var deviceInsclModelInDevice = _deviceModelService.GetDeviceByName(sclModelInDevice, deviceName);
            var devicesclModelInProject = _deviceModelService.GetDeviceByName(sclModelInProject, deviceName);

            var datasetsInDevice = _datasetModelService.GetAllDataSetOfDevice(deviceInsclModelInDevice);
            var datasetsInProject = _datasetModelService.GetAllDataSetOfDevice(devicesclModelInProject);

            var projectOnlydatasets = GetProjectOnlyList(datasetsInDevice, datasetsInProject);
            var deviceOnlydatasets = GetDeviceOnlyList(datasetsInDevice, datasetsInProject);



            if (isFromDevice)//подтянуть различия из устройства
            {
                foreach (var projectOnlydataset in projectOnlydatasets)
                {
                    _datasetModelService.DeleteDatasetFromDevice(projectOnlydataset, devicesclModelInProject, projectOnlydataset.GetFirstParentOfType<ILDevice>().Inst, projectOnlydataset.GetFirstParentOfType<ILogicalNode>().Name);
                }

                foreach (var deviceOnlydataset in deviceOnlydatasets)
                {
                    _datasetModelService.AddDatasetToDevice(deviceOnlydataset, devicesclModelInProject, deviceOnlydataset.GetFirstParentOfType<ILDevice>().Inst, deviceOnlydataset.GetFirstParentOfType<ILogicalNode>().Name);
                }
            }
            else
            { //записать в устройство различия
                if (_connectionPoolService.GetConnection(deviceInsclModelInDevice.Ip).IsConnected)
                {
                    foreach (var projectOnlydataset in projectOnlydatasets)
                    {
                        await _connectionPoolService.GetConnection(deviceInsclModelInDevice.Ip).MmsConnection
                            .AddDataSet(projectOnlydataset.GetFirstParentOfType<ILDevice>().Inst,
                                projectOnlydataset.GetFirstParentOfType<ILogicalNode>().Name,
                                deviceInsclModelInDevice.Name, projectOnlydataset.Name,
                                projectOnlydataset.FcdaList.ToDtos(deviceInsclModelInDevice.Name));

                        _datasetModelService.AddDatasetToDevice(projectOnlydataset, deviceInsclModelInDevice,
                            projectOnlydataset.GetFirstParentOfType<ILDevice>().Inst,
                            projectOnlydataset.GetFirstParentOfType<ILogicalNode>().Name);

                    }

                    foreach (var deviceOnlydataset in deviceOnlydatasets)
                    {
                        await _connectionPoolService.GetConnection(deviceInsclModelInDevice.Ip).MmsConnection
                            .DeleteDataSet(deviceOnlydataset.GetFirstParentOfType<ILogicalNode>().Name,
                                deviceOnlydataset.GetFirstParentOfType<ILDevice>().Inst,
                                deviceInsclModelInDevice.Name, deviceOnlydataset.Name);

                        _datasetModelService.DeleteDatasetFromDevice(deviceOnlydataset, deviceInsclModelInDevice,
                            deviceOnlydataset.GetFirstParentOfType<ILDevice>().Inst,
                            deviceOnlydataset.GetFirstParentOfType<ILogicalNode>().Name);
                    }
                }
                else
                {
                    return new ResolvingResult("Устройство не на связи");
                }
            }
            return ResolvingResult.SucceedResult;

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
