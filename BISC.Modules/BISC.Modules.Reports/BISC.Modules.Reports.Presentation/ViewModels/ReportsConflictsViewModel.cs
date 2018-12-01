using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Reports.Infrastructure.Keys;
using BISC.Modules.Reports.Infrastructure.Presentation.ViewModels;
using BISC.Modules.Reports.Presentation.ViewModels.Helpers;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Navigation;

namespace BISC.Modules.Reports.Presentation.ViewModels
{
   public class ReportsConflictsViewModel:NavigationViewModelBase
    {
        private ReportsConflictsContext _reportsConflictsContext;

        public ReportsConflictsViewModel()
        {
            ReportControlViewModelsInProject=new ObservableCollection<IReportControlViewModel>();
            ReportControlViewModelsInDevice=new ObservableCollection<IReportControlViewModel>();
        }

        public ObservableCollection<IReportControlViewModel> ReportControlViewModelsInDevice { get; }
        public ObservableCollection<IReportControlViewModel> ReportControlViewModelsInProject { get; }

        #region Overrides of NavigationViewModelBase

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            _reportsConflictsContext =
                navigationContext.BiscNavigationParameters.GetParameterByName<ReportsConflictsContext>(ReportsKeys
                    .ReportsPresentationKeys.ReportsConflictsContext);
            ReportControlViewModelsInDevice.Clear();
            ReportControlViewModelsInProject.Clear();

            foreach (var reportControlViewModel in _reportsConflictsContext.ReportControlViewModelsInDevice)
            {
                reportControlViewModel.IsEditable = false;
                reportControlViewModel.IsChenged = reportControlViewModel.ChangeTracker.GetIsModifiedRecursive();
                ReportControlViewModelsInDevice.Add(reportControlViewModel);
            }
            foreach (var reportControlViewModel in _reportsConflictsContext.ReportControlViewModelsInProject)
            {
                reportControlViewModel.IsEditable = false;
                reportControlViewModel.IsChenged = reportControlViewModel.ChangeTracker.GetIsModifiedRecursive();
                ReportControlViewModelsInProject.Add(reportControlViewModel);
            }
        }

        #endregion
    }
}
