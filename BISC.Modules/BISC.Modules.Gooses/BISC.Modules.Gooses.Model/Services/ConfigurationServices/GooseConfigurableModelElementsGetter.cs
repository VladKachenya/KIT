using System;
using System.Collections.Generic;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Keys;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Services;

namespace BISC.Modules.Gooses.Model.Services.ConfigurationServices
{
    public class GooseConfigurableModelElementsGetter : IConfigurableModelElementsGetter
    {
        private readonly IGoosesModelService _goosesModelService;

        public string ModuleName => InfrastructureKeys.ModulesKeys.GooseModule;

        public GooseConfigurableModelElementsGetter(IGoosesModelService goosesModelService)
        {
            _goosesModelService = goosesModelService;
        }
        public IEnumerable<IModelElement> GetConfigurableModelElements(Guid deviceGuid, ISclModel sclModel)
        {
            return _goosesModelService.GetGooseControls(deviceGuid, sclModel);
        }
    }
}