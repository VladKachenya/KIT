using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Bootstrapper;
using BISC.Infrastructure.CompositionRoot.Bootstraper;
using BISC.Infrastructure.CompositionRoot.Ioc;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Model.Global.Module;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prism.Unity;

namespace BISC.Tests.Model.InitializeModules
{
   
    public class TestBaseClass
    {
     
        protected virtual List<Type> GetTestingModules()
        {
           return new List<Type>(){ typeof(GlobalModelModule) };
        }
        public TestBaseClass()
        {
           
            TestBootstrapper testBootstrapper=new TestBootstrapper(GetTestingModules().ToArray());
            testBootstrapper.Run();
            StaticContainer.SetContainer(new InjectionContainer(ServiceLocator.Current.GetInstance<IUnityContainer>()));
        }
    }
}
