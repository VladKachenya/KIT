using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Reports.Infrastructure.Presentation.ViewModels;
using BISC.Modules.Reports.Presentation.ViewModels.ReportElementsViewModels;

namespace BISC.Modules.Reports.Presentation.ViewModels.Helpers
{
    public class ReportsConflictsContext
    {
        public ReportsConflictsContext(ObservableCollection<IReportControlViewModel> reportControlViewModelsInDevice, ObservableCollection<IReportControlViewModel> reportControlViewModelsInProject)
        {
            ReportControlViewModelsInDevice = reportControlViewModelsInDevice;
            ReportControlViewModelsInProject = reportControlViewModelsInProject;
        }

        public ObservableCollection<IReportControlViewModel> ReportControlViewModelsInDevice { get; }
        public ObservableCollection<IReportControlViewModel> ReportControlViewModelsInProject { get; }

    }
}
