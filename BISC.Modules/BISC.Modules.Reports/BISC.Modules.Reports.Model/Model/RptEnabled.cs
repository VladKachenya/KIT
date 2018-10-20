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
    public class RptEnabled : ModelElement, IRptEnabled
    {
        public RptEnabled()
        {
            ElementName = ReportsKeys.ReportsModelKeys.RptEnabledModelKey;
        }
        public int Max { get; set; }

        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (!base.ModelElementCompareTo(obj)) return false;
            if (!(obj is IRptEnabled)) return false;
            var element = obj as IRptEnabled;
            if (element.Max != Max) return false;
            return true;
        }
    }
}
