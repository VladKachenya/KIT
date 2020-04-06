using System;
using System.Collections.Generic;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Keys;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Reports.Infrastructure.Services;

namespace BISC.Modules.Reports.Model.Services
{
    public class ReportConfigurableModelElementsGetter : IConfigurableModelElementsGetter
    {
        private readonly IReportsModelService _reportsModelService;

        public ReportConfigurableModelElementsGetter(IReportsModelService reportsModelService)
        {
            _reportsModelService = reportsModelService;
        }
        public string ModuleName => InfrastructureKeys.ModulesKeys.ReportModule;
        public IEnumerable<IModelElement> GetConfigurableModelElements(Guid deviceGuid, ISclModel sclModel)
        {
            return _reportsModelService.GetDynamicReports(deviceGuid, sclModel);
        }
    }
}