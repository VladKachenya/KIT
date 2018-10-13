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
        public IDataSet CreateDataSet(IModelElement parent, string name, List<IFcda> fcdas)
        {
            var dataSet = new DataSet() {Name = name, ParentModelElement = parent, IsDynamic = true};
            dataSet.FcdaList.AddRange(fcdas);
            parent.ChildModelElements.Add(dataSet);
            return dataSet;
        }
    }
}