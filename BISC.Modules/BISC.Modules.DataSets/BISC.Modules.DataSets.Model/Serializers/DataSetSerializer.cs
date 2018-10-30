using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Global.Serializators;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Serializing;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Model.Model;

namespace BISC.Modules.DataSets.Model.Serializers
{
    public class DataSetSerializer : DefaultModelElementSerializer<IDataSet>
    {
        public DataSetSerializer()
        {
            //RegisterModelElementCollection(typeof(IFcda));
            RegisterProperty(nameof(IDataSet.Name),"name");
            RegisterProperty(nameof(IDataSet.IsDynamic), "isDynamic",SerializingType.Extended);
        }

        #region Overrides of DefaultModelElementSerializer<IDataSet>

        public override IModelElement GetConcreteObject()
        {
            return new DataSet();
        }

        #endregion
    }
}