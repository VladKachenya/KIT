using BISC.Model.Global.Serializators;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Modules.Reports.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Reports.Model.Serializers
{
    public class OptFieldsSerializer : DefaultModelElementSerializer<IOptFields>
    {
        public OptFieldsSerializer()
        {
            RegisterProperty(nameof(IOptFields.SeqNum), "seqNum");
            RegisterProperty(nameof(IOptFields.TimeStamp), "timeStamp");
            RegisterProperty(nameof(IOptFields.DataSet), "dataSet");
            RegisterProperty(nameof(IOptFields.ReasonCode), "reasonCode");
            RegisterProperty(nameof(IOptFields.DataRef), "dataRef");
            RegisterProperty(nameof(IOptFields.EntryID), "entryID");
            RegisterProperty(nameof(IOptFields.ConfigRef), "configRef");
            RegisterProperty(nameof(IOptFields.BufOvfl), "bufOvfl");
            RegisterProperty(nameof(IOptFields.Segmentation), "segmentation");

        }

        public override IModelElement GetConcreteObject()
        {
            return new OptFields();
        }

    }
}
