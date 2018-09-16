using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Modules.DataSets.Infrastructure.Keys;
using BISC.Modules.DataSets.Infrastructure.Model;

namespace BISC.Modules.DataSets.Model.Model
{
   public class DataSet:ModelElement,IDataSet
    {

        public DataSet()
        {
            ElementName = DatasetKeys.DatasetModelKeys.DataSetModelKey;
            FcdaList=new List<IFcda>();
        }
        #region Implementation of IDataSet

        public List<IFcda> FcdaList { get; }
        public string Name { get; set; }

        #endregion
    }
}
