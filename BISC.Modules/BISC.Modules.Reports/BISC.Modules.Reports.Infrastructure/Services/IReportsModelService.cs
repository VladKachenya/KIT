using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Reports.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture.Elements;

namespace BISC.Modules.Reports.Infrastructure.Services
{
    public interface IReportsModelService
    {
        List<IReportControl> GetAllReportControlsOfDevice(IModelElement device);
        IEnumerable<IReportControl> GetDynamicReports(Guid deviceGuid, ISclModel sclModel);

        void DeleteAllReportsOfDevice(IDevice device);
        void AddReportsToDevice(IDevice device, List<IReportControl> reportControls);
        void AddReportsToDevice(IDevice device, List<IReportControl> reportControls, string lDevice);
        void DeleteReportsFromDevice(IDevice device, List<IReportControl> reportControls);

    }
}
