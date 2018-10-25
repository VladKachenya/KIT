using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Reports.Infrastructure.Presentation.Services;
using BISC.Modules.Reports.Infrastructure.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Reports.Presentation.Services
{
    public class ReportsSavingService : IReportsSavingService
    {
        public Task SaveReports(List<IReportControlViewModel> dataSetsToSave, IModelElement device, bool isSavingInDevice)
        {
            throw new NotImplementedException();
        }
    }
}
