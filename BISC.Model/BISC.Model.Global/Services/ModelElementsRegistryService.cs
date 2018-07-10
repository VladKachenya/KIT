using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Modularity;
using BISC.Model.Global.Serializators;

namespace BISC.Model.Global.Services
{
    public class ModelElementsRegistryService : IModelElementsRegistryService
    {
        private Dictionary<string, IModelElementSerializer> _modelElementSerializatorDictionary = new Dictionary<string, IModelElementSerializer>();

        public void RegisterModelElement(IModelElementSerializer modelElementSerializer, string elementName)
        {
            if (_modelElementSerializatorDictionary.ContainsKey(elementName))
            {
                throw new ArgumentException($"Serializator with key {elementName} already exists");
            }
            _modelElementSerializatorDictionary.Add(elementName, modelElementSerializer);
        }

        public bool GetIsModelElementRegistered(string elementName)
        {
            return _modelElementSerializatorDictionary.ContainsKey(elementName);
        }

        public IModelElementSerializer GetModelElementSerializatorByKey(string elementName, bool isDefaultSerializatorAllowed = true)
        {
            if (!_modelElementSerializatorDictionary.ContainsKey(elementName))
            {
                if (isDefaultSerializatorAllowed)
                {
                    return new DefaultModelElementSerializer(this);
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
