using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using BISC.Infrastructure.Global.IoC;
using Prism.Unity;

namespace BISC.Infrastructure.CompositionRoot.Ioc
{
   public class InjectionContainer: IInjectionContainer
    {
        private readonly IUnityContainer _container;

        public InjectionContainer(IUnityContainer container)
        {
            _container = container;
        }
        
        public T ResolveType<T>()
        {
            return _container.Resolve<T>();
        }

        public T ResolveType<T>(string key)
        {
            return _container.Resolve<T>(key);
        }

        public object ResolveType(Type t,string key)
        {
            return _container.Resolve(t,key);
        }

        public object ResolveType(Type t)
        {
            return _container.Resolve(t);
        }

        public void RegisterType(Type t)
        {
            _container.RegisterType(t);
        }

        public void RegisterType<TFrom, TTo>(bool isSingleton=false) where TTo:TFrom
        {
            if (isSingleton)
            {
                _container.RegisterType<TFrom, TTo>(new ContainerControlledLifetimeManager());
            }
            else
            {
                _container.RegisterType<TFrom, TTo>();
            }
        }

        public void RegisterType<T>(bool isSingleton = false)
        {
            if (isSingleton)
            {
                _container.RegisterType<T>(new ContainerControlledLifetimeManager());
            }
            else
            {
                _container.RegisterType<T>();
            }
        }

        public void RegisterType<TFrom, TTo>(string key, bool isSingleton=false) where TTo : TFrom
        {
            if (isSingleton)
            {
                _container.RegisterType<TFrom, TTo>(key,new ContainerControlledLifetimeManager());
            }
            else
            {
                _container.RegisterType<TFrom, TTo>(key);
            }
        }

    }
}
