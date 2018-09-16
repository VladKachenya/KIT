using System.Collections.Generic;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.DataSets.Infrastructure.Model
{
    public interface IDataSet:IModelElement
    {
        List<IFcda> FcdaList { get; }
        string Name { get; set; }
    }
}