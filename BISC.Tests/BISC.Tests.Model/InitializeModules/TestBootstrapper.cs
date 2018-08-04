using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.CompositionRoot.Bootstraper;
using BISC.Infrastructure.CompositionRoot.Ioc;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Model.Global.Module;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Unity;

namespace BISC.Tests.Model.InitializeModules
{
  public  class TestBootstrapper:UnityBootstrapper
    {
        private readonly Type[] _modulesToTest;

        public TestBootstrapper(params Type[] modulesToTest)
        {
            _modulesToTest = modulesToTest;
        }


        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.RegisterType<IInjectionContainer, InjectionContainer>(new ContainerControlledLifetimeManager());
            StaticContainer.SetContainer(Container.Resolve<IInjectionContainer>());
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
            foreach (var moduleToTest in _modulesToTest)
            {
                Container.RegisterType(typeof(IAppModule), moduleToTest, Guid.NewGuid().ToString());
            }
        }
    }
}
