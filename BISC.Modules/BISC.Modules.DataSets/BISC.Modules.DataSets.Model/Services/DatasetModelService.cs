using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.InformationModel.Infrastucture.Services;

namespace BISC.Modules.DataSets.Model.Services
{
   public class DatasetModelService: IDatasetModelService
    {
        private readonly IInfoModelService _infoModelService;

        public DatasetModelService(IInfoModelService infoModelService)
        {
            _infoModelService = infoModelService;
        }


        #region Implementation of IDatasetModelService

        public void AddDatasetToDevice(IModelElement device, string ldName, string lnName)
        {
            throw new NotImplementedException();
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

                lDevice.LogicalNodeZero.ChildModelElements.ForEach((element =>
                {
                    if (element is IDataSet dataSet)
                    {
                        dataSets.Add(dataSet);
                    }
                }));
            }

            return dataSets;
        }

        #endregion
    }
}
