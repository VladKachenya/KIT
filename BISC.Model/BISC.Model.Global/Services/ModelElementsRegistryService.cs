using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Modularity;
using BISC.Model.Global.Default;

namespace BISC.Model.Global.Services
{
    public class ModelElementsRegistryService : IModelElementsRegistryService
    {
        private Dictionary<string, IModelElementSerializator> _modelElementSerializatorDictionary = new Dictionary<string, IModelElementSerializator>();

        public void RegisterModelElement(IModelElementSerializator modelElementSerializator, string elementName)
        {
            if (_modelElementSerializatorDictionary.ContainsKey(elementName))
            {
                throw new ArgumentException($"Serializator with key {elementName} already exists");
            }
            _modelElementSerializatorDictionary.Add(elementName, modelElementSerializator);
        }

        public bool GetIsModelElementRegistered(string elementName)
        {
            return _modelElementSerializatorDictionary.ContainsKey(elementName);
        }

        public IModelElementSerializator GetModelElementSerializatorByKey(string elementName, bool isDefaultSerializatorAllowed = true)
        {
            if (!_modelElementSerializatorDictionary.ContainsKey(elementName))
            {
                if (isDefaultSerializatorAllowed)
                {
                    return new DefaultModelElementSerializator(this);
                }
                else
                {
                    throw new ArgumentException($"Serializator with key {elementName} is not added");
                }
            }
            return _modelElementSerializatorDictionary[elementName];
        }

     

     
    }
}
