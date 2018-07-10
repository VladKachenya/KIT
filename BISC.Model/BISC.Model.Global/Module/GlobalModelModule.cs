using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Global.Serializators;
using BISC.Model.Global.Services;
using BISC.Model.Infrastructure.Keys;
using BISC.Model.Infrastructure.Services;

namespace BISC.Model.Global.Module
{
    public class GlobalModelModule : IAppModule
    {
        private readonly IInjectionContainer _injectionContainer;

        public GlobalModelModule(IInjectionContainer injectionContainer)
        {
            _injectionContainer = injectionContainer;
        }


        #region Implementation of IAppModule

        public void Initialize()
        {
            _injectionContainer.RegisterType<IModelTypesResolvingService, ModelTypesResolvingService>(true);
            _injectionContainer.RegisterType<IModelElementsRegistryService, ModelElementsRegistryService>(true);
            _injectionContainer.RegisterType<IModelComposingService, ModelComposingService>(true);
            _injectionContainer.RegisterType<SclModelElementSerializer>();
            _injectionContainer.ResolveType<IModelElementsRegistryService>()
                .RegisterModelElement(_injectionContainer.ResolveType<SclModelElementSerializer>(),
                    ModelKeys.SclModelKey);
        }

        #endregion
    }
}
