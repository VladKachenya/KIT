using System.Collections.Generic;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.Reports.Infrastructure.Model;

namespace BISC.Modules.Reports.Infrastructure.Services
{
    public interface IFtpReportModelService
    {
        Task<List<IReportControl>> GetReportsFromDevice(string ip);
        Task<OperationResult> WriteReportsToDevice(string ip, IEnumerable<IReportControl> reportControlsToSave);
    }
}