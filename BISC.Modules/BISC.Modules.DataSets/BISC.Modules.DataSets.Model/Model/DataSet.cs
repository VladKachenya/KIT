using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.DataSets.Infrastructure.Keys;
using BISC.Modules.DataSets.Infrastructure.Model;

namespace BISC.Modules.DataSets.Model.Model
{
   public class DataSet:ModelElement,IDataSet
    {

        public DataSet()
        {
            ElementName = DatasetKeys.DatasetModelKeys.DataSetModelKey;
        }
        #region Implementation of IDataSet

        public ChildModelsList<IFcda> FcdaList => new ChildModelsList<IFcda>(this,DatasetKeys.DatasetModelKeys.FcdaModelKey);
        public string Name { get; set; }
        public bool IsDynamic { get; set; }
        #endregion

        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (!base.ModelElementCompareTo(obj)) return false;
            if (!(obj is IDataSet)) return false;
            var element = obj as IDataSet;
            if (element.Name != Name) return false;
            if (element.IsDynamic != IsDynamic) return false;
            return true;
        }
    }
}
