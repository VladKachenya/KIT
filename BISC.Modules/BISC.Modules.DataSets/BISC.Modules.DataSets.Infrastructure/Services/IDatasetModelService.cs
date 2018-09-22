using System.Collections.Generic;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.DataSets.Infrastructure.Model;

namespace BISC.Modules.DataSets.Infrastructure.Services
{
    public interface IDatasetModelService
    {
        void AddDatasetToDevice(IModelElement device, string ldName, string lnName);
        List<IDataSet> GetAllDataSetOfDevice(IModelElement device);
    }
}