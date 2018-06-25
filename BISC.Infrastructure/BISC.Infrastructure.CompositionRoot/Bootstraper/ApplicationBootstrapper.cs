using BISC.Infrastructure.CompositionRoot.Ioc;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Modules.Device.Presentation.Module;
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
            Container.RegisterType(typeof(IAppModule), typeof(GlobalPresentationModule),nameof(GlobalPresentationModule));
            Container.RegisterType(typeof(IAppModule), typeof(DevicePresentationModule), nameof(DevicePresentationModule));

        }
    }
}