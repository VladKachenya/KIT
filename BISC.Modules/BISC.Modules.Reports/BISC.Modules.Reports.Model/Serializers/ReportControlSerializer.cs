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
    public class ReportControlSerializer : DefaultModelElementSerializer<IReportControl>
    {
        public ReportControlSerializer()
        {
            RegisterProperty(nameof(IReportControl.Name), "name");
            RegisterProperty(nameof(IReportControl.RptID), "rptID");
            RegisterProperty(nameof(IReportControl.Buffered), "buffered");
            RegisterProperty(nameof(IReportControl.BufTime), "bufTime");
            RegisterProperty(nameof(IReportControl.DataSet), "datSet");
            RegisterProperty(nameof(IReportControl.IntgPd), "intgPd");
            RegisterProperty(nameof(IReportControl.ConfRev), "confRev");
        }

        public override IModelElement GetConcreteObject()
        {
            return new ReportControl();
        }
    }
}
