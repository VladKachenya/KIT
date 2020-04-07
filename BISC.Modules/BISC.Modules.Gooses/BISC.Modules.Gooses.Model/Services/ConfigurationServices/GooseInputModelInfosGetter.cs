using System;
using System.Collections.Generic;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Keys;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Services;

namespace BISC.Modules.Gooses.Model.Services.ConfigurationServices
{
    public class GooseInputModelInfosGetter : IConfigurableModelElementsGetter
    {
        private readonly IGoosesModelService _goosesModelService;

        public GooseInputModelInfosGetter(
            IGoosesModelService goosesModelService)
        {
            _goosesModelService = goosesModelService;
        }
        public string ModuleName => InfrastructureKeys.ModulesKeys.GooseInputModelInfoSubModule;
        public IEnumerable<IModelElement> GetConfigurableModelElements(IDevice device, ISclModel sclModel)
        {
            return _goosesModelService.GetGooseInputModelInfos(device, sclModel.GetFirstParentOfType<IBiscProject>());
        }
    }
}