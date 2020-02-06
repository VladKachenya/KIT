using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Model.Infrastructure.Serializing;
using BISC.Modules.Device.Infrastructure.Loading;
using BISC.Modules.Gooses.Infrastructure.Factorys;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Modules.Gooses.Infrastructure.Services;
using BISC.Modules.Gooses.Model.Factorys;
using BISC.Modules.Gooses.Model.Helpers;
using BISC.Modules.Gooses.Model.Serializers;
using BISC.Modules.Gooses.Model.Serializers.FtpMatrix;
using BISC.Modules.Gooses.Model.Services;
using BISC.Modules.Gooses.Model.Services.LoadingServices;
using System;
using BISC.Modules.Gooses.Infrastructure.Helpers;

namespace BISC.Modules.Gooses.Model.Module
{
    public class GoosesModelModule : IAppModule
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
            modelElementsRegistryService.RegisterModelElement(new GooseInputSerializer(), GooseKeys.GooseModelKeys.GooseInputKey);
            modelElementsRegistryService.RegisterModelElement(new GooseControlSerializer(), GooseKeys.GooseModelKeys.GooseControlKey);
            modelElementsRegistryService.RegisterModelElement(new SubscriberDeviceSerializer(), GooseKeys.GooseModelKeys.SubscriberDeviceKey);
            modelElementsRegistryService.RegisterModelElement(new GooseRowSerializer(), GooseKeys.GooseModelKeys.GooseRowKey);
            modelElementsRegistryService.RegisterModelElement(new GooseMatrixFtpSerializer(), GooseKeys.GooseModelKeys.GooseMatrixFtpKey);
            modelElementsRegistryService.RegisterModelElement(new GooseRowFtpEntitySerializer(), GooseKeys.GooseModelKeys.GooseRowFtpEntityKey);
            modelElementsRegistryService.RegisterModelElement(new GoCbFtpEntitySerializer(), GooseKeys.GooseModelKeys.GoCbFtpEntityKey);
            modelElementsRegistryService.RegisterModelElement(new MacAddressEntitySerializer(), GooseKeys.GooseModelKeys.MacAddressEntityKey);
            modelElementsRegistryService.RegisterModelElement(new GooseQualityRowFtpEntitySerializer(), GooseKeys.GooseModelKeys.GooseRowQualityFtpEntityKey);
            modelElementsRegistryService.RegisterModelElement(new GooseInputModelInfoSerializer(), GooseKeys.GooseModelKeys.GooseInputModelInfoKey);
            modelElementsRegistryService.RegisterModelElement(new GooseDeviceInputSerializer(), GooseKeys.GooseModelKeys.GooseDeviceInputKey);

            _injectionContainer.RegisterType<IDeviceElementLoadingService, GoosesLoadingService>(Guid.NewGuid().ToString());
            _injectionContainer.RegisterType<IDeviceElementLoadingService, GooseMatrixLoadingService>(Guid.NewGuid().ToString());
            _injectionContainer.RegisterType<IDeviceElementLoadingService, GooseInputModelInfosLoadingService>(Guid.NewGuid().ToString());


            _injectionContainer.RegisterType<IGoosesModelService, GoosesModelService>();
            _injectionContainer.RegisterType<IFtpGooseModelService, FtpGooseModelService>();
            _injectionContainer.RegisterType<IGooseSavingService, GooseSavingService>();
            _injectionContainer.RegisterType<IGooseMatrixFtpService, GooseMatrixFtpService>();
            _injectionContainer.RegisterType<IGooseModelServicesFacade, GooseModelServicesFacade>();

            _injectionContainer.RegisterType<IGooseMatrixParsersFactory, GooseMatrixParsersFactory>();
            _injectionContainer.RegisterType<IGooseInputModelInfoFactory, GooseInputModelInfoFactory>();
            _injectionContainer.RegisterType<IGoCbFtpEntityFactory, GoCbFtpEntityFactory>();


        }

        #endregion
    }
}
