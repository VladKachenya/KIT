using System.Collections.Generic;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.Reports.Infrastructure.Services
{
    public interface IReportConfRevisionService
    {
        void IncrementConfRevisionReportControl(IDevice device, List<string> dataSetsNames);
    }
}