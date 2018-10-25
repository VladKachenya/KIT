using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Modules.Reports.Infrastructure.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Reports.Infrastructure.Presentation.Factorys
{
    public interface IReportControlFactoryViewModel
    {
        IReportControlViewModel GetReportControlViewModel(IReportControl model, IDevice device);
        IReportControlViewModel GetReportControlViewModel(IDevice device);

        ObservableCollection<IReportControlViewModel> GetReportControlsViewModel ( List<IReportControl> modelsList, IDevice device);
    }
}
