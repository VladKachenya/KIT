using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Model;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.DataSets.Infrastructure.Keys;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.DataSets.Model.Services;

namespace BISC.Modules.DataSets.Model.Model
{
   public class DataSet:ModelElement,IDataSet
    {
        private readonly IDataSetNameService _dataSetNameService = new DataSetNameService();

        public DataSet()
        {
            ElementName = DatasetKeys.DatasetModelKeys.DataSetModelKey;
        }
        #region Implementation of IDataSet

        public ChildModelsList<IFcda> FcdaList => new ChildModelsList<IFcda>(this,DatasetKeys.DatasetModelKeys.FcdaModelKey);
        public string Name { get; set; }

        public bool IsDynamic => _dataSetNameService.GetIsDynamic(Name);

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
