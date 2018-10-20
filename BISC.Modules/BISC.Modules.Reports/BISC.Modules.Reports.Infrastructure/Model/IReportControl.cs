using BISC.Model.Infrastructure.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        string ConfRev { get; set; }
        ChildModelProperty<ITrgOps> TrgOps { get; }
        ChildModelProperty<IOptFields> OptFields { get; }
        ChildModelProperty<IRptEnabled> RptEnabled { get; }

    }
}
