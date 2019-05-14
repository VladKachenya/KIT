using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BISC.Infrastructure.Global.Modularity;
using BISC.Model.Global.Serializators;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Serializing;

namespace BISC.Model.Global.Services
{
    public class ModelElementsRegistryService : IModelElementsRegistryService
    {
        private Dictionary<string, object> _modelElementSerializatorDictionary = new Dictionary<string, object>();

        public void RegisterModelElement(IModelSerializer<IModelElement> modelElementSerializer, string elementName)
        {
            if (_modelElementSerializatorDictionary.ContainsKey(elementName))
            {
                throw new ArgumentException($"Serializator with key {elementName} already exists");
            }
            _modelElementSerializatorDictionary.Add(elementName, modelElementSerializer);
        }

        public void RegisterModelElement<T>(IModelSerializer< T> modelElementSerializer, string elementName)  where T:IModelElement
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

        public T DeserializeModelElement<T>(XElement xElement, bool isDefaultSerializatorAllowed = true) where T : IModelElement
        {
            if (!_modelElementSerializatorDictionary.ContainsKey(xElement.Name.LocalName))
            {
                if (isDefaultSerializatorAllowed)
                {
                    return (new DefaultModelElementSerializer<T>(this)).DeserializeModelElement(xElement);
                }
                else
                {
                    throw new ArgumentException($"Serializator with key {xElement.Name.LocalName} for {xElement} is not added");
                }
            }

          return  (T)(_modelElementSerializatorDictionary[xElement.Name.LocalName] as IModelElementDeSerializer<T>).DeserializeModelElement(xElement);
        }

        public XElement SerializeModelElement<T>(T modelElement,SerializingType serializingType, bool isDefaultSerializatorAllowed = true) where T : IModelElement
        {
            if (!_modelElementSerializatorDictionary.ContainsKey(modelElement.ElementName))
            {
                if (isDefaultSerializatorAllowed)
                {
                    return (new DefaultModelElementSerializer<IModelElement>(this)).SerializeModelElement(modelElement,serializingType);
                }
                else
                {
                    throw new ArgumentException($"Serializator with key {modelElement.ElementName} is not added");
                }
            }

            return (_modelElementSerializatorDictionary[modelElement.ElementName] as IModelElementSerializer).SerializeSimpleModelElement(modelElement,serializingType);
        }


     

     
    }
}
