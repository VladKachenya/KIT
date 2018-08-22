using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace BISC.Infrastructure.Global.IoC
{
    public interface IInjectionContainer
    {
        T ResolveType<T>();
        T ResolveType<T>(string key);
        List<object> ResolveAll(Type type);
        object ResolveType(Type t, string key);
        object ResolveType(Type t);
        void RegisterType(Type t);
        void RegisterType<TFrom, TTo>(bool isSingleton=false) where TTo : TFrom;
        void RegisterType<T>(bool isSingleton = false);

        void RegisterType<TFrom, TTo>(string key, bool isSingleton=false) where TTo : TFrom;

        
    }
}