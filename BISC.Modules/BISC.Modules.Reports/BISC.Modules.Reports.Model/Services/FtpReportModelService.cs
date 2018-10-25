using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;
using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Modules.Reports.Infrastructure.Services;

namespace BISC.Modules.Reports.Model.Services
{
   public class FtpReportModelService: IFtpReportModelService
    {
        #region Implementation of IFtpReportModelService

        public Task<List<IReportControl>> GetReportsFromDevice(string ip)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> WriteReportsToDevice(string ip, List<IReportControl> gooseDtos)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
