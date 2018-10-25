using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Reports.Infrastructure.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Reports.Infrastructure.Services
{
    public interface IReportControlSavingService
    {
        Task SaveReportControls(List<IReportControlViewModel> ReportsToSave, IModelElement device, bool isSavingInDevice);
    }
}
