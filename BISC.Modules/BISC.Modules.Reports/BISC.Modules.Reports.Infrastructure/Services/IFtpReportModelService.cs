using System.Collections.Generic;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Reports.Infrastructure.Model;

namespace BISC.Modules.Reports.Infrastructure.Services
{
    public interface IFtpReportModelService
    {
        Task<List<IReportControl>> GetReportsFromDevice(string ip);
        Task<OperationResult> WriteReportsToDevice(IDevice device, IEnumerable<IReportControl> reportControlsToSave);
    }
}