using BISC.Model.Infrastructure.Elements;
using BISC.Modules.DataSets.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.DataSets.Infrastructure.Factorys
{
    public interface IDataSetFactory
    {
        IDataSet GetDataSet(IModelElement parient, string name);
    }
}
