using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Reports.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.Reports.Infrastructure.Services
{
    public interface IReportsModelService
    {
        List<IReportControl> GetAllReportControlsOfDevice(IModelElement device);

        void DeleteAllReportsOfDevice(IDevice device);
        void AddReportsToDevice(IDevice device, List<IReportControl> reportControls);
    }
}
