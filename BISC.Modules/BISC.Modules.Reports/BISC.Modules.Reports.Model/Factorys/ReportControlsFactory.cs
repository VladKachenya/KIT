using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Reports.Infrastructure.Factorys;
using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Modules.Reports.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Reports.Model.Factorys
{
    public class ReportControlsFactory : IReportControlsFactory
    {
        public IReportControl GetReportControl()
        {
            var report = new ReportControl();
            report.IsDynamic = true;
            report.OptFields.Value = new OptFields();
            report.RptEnabled.Value = new RptEnabled();
            report.TrgOps.Value = new TrgOps();
            return report;
        }
    }
}
