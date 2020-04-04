using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Services.Communication;
using BISC.Modules.Connection.Infrastructure.Connection;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.DataSets.Infrastructure.Factorys;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.DataSets.Infrastructure.ViewModels;
using BISC.Modules.DataSets.Model.Mappers;
using BISC.Modules.DataSets.Presentation.Services.Interfaces;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Saving;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Services;

namespace BISC.Modules.DataSets.Presentation.Services
{
    public class DataSetSavingService : IDeviceElementSavingService
    {
        private readonly IFtpDataSetModelService _ftpDataSetModelService;
        private readonly IConnectionPoolService _connectionPoolService;
        private readonly IDatasetModelService _datasetModelService;

        public DataSetSavingService(
            IFtpDataSetModelService ftpDataSetModelService,
            IConnectionPoolService connectionPoolService,
            IDatasetModelService datasetModelService)
        {
            _ftpDataSetModelService = ftpDataSetModelService;
            _connectionPoolService = connectionPoolService;
            _datasetModelService = datasetModelService;
        }
        


        #region Implementation of IDeviceElementSavingService

        public int Priority => 10;
        public async Task<OperationResult> Save(IDevice device)
        {
            if (_connectionPoolService.GetConnection(device.Ip).IsConnected)
            {
                var dsToSaveByFtp = _datasetModelService.GetDynamicDataSetsFromProject(device.Ip);
                return await _ftpDataSetModelService.WriteDatasetsToDevice(device.Ip, dsToSaveByFtp);
            }
            return new OperationResult("Ошибка загрузки DataSet");
        }

        #endregion
    }
}
