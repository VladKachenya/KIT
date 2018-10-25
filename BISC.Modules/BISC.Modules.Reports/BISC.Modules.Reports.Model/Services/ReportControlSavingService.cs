using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Reports.Infrastructure.Presentation.ViewModels;
using BISC.Modules.Reports.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Reports.Model.Services
{
    public class ReportControlSavingService : IReportControlSavingService
    {
        public Task SaveReportControls(List<IReportControlViewModel> ReportsToSave, IModelElement device, bool isSavingInDevice)
        {
            throw new NotImplementedException();
        }
    }
}
