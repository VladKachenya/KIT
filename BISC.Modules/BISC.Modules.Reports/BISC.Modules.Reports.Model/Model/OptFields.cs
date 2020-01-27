using BISC.Model.Global.Model;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Reports.Infrastructure.Keys;
using BISC.Modules.Reports.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Reports.Model.Model
{
    public class OptFields : ModelElement, IOptFields
    {
        public OptFields()
        {
            ElementName = ReportsKeys.ReportsModelKeys.OptFieldsModelKey;
        }
        public bool SeqNum { get; set; }
        public bool TimeStamp { get; set; }
        public bool DataSet { get; set; }
        public bool ReasonCode { get; set; }
        public bool DataRef { get; set; }
        public bool EntryID { get; set; }
        public bool ConfigRef { get; set; }
        public bool BufOvfl { get; set; }
        public bool Segmentation { get; set; }

        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (!base.ModelElementCompareTo(obj)) return false;
            if (!(obj is IOptFields)) return false;
            var element = obj as IOptFields;
            //if (element.SeqNum != SeqNum) return false;
            //if (element.TimeStamp != TimeStamp) return false;
            //if (element.DataSet != DataSet) return false;
            //if (element.ReasonCode != ReasonCode) return false;
            //if (element.DataRef != DataRef) return false;
            //if (element.EntryID != EntryID) return false;
            //if (element.ConfigRef != ConfigRef) return false;

            return true;
        }
    }
}
