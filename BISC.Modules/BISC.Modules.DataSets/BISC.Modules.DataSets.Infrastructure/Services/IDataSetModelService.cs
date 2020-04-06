using System;
using System.Collections.Generic;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.DataSets.Infrastructure.Model;

namespace BISC.Modules.DataSets.Infrastructure.Services
{
    public interface IDataSetModelService
    {
        void AddDatasetToDevice(IDataSet dataSet, IModelElement device, string ldName = null, string lnFullName = null);
        void DeleteDatasetFromDevice(IDataSet dataSet, IModelElement device, string ldName = null, string lnFullName = null);
        void DeleteAllDatasetsFromDevice(IModelElement device);
        List<IDataSet> GetAllDataSetOfDevice(IModelElement device);
        IEnumerable<IDataSet> GetDynamicDataSets(string deviceIp);
        IEnumerable<IDataSet> GetDynamicDataSets(Guid deviceGuid, ISclModel sclModel);


        IDataSet GetDataSetOfDevice(IModelElement device, string dataSetName);
    }
}