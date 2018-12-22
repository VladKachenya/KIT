using BISC.Modules.Reports.Infrastructure.Presentation.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BISC.Modules.Reports.Presentation.ViewModels.Helpers
{
    public class ReportsConflictsContext
    {
        public ReportsConflictsContext(ObservableCollection<IReportControlViewModel> reportControlViewModelsInDevice, ObservableCollection<IReportControlViewModel> reportControlViewModelsInProject)
        {
            ReportControlViewModelsInDevice = reportControlViewModelsInDevice;
            ReportControlViewModelsInProject = reportControlViewModelsInProject;
            SortReportsByBuffered();
        }

        private void SortReportsByBuffered()
        {
            List<IReportControlViewModel> unBufferidReportsViewModel = new List<IReportControlViewModel>();
            List<IReportControlViewModel> bufferidReportsViewModel = new List<IReportControlViewModel>();
            foreach (var report in ReportControlViewModelsInProject)
            {
                if (report.IsBuffered)
                {
                    bufferidReportsViewModel.Add(report);
                }
                else
                {
                    unBufferidReportsViewModel.Add(report);
                }
            }
            ReportControlViewModelsInProject.Clear();
            unBufferidReportsViewModel.ForEach(element => ReportControlViewModelsInProject.Add(element));
            bufferidReportsViewModel.ForEach(element => ReportControlViewModelsInProject.Add(element));

        }

        public ObservableCollection<IReportControlViewModel> ReportControlViewModelsInDevice { get; }
        public ObservableCollection<IReportControlViewModel> ReportControlViewModelsInProject { get; }

    }
}
