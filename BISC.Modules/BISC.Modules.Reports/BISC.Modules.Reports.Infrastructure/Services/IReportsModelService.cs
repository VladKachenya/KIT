using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Reports.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Reports.Infrastructure.Services
{
    public interface IReportsModelService
    {
        List<IReportControl> GetAllReportControlsOfDevice(IModelElement device);

    }
}
