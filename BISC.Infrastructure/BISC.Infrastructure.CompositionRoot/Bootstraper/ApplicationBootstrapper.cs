using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Modularity;
using BISC.Modules.Gooses.Module;
using BISC.Presentation.Module;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism;
using Prism.Modularity;
using Prism.Unity;

namespace BISC.Infrastructure.CompositionRoot.Bootstraper
{
    public class ApplicationBootstrapper : UnityBootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
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
            Container.RegisterType<IAppModule, GoosesModule>();
            Container.RegisterType<IAppModule, GlobalPresentationModule>();

        }
    }
}