using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Loading;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.InformationModel.Infrastucture.Services;

namespace BISC.Modules.DataSets.Model.Services
{
   public class DatasetsLoadingService: IDeviceElementLoadingService
    {
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly IInfoModelService _infoModelService;
        private readonly IDatasetModelService _datasetModelService;
        private Dictionary<string,List<string>> _ldDatasetDictionary=new Dictionary<string, List<string>>();
        public DatasetsLoadingService(IConnectionPoolService connectionPoolService,IInfoModelService infoModelService,IDatasetModelService datasetModelService)
        {
            _connectionPoolService = connectionPoolService;
            _infoModelService = infoModelService;
            _datasetModelService = datasetModelService;
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
            var ldevices = (await connection.MmsConnection
                .GetLdListAsync()).Item;
            int count = 0;
            foreach (var lDevice in ldevices)
            {
                var datasets = await connection.MmsConnection.GetListDataSetsAsync(lDevice, true);
                _ldDatasetDictionary.Add(lDevice,datasets.Item);
                count += datasets.Item.Count;
            }

            return count;
        }

        public async Task Load(IDevice device, IProgress<object> deviceLoadingProgress, ISclModel sclModel)
        {
            var connection = _connectionPoolService.GetConnection(device.Ip);
            foreach (var ldevice in _ldDatasetDictionary.Keys)
            {
                foreach (var datasetRef in _ldDatasetDictionary[ldevice])
                {
                    string lnName = datasetRef.Split('$').First();
                    string dsName = datasetRef.Split('$').Last();
                    var dsDto =await connection.MmsConnection.GetListDataSetInfoAsync(ldevice, lnName, dsName, true);
                }

            }
        }

        public int Priority => 10;

        #endregion
    }
}
