using System.Collections.Generic;
using System.Collections.ObjectModel;
using BISC.Modules.Reports.Infrastructure.Presentation.ViewModels;

namespace BISC.Modules.Reports.Infrastructure.Presentation.Services
{
    public interface IReportVeiwModelService
    {
        ObservableCollection<IReportControlViewModel> SortReportViewModels(IEnumerable<IReportControlViewModel> reportControlViewModels);
    }
}