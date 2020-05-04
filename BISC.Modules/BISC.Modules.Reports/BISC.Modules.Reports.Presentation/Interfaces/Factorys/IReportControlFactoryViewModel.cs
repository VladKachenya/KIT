using System.Collections.Generic;
using System.Collections.ObjectModel;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Modules.Reports.Presentation.Interfaces.ViewModels;

namespace BISC.Modules.Reports.Presentation.Interfaces.Factorys
{
    public interface IReportControlFactoryViewModel
    {
        IReportControlViewModel GetReportControlViewModel(IReportControl model, IDevice device);
        IReportControlViewModel CreateReportViewModel(List<string> existingNames, IDevice device);

        ObservableCollection<IReportControlViewModel> GetReportControlsViewModel ( List<IReportControl> modelsList, IDevice device);
    }
}
