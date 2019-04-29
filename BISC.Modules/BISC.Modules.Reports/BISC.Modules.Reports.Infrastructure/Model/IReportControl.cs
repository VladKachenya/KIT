using BISC.Model.Infrastructure.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;

namespace BISC.Modules.Reports.Infrastructure.Model
{
    public interface IReportControl : IModelElement
    {
        string Name { get; set; }
        string RptID { get; set; }
        bool Buffered { get; set; }
        int BufTime { get; set; }
        string DataSet { get; set; }
        int IntgPd { get; set; }
        int ConfRev { get; set; }
        bool IsDynamic { get;}
        ChildModelProperty<ITrgOps> TrgOps { get; }
        ChildModelProperty<IOptFields> OptFields { get; }
        ChildModelProperty<IRptEnabled> RptEnabled { get; }
        bool RptEnabledBool { get; set; }
        bool GiBool { get; set; }
    }
}
