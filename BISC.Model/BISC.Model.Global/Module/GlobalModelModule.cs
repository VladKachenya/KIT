using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Global.Factorys;
using BISC.Model.Global.Model;
using BISC.Model.Global.Project;
using BISC.Model.Global.Serializators;
using BISC.Model.Global.Serializators.Communication;
using BISC.Model.Global.Services;
using BISC.Model.Global.Services.CommunicationModel;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Factorys;
using BISC.Model.Infrastructure.Keys;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Serializing;
using BISC.Model.Infrastructure.Services;
using BISC.Model.Infrastructure.Services.Communication;

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
            _injectionContainer.RegisterType<IBiscProject, BiscProject>(true);
            _injectionContainer.RegisterType<ISclModel, SclModel>();
            _injectionContainer.RegisterType<IMismatchFactory, MismatchFactory>(true);
            _injectionContainer.RegisterType<IModelsComparingService, ModelsComparingService>(true);
           
            _injectionContainer.RegisterType<IProjectService, ProjectService>(true);
            RegisterSerializers(_injectionContainer.ResolveType<IModelElementsRegistryService>());
                      _injectionContainer.RegisterType<ISclCommunicationModelService, SclCommunicationModelService>();

        }

        private void RegisterSerializers(IModelElementsRegistryService modelElementsRegistryService)
        {
            _injectionContainer.RegisterType<BiscProjectSerializer>();
            _injectionContainer.RegisterType<SclModelElementSerializer>();
            _injectionContainer.RegisterType<ConnectedAccessPointSerializer>();
            _injectionContainer.RegisterType<AddressPropertySerializer>();
            _injectionContainer.RegisterType<DurationInMillisecondsSerializer>();
            _injectionContainer.RegisterType<GseSerializer>();
            _injectionContainer.RegisterType<SclAddressSerializer>();
            _injectionContainer.RegisterType<SubNetworkSerializer>();
            _injectionContainer.RegisterType<CommunicationSerializer>();






            modelElementsRegistryService.RegisterModelElement(_injectionContainer.ResolveType<SclModelElementSerializer>(),
                    ModelKeys.SclModelKey);
            modelElementsRegistryService.RegisterModelElement(_injectionContainer.ResolveType<BiscProjectSerializer>(),
                    ModelKeys.BiscProjectKey);

            modelElementsRegistryService.RegisterModelElement(_injectionContainer.ResolveType<ConnectedAccessPointSerializer>(),
                ModelKeys.ConnectedAccessPointKey);
            modelElementsRegistryService.RegisterModelElement(_injectionContainer.ResolveType<AddressPropertySerializer>(),
                ModelKeys.AddressPropertyKey);

            modelElementsRegistryService.RegisterModelElement(_injectionContainer.ResolveType<DurationInMillisecondsSerializer>(),
                ModelKeys.DurationInMillisecMaxTimeKey);
            modelElementsRegistryService.RegisterModelElement(_injectionContainer.ResolveType<DurationInMillisecondsSerializer>(),
                ModelKeys.DurationInMillisecMinTimeKey);
            modelElementsRegistryService.RegisterModelElement(_injectionContainer.ResolveType<GseSerializer>(),
                ModelKeys.GseKey);

            modelElementsRegistryService.RegisterModelElement(_injectionContainer.ResolveType<SclAddressSerializer>(),
                ModelKeys.SclAddressKey);
            modelElementsRegistryService.RegisterModelElement(_injectionContainer.ResolveType<SubNetworkSerializer>(),
                ModelKeys.SubNetworkKey);

            modelElementsRegistryService.RegisterModelElement(_injectionContainer.ResolveType<CommunicationSerializer>(),
                ModelKeys.CommunicationModelKey);

        }

        #endregion
    }
}
