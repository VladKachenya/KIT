using System;
using System.Collections.Generic;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Keys;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Reports.Infrastructure.Services;

namespace BISC.Modules.Reports.Model.Services
{
    public class ReportControlsGetter : IConfigurableModelElementsGetter
    {
        private readonly IReportsModelService _reportsModelService;

        public ReportControlsGetter(IReportsModelService reportsModelService)
        {
            _reportsModelService = reportsModelService;
        }
        public string ModuleName => InfrastructureKeys.ModulesKeys.ReportModule;
        public IEnumerable<IModelElement> GetConfigurableModelElements(IDevice device, ISclModel sclModel)
        {
            return _reportsModelService.GetDynamicReports(device.DeviceGuid, sclModel);
        }
    }
}