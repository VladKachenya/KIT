using BISC.Modules.Reports.Infrastructure.Keys;
using BISC.Modules.Reports.Infrastructure.Presentation.Services;
using BISC.Modules.Reports.Infrastructure.Presentation.ViewModels;
using BISC.Modules.Reports.Presentation.ViewModels.Helpers;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Navigation;
using System.Collections.ObjectModel;

namespace BISC.Modules.Reports.Presentation.ViewModels
{
    public class ReportsConflictsViewModel : NavigationViewModelBase
    {
        private readonly IReportVeiwModelService _controlViewModelService;
        private ReportsConflictsContext _reportsConflictsContext;


        public ReportsConflictsViewModel(IReportVeiwModelService controlViewModelService)
        {
            _controlViewModelService = controlViewModelService;
            ReportControlViewModelsInProject = new ObservableCollection<IReportControlViewModel>();
            ReportControlViewModelsInDevice = new ObservableCollection<IReportControlViewModel>();
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

            var contextReportInDevice =
                _controlViewModelService.SortReportViewModels(_reportsConflictsContext
                    .ReportControlViewModelsInDevice);
            var contextReportInProject =
                _controlViewModelService.SortReportViewModels(_reportsConflictsContext
                    .ReportControlViewModelsInProject);


            foreach (var reportControlViewModel in contextReportInDevice)
            {
                reportControlViewModel.IsEditable = false;
                reportControlViewModel.IsChenged = reportControlViewModel.ChangeTracker.GetIsModifiedRecursive();
                ReportControlViewModelsInDevice.Add(reportControlViewModel);
            }
            foreach (var reportControlViewModel in contextReportInProject)
            {
                reportControlViewModel.IsEditable = false;
                reportControlViewModel.IsChenged = reportControlViewModel.ChangeTracker.GetIsModifiedRecursive();
                ReportControlViewModelsInProject.Add(reportControlViewModel);
            }
        }

        #endregion
    }
}
