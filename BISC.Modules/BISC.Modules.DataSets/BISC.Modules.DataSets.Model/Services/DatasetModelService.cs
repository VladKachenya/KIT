using System;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.DataSets.Infrastructure.Keys;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Services;
using System.Collections.Generic;
using System.Linq;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;

namespace BISC.Modules.DataSets.Model.Services
{
    public class DatasetModelService : IDatasetModelService
    {
        private readonly IInfoModelService _infoModelService;
        private readonly IBiscProject _biscProject;
        private readonly IDeviceModelService _deviceModelService;

        public DatasetModelService(
            IInfoModelService infoModelService, 
            IBiscProject biscProject, 
            IDeviceModelService deviceModelService)
        {
            _infoModelService = infoModelService;
            _biscProject = biscProject;
            _deviceModelService = deviceModelService;
        }

        #region private methods

        private ILogicalNode TryFindLn(IDataSet dataSet, IModelElement device, string ldName = null, string lnFullName = null)
        {
            // первое приближение 
            if (ldName == null || lnFullName == null)
            {
                ldName = _infoModelService.GetParentLDevice(dataSet)?.Inst;
                lnFullName = _infoModelService.GetFullNameOfLogicalNode(_infoModelService.GetParentLogicalNode(dataSet));
            }
            ILogicalNode logicalNode = null;
            List<ILDevice> lDevices = _infoModelService.GetLDevicesFromDevices(device);
            foreach (var lDevice in lDevices)
            {
                if (lDevice.Inst == ldName)
                {
                    if (lDevice.LogicalNodeZero.Value.Name == lnFullName)
                    {
                        logicalNode = lDevice.LogicalNodeZero.Value;
                    }

                    if (logicalNode == null)
                    {
                        foreach (ILogicalNode ln in lDevice.LogicalNodes)
                        {
                            string lnName = _infoModelService.GetFullNameOfLogicalNode(ln);
                            if (lnName == lnFullName)
                            {
                                logicalNode = ln;
                                break;
                            }
                        }
                    }
                    if (logicalNode != null)
                    {
                        break;
                    }
                }
            }
            return logicalNode;
        }
        #endregion


        #region Implementation of IDatasetModelService
        public void DeleteDatasetFromDevice(IDataSet dataSet, IModelElement device, string ldName = null, string lnFullName = null)
        {
            string dsName = dataSet.Name;
            ILogicalNode logicalNode = TryFindLn(dataSet, device, ldName, lnFullName);
            if (logicalNode != null)
            {
                List<IModelElement> nodeDataSets = new List<IModelElement>(logicalNode.ChildModelElements);
                foreach (var ds in nodeDataSets)
                {
                    if (ds is IDataSet set)
                    {
                        if (set.Name == dsName)
                        {
                            logicalNode.ChildModelElements.Remove(set);
                        }
                    }
                }
            }
        }
        public void DeleteAllDatasetsFromDevice(IModelElement device)
        {
            List<ILDevice> lDevices = _infoModelService.GetLDevicesFromDevices(device);
            foreach (var lDevice in lDevices)
            {
                foreach (var logicalNode in lDevice.AlLogicalNodes)
                {
                    var datasetsToDelete = logicalNode.ChildModelElements.Where((element =>
                        element.ElementName == DatasetKeys.DatasetModelKeys.DataSetModelKey)).ToList();
                    foreach (var datasetToDelete in datasetsToDelete)
                    {
                        logicalNode.ChildModelElements.Remove(datasetToDelete);
                    }
                }
            }
        }
        public void AddDatasetToDevice(IDataSet dataSet, IModelElement device, string ldName = null, string lnFullName = null)
        {
            // первое приближение 
            string dsName = dataSet.Name;
            ILogicalNode logicalNode = TryFindLn(dataSet, device, ldName, lnFullName);
            if (logicalNode != null)
            {
                foreach (var ds in logicalNode.ChildModelElements)
                {
                    if (ds is IDataSet)
                    {
                        if ((ds as IDataSet).Name == dsName)
                        {
                            return;
                        }
                    }
                }

                dataSet.ParentModelElement = logicalNode;
                logicalNode.ChildModelElements.Add(dataSet);
                return;
            }
        }


        public List<IDataSet> GetAllDataSetOfDevice(IModelElement device)
        {
            List<IDataSet> dataSets = new List<IDataSet>();
            var ldevices = _infoModelService.GetLDevicesFromDevices(device);
            foreach (var lDevice in ldevices)
            {
                foreach (var logicalNode in lDevice.LogicalNodes)
                {
                    logicalNode.ChildModelElements.ForEach((element =>
                    {
                        if (element is IDataSet dataSet)
                        {
                            dataSets.Add(dataSet);
                        }
                    }));
                }

                lDevice.LogicalNodeZero.Value.ChildModelElements.ForEach((element =>
                {
                    if (element is IDataSet dataSet)
                    {
                        dataSets.Add(dataSet);
                    }
                }));
            }

            return dataSets;
        }

        public List<IDataSet> GetDynamicDataSetsFromProject(string deviceIp)
        {
            var device = _deviceModelService.GetDeviceByIp(_biscProject.MainSclModel.Value, deviceIp);
            return GetAllDataSetOfDevice(device).Where(ds => ds.IsDynamic).ToList();
        }


        public IDataSet GetDataSetOfDevice(IModelElement device, string dataSetName)
        {
            IDataSet res;
            try
            {
                res = GetAllDataSetOfDevice(device).First(ds => ds.Name == dataSetName);
            }
            catch (Exception e)
            {
                throw new Exception($"Неудалось найти DataSet {dataSetName} в устройстве {((IDevice)device).Name}");
            }
            return res;
        }

        #endregion
    }
}
