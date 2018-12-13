using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Reports.Infrastructure.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Common;

namespace BISC.Modules.Reports.Infrastructure.Presentation.Services
{
    public interface IReportsSavingService
    {
        Task<SavingResultEnum> SaveReportsAsync(List<IReportControlViewModel> reportsToSave, IDevice device, bool isSavingInDevice);

	    Task<bool> IsFtpSavingNeeded(List<IReportControlViewModel> reportsToSave, IDevice device,
		    bool isSavingInDevice);

    }
    
}
