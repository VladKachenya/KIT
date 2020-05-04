using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Modules.Reports.Presentation.Interfaces.ViewModels.Base;

namespace BISC.Modules.Reports.Presentation.Interfaces.ViewModels
{
    public interface IReportEnabledViewModel : IReportElementBase<IRptEnabled>
    {
        int Max { get; set; }
    }
}
