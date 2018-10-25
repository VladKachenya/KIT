using System.Collections.Generic;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.DataSets.Infrastructure.Model;

namespace BISC.Modules.DataSets.Infrastructure.Services
{
    public interface IDatasetModelService
    {
        void AddDatasetToDevice(IDataSet dataSet, IModelElement device, string ldName = null, string lnName = null);
        void DeleteDatasetFromDevice(IDataSet dataSet, IModelElement device, string ldName = null, string lnName = null);
        void DeleteAllDatasetsFromDevice(IModelElement device);
        List<IDataSet> GetAllDataSetOfDevice(IModelElement device);
    }
}