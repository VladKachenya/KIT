using System;

namespace BISC.Model.Infrastructure.Services
{
    public interface IModelTypesResolvingService
    {
        void RegisterType(Type baseType, Type typeToRegister, string name,int edition);
        object ResolveTypeByName(Type baseType, string name, int edition);
    }
}