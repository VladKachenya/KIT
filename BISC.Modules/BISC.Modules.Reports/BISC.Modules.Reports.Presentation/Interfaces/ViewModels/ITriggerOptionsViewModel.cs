using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Modules.Reports.Presentation.Interfaces.ViewModels.Base;

namespace BISC.Modules.Reports.Presentation.Interfaces.ViewModels
{
    public interface ITriggerOptionsViewModel : IReportElementBase<ITrgOps>
    {
        bool DataChange { get; set; }
        bool QualityChange { get; set; }
        bool DataUpdate { get; set; }
        bool Integrity { get; set; }
        bool GenetralInterrogation { get; set; }
    }
}
