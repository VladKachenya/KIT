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
    public class ReportControl : ModelElement, IReportControl
    {
        public ReportControl()
        {
            ElementName = ReportsKeys.ReportsModelKeys.ReportControlModelKey;
        }
        public string Name { get; set; }
        public string RptID { get; set; }
        public bool Buffered { get; set; }
        public int BufTime { get; set; }
        public string DataSet { get; set; }
        public int IntgPd { get; set; }
        public string ConfRev { get; set; }
        public bool IsDynamic { get; set; }


        public ChildModelProperty<ITrgOps> TrgOps => new ChildModelProperty<ITrgOps>(this, ReportsKeys.ReportsModelKeys.TrgOpsModelKey);
        public ChildModelProperty<IOptFields> OptFields => new ChildModelProperty<IOptFields>(this, ReportsKeys.ReportsModelKeys.OptFieldsModelKey);
        public ChildModelProperty<IRptEnabled> RptEnabled => new ChildModelProperty<IRptEnabled>(this, ReportsKeys.ReportsModelKeys.RptEnabledModelKey);

        public override bool ModelElementCompareTo(IModelElement obj)
        {
            if (!base.ModelElementCompareTo(obj)) return false;
            if (!(obj is IReportControl)) return false;
            var element = obj as IReportControl;
            if (element.Name != Name) return false;
            if (element.RptID != RptID) return false;
            if (element.Buffered != Buffered) return false;
            if (element.BufTime != BufTime) return false;
            if (element.DataSet != DataSet) return false;
            if (element.IntgPd != IntgPd) return false;
            if (element.ConfRev != ConfRev) return false;
            if (element.IsDynamic != IsDynamic) return false;
            return true;
        }
    }
}
