using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.DataSets.Infrastructure.Services;

namespace BISC.Modules.DataSets.Model.Services
{
   public class DatasetModelService: IDatasetModelService
    {
        #region Implementation of IDatasetModelService

        public void AddDatasetToDevice(IModelElement device, string ldName, string lnName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
