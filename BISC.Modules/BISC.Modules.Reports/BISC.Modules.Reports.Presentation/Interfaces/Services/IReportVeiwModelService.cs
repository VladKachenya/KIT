using System.Collections.Generic;
using System.Collections.ObjectModel;
using BISC.Modules.Reports.Presentation.Interfaces.ViewModels;

namespace BISC.Modules.Reports.Presentation.Interfaces.Services
{
    public interface IReportVeiwModelService
    {
        ObservableCollection<IReportControlViewModel> SortReportViewModels(IEnumerable<IReportControlViewModel> reportControlViewModels);
    }
}