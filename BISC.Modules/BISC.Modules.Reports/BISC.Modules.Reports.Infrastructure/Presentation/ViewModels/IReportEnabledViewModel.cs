using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Modules.Reports.Infrastructure.Presentation.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Reports.Infrastructure.Presentation.ViewModels
{
    public interface IReportEnabledViewModel : IReportElementBase<IRptEnabled>
    {
        int Max { get; set; }
    }
}
