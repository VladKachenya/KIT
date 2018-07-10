using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Model.Infrastructure;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Device.Model.Serialization;
using BISC.Modules.Device.Model.Services;

namespace BISC.Modules.Device.Model.Module
{
   public class DeviceModelModule:IAppModule
    {
        private readonly IInjectionContainer _injectionContainer;

        public DeviceModelModule(IInjectionContainer injectionContainer)
        {
            _injectionContainer = injectionContainer;
        }

        #region Implementation of IAppModule

        public void Initialize()
        {
            _injectionContainer.RegisterType<DeviceSerializer>();
            _injectionContainer.RegisterType<IDevice,Model.Device>();
            _injectionContainer.RegisterType<IDeviceModelService, DeviceModelService>(true);

            _injectionContainer.ResolveType<IModelElementsRegistryService>().RegisterModelElement(_injectionContainer.ResolveType<DeviceSerializer>(), DeviceKeys.DeviceModelKey);
        }

        #endregion
    }
}
