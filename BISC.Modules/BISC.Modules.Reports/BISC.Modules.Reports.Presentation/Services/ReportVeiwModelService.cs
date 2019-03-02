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
            var coll = reportControlViewModels.OrderBy(el=> el.IsDynamic).ThenBy(el=> !el.IsBuffered).
                ThenBy(el => el.Name.Trim(new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', })).
                ThenBy(el => TrimNamber(el.Name));
            return new ObservableCollection<IReportControlViewModel>(coll);

            //List <IReportControlViewModel> notDynamicUnBuffered = null;
            //List<IReportControlViewModel> dynamicUnBuffered = null;
            //List<IReportControlViewModel> notDynamicBuffered = null;
            //List<IReportControlViewModel> dynamicBuffered = null;

            //ObservableCollection<IReportControlViewModel> resultCollection = new ObservableCollection<IReportControlViewModel>();

            //foreach (var reportControlViewModel in reportControlViewModels)
            //{
            //    if (!reportControlViewModel.IsBuffered)
            //    {
            //        if (!reportControlViewModel.IsDynamic)
            //        {
            //            if (notDynamicUnBuffered == null)
            //            {
            //                notDynamicUnBuffered = new List<IReportControlViewModel>();
            //            }
            //            notDynamicUnBuffered.Add(reportControlViewModel);
            //        }
            //        else
            //        {
            //            if (dynamicUnBuffered == null)
            //            {
            //                dynamicUnBuffered = new List<IReportControlViewModel>();
            //            }
            //            dynamicUnBuffered.Add(reportControlViewModel);
            //        }
            //    }
            //    else
            //    {
            //        if (!reportControlViewModel.IsDynamic)
            //        {
            //            if (notDynamicBuffered == null)
            //            {
            //                notDynamicBuffered = new List<IReportControlViewModel>();
            //            }
            //            notDynamicBuffered.Add(reportControlViewModel);
            //        }
            //        else
            //        {
            //            if (dynamicBuffered == null)
            //            {
            //                dynamicBuffered = new List<IReportControlViewModel>();
            //            }
            //            dynamicBuffered.Add(reportControlViewModel);
            //        }
            //    }
            //}

            //var dU = dynamicUnBuffered?.OrderBy(el => TrimNam(el.Name)).ThenBy(el => Trim2(el.Name)).ToList();
            //var dB = dynamicBuffered?.OrderBy(el => el.Name).ToList();

            //notDynamicUnBuffered?.ForEach(element => resultCollection.Add(element));
            //dU?.ForEach(element => resultCollection.Add(element));
            //notDynamicBuffered?.ForEach(element => resultCollection.Add(element));
            //dB?.ForEach(element => resultCollection.Add(element));

            //return resultCollection;
        }


        private int TrimNamber(string str)
        {
            var clearStr = str.Trim(new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', });
            var str1 = str.Trim();
            var intStr = str1.Substring(clearStr.Length);
            if (string.IsNullOrEmpty(intStr))
            {
                return 0;
            }
            int.TryParse(intStr, out var res);
            return res;
        }
    }
}