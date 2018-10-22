using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Modules.Reports.Infrastructure.Presentation.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BISC.Modules.Reports.Infrastructure.Presentation.ViewModels
{
    public interface IReportControlViewModel : IReportElementBase<IReportControl>
    {
        string ElementName { get; }
        Brush TypeColorBrush { get; }
        string Name { get; set; }
        //string PrefixName { get; set; }
        string ReportID { get; set; }
        bool IsBuffered { get; set; }
        int BufferTime { get; set; }
        string DataSetName { get; set; }
        int IntegrutyPeriod { get; set; }

        IReportEnabledViewModel ReportEnabledViewModel { get; }
        ITriggerOptionsViewModel TriggerOptionsViewModel { get; }
        IOprionalFildsViewModel OprionalFildsViewModel { get; }

    }
}
