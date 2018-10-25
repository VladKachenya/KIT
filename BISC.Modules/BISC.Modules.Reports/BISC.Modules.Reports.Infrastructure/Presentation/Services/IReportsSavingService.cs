using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Reports.Infrastructure.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Reports.Infrastructure.Presentation.Services
{
    public interface IReportsSavingService
    {
        Task SaveReports(List<IReportControlViewModel> dataSetsToSave, IModelElement device, bool isSavingInDevice);

    }
}
