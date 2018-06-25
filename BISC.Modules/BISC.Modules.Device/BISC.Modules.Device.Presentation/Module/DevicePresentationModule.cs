using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Device.Presentation.Services;
using BISC.Presentation.Infrastructure.Commands;
using BISC.Presentation.Infrastructure.Factories;

namespace BISC.Modules.Device.Presentation.Module
{
    public class DevicePresentationModule : IAppModule
    {

        private readonly IInjectionContainer _injectionContainer;

        public DevicePresentationModule(IInjectionContainer injectionContainer)
        {

            _injectionContainer = injectionContainer;
        }

        public void Initialize()
        {
            _injectionContainer.RegisterType<IDeviceAddingService, DeviceAddingService>(true);
            var presentationInitialization = _injectionContainer.ResolveType(typeof(DevicePresentationInitialization)) as DevicePresentationInitialization;

        }


    }
}