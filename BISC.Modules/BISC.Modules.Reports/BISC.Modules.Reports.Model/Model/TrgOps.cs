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
    public class TrgOps : ModelElement, ITrgOps
    {
        public TrgOps()
        {
            ElementName = ReportsKeys.ReportsModelKeys.TrgOpsModelKey;
        }
        public bool Dchg { get; set; }
        public bool Qchg { get; set; }
        public bool Dupd { get; set; }
        public bool Period { get; set; }
        public bool Gi { get; set; }

        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (!base.ModelElementCompareTo(obj)) return false;
            if (!(obj is ITrgOps)) return false;
            var element = obj as ITrgOps;
            if (element.Dchg != Dchg) return false;
            if (element.Qchg != Qchg) return false;
            if (element.Dupd != Dupd) return false;
            if (element.Period != Period) return false;
            if (element.Gi != Gi) return false;
            return true;
        }
    }
}
