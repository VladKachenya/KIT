using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Model.Infrastructure;
using BISC.Modules.Device.Infrastructure.Loading;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Model.Serializers;
using BISC.Modules.Gooses.Model.Services;

namespace BISC.Modules.Gooses.Model.Module
{
   public class GoosesModelModule:IAppModule
    {
        private readonly IInjectionContainer _injectionContainer;

        public GoosesModelModule(IInjectionContainer injectionContainer)
        {
            _injectionContainer = injectionContainer;
        }


        #region Implementation of IAppModule

        public void Initialize()
        {
            var modelElementsRegistryService = _injectionContainer.ResolveType<IModelElementsRegistryService>();
            modelElementsRegistryService.RegisterModelElement(new ExternalGooseRefSerializer(), GooseKeys.GooseModelKeys.ExternalGooseRefKey);
            modelElementsRegistryService.RegisterModelElement(new GooseInputSerializer(),  GooseKeys.GooseModelKeys.GooseInputKey);
            modelElementsRegistryService.RegisterModelElement(new GooseControlSerializer(), GooseKeys.GooseModelKeys.GooseControlKey);
            _injectionContainer.RegisterType<IDeviceElementLoadingService, GoosesLoadingService>(Guid.NewGuid().ToString());
            _injectionContainer.RegisterType<IGoosesModelService, GoosesModelService>();

        }

        #endregion
    }
}
