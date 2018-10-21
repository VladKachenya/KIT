using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Reports.Infrastructure.Presentation.ViewModels
{
    public interface IReportControlViewModel
    {
        string Name { get; set; }
        string ReportID { get; set; }
        bool IsBuffered { get; set; }
        int BufferTime { get; set; }
        string DataSetName { get; set; }
        int IntegrutyPeriod { get; set; }

    }
}
