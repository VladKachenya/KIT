using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Services;

namespace BISC.Model.Global.Services
{
    public class ModelTypesResolvingService : IModelTypesResolvingService
    {

        private Dictionary<int, Dictionary<Type, Dictionary<string, Type>>> _editionsDictionary;
        private Func<object> _getLnStubFunc;

        public ModelTypesResolvingService()
        {
            _editionsDictionary = new Dictionary<int, Dictionary<Type, Dictionary<string, Type>>>();
        }

        #region Implementation of IModelTypesResolvingService

        public void RegisterType(Type baseType, Type typeToRegister, string name, int edition)
        {
            if (!_editionsDictionary.ContainsKey(edition))
            {
                _editionsDictionary.Add(edition, new Dictionary<Type, Dictionary<string, Type>>());
            }

            if (!_editionsDictionary[edition].ContainsKey(baseType))
            {
                _editionsDictionary[edition].Add(baseType, new Dictionary<string, Type>());
            }

            if (_editionsDictionary[edition][baseType].ContainsKey(name))
            {
                if (_editionsDictionary[edition][baseType][name] != typeToRegister)
                {
                    throw new ArgumentException("Another type registered on this name");
                }
            }
            _editionsDictionary[edition][baseType].Add(name,typeToRegister);

        }

        public object ResolveTypeByName(Type baseType, string name, int edition)
        {
            if (!_editionsDictionary.ContainsKey(edition)) return null;
            if (!_editionsDictionary[edition].ContainsKey(baseType)) return null;
            if (_editionsDictionary[edition][baseType].ContainsKey(name))
            {
                return _editionsDictionary[edition][baseType][name];
            }

            return _getLnStubFunc();
        }

        public List<Type> GetAllRegisteredTypes(int ed, Type baseType)
        {
            return _editionsDictionary[ed][baseType].Values.ToList();
        }

        public void SetLnStub(Func<object> getLnStubFunc)
        {
            _getLnStubFunc = getLnStubFunc;
        }

        #endregion
    }
}
