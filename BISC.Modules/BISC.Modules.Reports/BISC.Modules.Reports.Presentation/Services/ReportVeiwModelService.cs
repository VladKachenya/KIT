using BISC.Modules.Reports.Infrastructure.Presentation.Services;
using BISC.Modules.Reports.Infrastructure.Presentation.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace BISC.Modules.Reports.Presentation.Services
{
    public class ReportVeiwModelService : IReportVeiwModelService
    {
        
        public ObservableCollection<IReportControlViewModel> SortReportViewModels(IEnumerable<IReportControlViewModel> reportControlViewModels)
        {
            List<IReportControlViewModel> notDynamicUnBuffered = null;
            List<IReportControlViewModel> dynamicUnBuffered = null;
            List<IReportControlViewModel> notDynamicBuffered = null;
            List<IReportControlViewModel> dynamicBuffered = null;

            ObservableCollection<IReportControlViewModel> resultCollection = new ObservableCollection<IReportControlViewModel>();

            foreach (var reportControlViewModel in reportControlViewModels)
            {
                if (!reportControlViewModel.IsBuffered)
                {
                    if (!reportControlViewModel.IsDynamic)
                    {
                        if (notDynamicUnBuffered == null)
                        {
                            notDynamicUnBuffered = new List<IReportControlViewModel>();
                        }
                        notDynamicUnBuffered.Add(reportControlViewModel);
                    }
                    else
                    {
                        if (dynamicUnBuffered == null)
                        {
                            dynamicUnBuffered = new List<IReportControlViewModel>();
                        }
                        dynamicUnBuffered.Add(reportControlViewModel);
                    }
                }
                else
                {
                    if (!reportControlViewModel.IsDynamic)
                    {
                        if (notDynamicBuffered == null)
                        {
                            notDynamicBuffered = new List<IReportControlViewModel>();
                        }
                        notDynamicBuffered.Add(reportControlViewModel);
                    }
                    else
                    {
                        if (dynamicBuffered == null)
                        {
                            dynamicBuffered = new List<IReportControlViewModel>();
                        }
                        dynamicBuffered.Add(reportControlViewModel);
                    }
                }
            }

            var dU = dynamicUnBuffered?.OrderBy(el => el.Name).ToList();
            var dB = dynamicBuffered?.OrderBy(el => el.Name).ToList();

            notDynamicUnBuffered?.ForEach(element => resultCollection.Add(element));
            dU?.ForEach(element => resultCollection.Add(element));
            notDynamicBuffered?.ForEach(element => resultCollection.Add(element));
            dB?.ForEach(element => resultCollection.Add(element));
            return resultCollection;
        }
    }
}