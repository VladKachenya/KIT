using BISC.Infrastructure.Global.IoC;
using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Modules.Reports.Infrastructure.Presentation.Factorys;
using BISC.Modules.Reports.Infrastructure.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Reports.Presentation.Factorys
{
    public class ReportControlFactoryViewModel : IReportControlFactoryViewModel
    {
        private readonly IInjectionContainer _injectionContainer;

        public ReportControlFactoryViewModel(IInjectionContainer injectionContainer)
        {
            _injectionContainer = injectionContainer;
        }
        public ObservableCollection<IReportControlViewModel> GetReportControlsViewModel(List<IReportControl> modelsList)
        {
            ObservableCollection<IReportControlViewModel> reportControlsColection = new ObservableCollection<IReportControlViewModel>();
            foreach (var element in modelsList)
                reportControlsColection.Add(GetReportControlViewModel(element));
            return reportControlsColection;
        }

        public IReportControlViewModel GetReportControlViewModel(IReportControl model)
        {
            IReportControlViewModel obj = _injectionContainer.ResolveType<IReportControlViewModel>();
            obj.Model = model;
            return obj;
        }
    }
}
