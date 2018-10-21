using BISC.Model.Infrastructure.Elements;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture.Services;
using BISC.Modules.Reports.Infrastructure.Model;
using BISC.Modules.Reports.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Reports.Model.Services
{
    public class ReportsModelService : IReportsModelService
    {
        private readonly IInfoModelService _infoModelService;

        public ReportsModelService(IInfoModelService infoModelService)
        {
            _infoModelService = infoModelService;
        }

        public List<IReportControl> GetAllReportControlsOfDevice(IModelElement device)
        {
            List<IReportControl> ReportControls = new List<IReportControl>();
            var ldevices = _infoModelService.GetLDevicesFromDevices(device);
            foreach (var lDevice in ldevices)
            {
                foreach (var logicalNode in lDevice.LogicalNodes)
                {
                    logicalNode.ChildModelElements.ForEach((element =>
                    {
                        if (element is IReportControl reportControl)
                        {
                            ReportControls.Add(reportControl);
                        }
                    }));
                }
                foreach (var element in lDevice.LogicalNodeZero.Value.ChildModelElements)
                {
                    if (element is IDataSet)
                    {
                        
                    }
                    if (element is IReportControl reportControl)
                    {
                        ReportControls.Add(reportControl);
                    }
                }

            }
            return ReportControls;
        }
    }
}
