using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Loading;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.DataSets.Infrastructure.Factorys;
using BISC.Modules.DataSets.Infrastructure.Keys;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.DataSets.Model.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Services;

namespace BISC.Modules.DataSets.Model.Services
{
    public class DatasetsLoadingService : IDeviceElementLoadingService
    {
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly IInfoModelService _infoModelService;
        private readonly IDataSetModelService _dataSetModelService;
        private readonly IDeviceWarningsService _deviceWarningsService;
        private readonly IFcdaFactory _fcdaFactory;

        private Dictionary<string, List<string>> _ldDatasetDictionary = new Dictionary<string, List<string>>();

        public DatasetsLoadingService(
            IConnectionPoolService connectionPoolService, 
            IInfoModelService infoModelService,
            IDataSetModelService dataSetModelService, 
            IDeviceWarningsService deviceWarningsService,
            IFcdaFactory fcdaFactory)
        {
            _connectionPoolService = connectionPoolService;
            _infoModelService = infoModelService;
            _dataSetModelService = dataSetModelService;
            _deviceWarningsService = deviceWarningsService;
            _fcdaFactory = fcdaFactory;
        }



        #region Implementation of IDisposable

        public void Dispose()
        {

        }

        #endregion

        #region Implementation of IDeviceElementLoadingService

        public async Task<int> EstimateProgress(IDevice device)
        {
            var connection = _connectionPoolService.GetConnection(device.Ip);
            _ldDatasetDictionary.Clear();
            var ldevices = (await connection.MmsConnection
                .GetLdListAsync()).Item;
            int count = 0;
            foreach (var lDevice in ldevices)
            {
                var datasets = await connection.MmsConnection.GetListDataSetsAsync(lDevice, true);
                _ldDatasetDictionary.Add(lDevice, datasets.Item);
                count += datasets.Item.Count;
            }

            return count;
        }

        public async Task Load(IDevice device, IProgress<object> deviceLoadingProgress, ISclModel sclModel,CancellationToken cancellationToken)
        {
            var connection = _connectionPoolService.GetConnection(device.Ip);
            _dataSetModelService.DeleteAllDatasetsFromDevice(device);
            try
            {
                foreach (var ldevice in _ldDatasetDictionary.Keys)
                {
                    foreach (var datasetRef in _ldDatasetDictionary[ldevice])
                    {
                        string lnName = datasetRef.Split('$').First();
                        string dsName = datasetRef.Split('$').Last();
                        var dsDto = await connection.MmsConnection.GetListDataSetInfoAsync(ldevice, lnName, dsName, true);
                        IDataSet dataSet = new DataSet();
                        dataSet.Name = dsName;
                        //dataSet.IsDynamic = dsDto.Item.IsDynamic;
                        var ldevices = _infoModelService.GetLDevicesFromDevices(device);

                        foreach (var fcdaString in dsDto.Item.FcdaList)
                        {

                            var fcdaParts = fcdaString.Split('$');

                            var ldOfFcda = ldevices.First((ldevice1 =>
                                ldevice1.Inst == fcdaParts[0].Replace(device.Name, "")));
                            ILogicalNode lnOfFcda = null;
                            if (fcdaParts[1] == "LLN0")
                            {
                                lnOfFcda = ldOfFcda.LogicalNodeZero.Value;
                            }
                            else
                            {
                                lnOfFcda = ldOfFcda.LogicalNodes.FirstOrDefault((ln) => ln.Name == fcdaParts[1]);
                            }

                            if (lnOfFcda != null)
                            {
                                var modelElement = lnOfFcda as IModelElement;
                                for (int i = 3; i < fcdaParts.Length; i++)
                                {
                                    modelElement = modelElement.ChildModelElements.
                                        First(me => me is INameable nameableElement && nameableElement.Name == fcdaParts[i]);
                                }

                                IFcda fcda = null;

                                if (modelElement is IDai daModelElement)
                                {
                                    fcda = _fcdaFactory.GetFcda(daModelElement, fcdaParts[2]);
                                }
                                else
                                {
                                    fcda = _fcdaFactory.GetStructFcda(modelElement, fcdaParts[2]);
                                }

                                dataSet.FcdaList.Add(fcda);
                            }
                        }

                        var ldeviceOfDataset =
                            ldevices.First((lDevice => lDevice.Inst == ldevice.Replace(device.Name, "")));
                        if (lnName == "LLN0")
                        {
                            ldeviceOfDataset.LogicalNodeZero.Value.ChildModelElements.Add(dataSet);
                            dataSet.ParentModelElement = ldeviceOfDataset.LogicalNodeZero.Value;
                        }
                        else
                        {
                            ldeviceOfDataset.LogicalNodes.First((node => node.Name == lnName)).ChildModelElements
                                .Add(dataSet);
                            dataSet.ParentModelElement = ldeviceOfDataset.LogicalNodes.First((node => node.Name == lnName));
                        }

                        deviceLoadingProgress?.Report(new object());
                    }
                }
            }
            catch (Exception e)
            {
                _deviceWarningsService.SetWarningOfDevice(device.DeviceGuid, DatasetKeys.DataSetWarningKeys.DataSetLoadErrorWarningTagKey, "Ошибка вычитывания DataSets из устройства. Проверьте подключение");
                throw;
            }
        }




        private IFcda CreateComplexFcda(string[] fcdaParts, IDoi doiOfFcda, string ldStr, string lnStr)
            {
                IFcda fcda = null;
                object currentFcdaObject;
                string doName = doiOfFcda.Name;
                string daName = null;
                currentFcdaObject = doiOfFcda.DaiCollection.FirstOrDefault((dai => dai.Name == fcdaParts[4]));
                if (currentFcdaObject == null)
                {
                    currentFcdaObject = doiOfFcda.SdiCollection.FirstOrDefault((dai1 => dai1.Name == fcdaParts[4]));
                    if (currentFcdaObject is IDai dai)
                    {
                        doName += "." + dai.Name;
                    }

                    if (currentFcdaObject is ISdi sdi)
                    {
                        doName += "." + sdi.Name;
                    }
                }
                else
                {
                    daName = (currentFcdaObject as IDai)?.Name;
                }

                for (int i = 5; i < fcdaParts.Length; i++)
                {
                    if (currentFcdaObject is IDai)
                    {

                    }
                    if (currentFcdaObject is ISdi)
                    {
                        var o = (currentFcdaObject as ISdi).DaiCollection.FirstOrDefault((dai => dai.Name == fcdaParts[i]));


                        if (o == null)
                        {
                            currentFcdaObject = (currentFcdaObject as ISdi).SdiCollection.FirstOrDefault((dai => dai.Name == fcdaParts[i]));
                            if (currentFcdaObject is IDai dai2)
                            {
                                doName += "." + dai2.Name;
                            }

                            if (currentFcdaObject is ISdi sdi)
                            {
                                doName += "." + sdi.Name;
                            }

                    }
                        else
                        {
                            currentFcdaObject = o;
                            daName = (currentFcdaObject as IDai)?.Name;

                        }

                    }




                }
                fcda = new Fcda(
                    ldStr + "/" +
                    lnStr + "." +
                    doiOfFcda.Name, doName, daName, fcdaParts[2]);
                return fcda;
            }




        public int Priority => 10;

        #endregion
    }
}
