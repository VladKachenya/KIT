using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Modules.Reports.Presentation.Interfaces.ViewModels.Base;

namespace BISC.Modules.Reports.Presentation.Interfaces.ViewModels
{
    public interface IOprionalFildsViewModel: IReportElementBase<IOptFields>
    {
        bool SequenceNumber { get; set; }
        bool ReportTimeStamp { get; set; }
        bool ReasonForInclusion { get; set; }
        bool DataSetName { get; set; }
        bool DataReference { get; set; }
        bool BufferOverflow { get; set; }
        bool EntruID { get; set; }
        bool ConfigRevision { get; set; }
        bool Segmentation { get; set; }


    }
}
