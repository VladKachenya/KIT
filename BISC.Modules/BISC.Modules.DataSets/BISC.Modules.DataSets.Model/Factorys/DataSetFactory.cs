using BISC.Model.Infrastructure.Elements;
using BISC.Modules.DataSets.Infrastructure.Factorys;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.DataSets.Model.Factorys
{
    public class DataSetFactory : IDataSetFactory
    {
        public IDataSet GetDataSet(IModelElement parient, string name = "New DataSet")
        {
            return new DataSet() { Name = name , ParentModelElement = parient, IsDynamic = true};
        }
    }
}
