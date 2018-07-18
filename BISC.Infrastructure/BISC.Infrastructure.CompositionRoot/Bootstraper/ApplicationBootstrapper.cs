using BISC.Infrastructure.CompositionRoot.Ioc;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Model.Global.Module;
using BISC.Modules.Connection.Model.Module;
using BISC.Modules.Connection.Presentation.Module;
using BISC.Modules.Device.Model.Module;
using BISC.Modules.Device.Presentation.Module;
using BISC.Presentation.BaseItems.Module;
using BISC.Presentation.Module;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Unity;


namespace BISC.Infrastructure.CompositionRoot.Bootstraper
{
    public class ApplicationBootstrapper : UnityBootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.RegisterType<IInjectionContainer, InjectionContainer>(new ContainerControlledLifetimeManager());
            RegisterModules();
            ResolveModules();

        }

   
        private void ResolveModules()
        {
            var modules = Container.ResolveAll(typeof(IAppModule));
            modules.ForEach((o => (o as IAppModule)?.Initialize()));
        }



        private void RegisterModules()
        {
            
            Container.RegisterType(typeof(IAppModule), typeof(GlobalModelModule),nameof(GlobalModelModule));
            Container.RegisterType(typeof(IAppModule), typeof(PresentationBaseItemsModule),nameof(PresentationBaseItemsModule));
            Container.RegisterType(typeof(IAppModule), typeof(GlobalPresentationModule),nameof(GlobalPresentationModule));
            Container.RegisterType(typeof(IAppModule), typeof(DeviceModelModule), nameof(DeviceModelModule));
            Container.RegisterType(typeof(IAppModule), typeof(DevicePresentationModule), nameof(DevicePresentationModule));
            Container.RegisterType(typeof(IAppModule), typeof(ConnectionPresentationModule), nameof(ConnectionPresentationModule));
            Container.RegisterType(typeof(IAppModule), typeof(ConnectionModelModule), nameof(ConnectionModelModule));


        }
    }
}