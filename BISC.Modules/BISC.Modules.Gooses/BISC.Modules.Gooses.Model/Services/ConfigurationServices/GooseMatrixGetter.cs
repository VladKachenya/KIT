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
    public class GooseMatrixGetter : IConfigurableModelElementsGetter
    {
        private readonly IGooseMatrixFtpService _gooseMatrixFtpService;

        public GooseMatrixGetter(
            IGooseMatrixFtpService gooseMatrixFtpService)
        {
            _gooseMatrixFtpService = gooseMatrixFtpService;
        }
        public string ModuleName => InfrastructureKeys.ModulesKeys.GooseMatrixSubModule;
        public IEnumerable<IModelElement> GetConfigurableModelElements(IDevice device, ISclModel sclModel)
        {
            return new[]
            {
                _gooseMatrixFtpService.GetGooseMatrixFtpForDevice(device, sclModel.GetFirstParentOfType<IBiscProject>())
            };
        }
    }
}