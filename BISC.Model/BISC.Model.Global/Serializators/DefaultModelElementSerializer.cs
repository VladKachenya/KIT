using System;
using System.Xml.Linq;
using BISC.Infrastructure.Global.Modularity;
using BISC.Model.Global.Model;

namespace BISC.Model.Global.Serializators
{
    public class DefaultModelElementSerializer : IModelElementSerializer
    {
        private readonly IModelElementsRegistryService _modelElementsRegistryService;

        public DefaultModelElementSerializer(IModelElementsRegistryService modelElementsRegistryService)
        {
            _modelElementsRegistryService = modelElementsRegistryService;
        }


        public XElement SerializeModelElement(IModelElement modelElement)
        {
            if (!(modelElement is DefaultModelElement))
            {
                throw new Exception("Элемент должен быть зарегистрирован");
            }
            
            XElement xElement;
            if (!string.IsNullOrEmpty((modelElement as DefaultModelElement).Namespace))
            {
                xElement = new XElement("{"+(modelElement as DefaultModelElement).Namespace+"}"+modelElement.ElementName);
             
            }
            else
            {
                xElement = new XElement(modelElement.ElementName);
            }
            foreach (var modelElementChildElement in (modelElement as DefaultModelElement).ChildModelElements)
            {
                xElement.Add(_modelElementsRegistryService
                    .GetModelElementSerializatorByKey(modelElementChildElement.ElementName)
                    .SerializeModelElement(modelElementChildElement));
            }

            foreach (var attribute in (modelElement as DefaultModelElement).ModelElementAttributes)
            {
                xElement.SetAttributeValue(attribute.Name,attribute.Value);
            }

            return xElement;
        }

        public IModelElement DeserializeModelElement(XElement xElement)
        {
            DefaultModelElement modelElement = new DefaultModelElement();
            modelElement.ElementName = xElement.Name.LocalName;
            if (!string.IsNullOrEmpty(xElement.Name.NamespaceName))
            {
                modelElement.Namespace = xElement.Name.NamespaceName;

            }
            foreach (var attribute in xElement.Attributes())
            {
               modelElement.ModelElementAttributes.Add(attribute);
            }

            foreach (var element in xElement.Elements())
            {
                modelElement.ChildModelElements.Add(_modelElementsRegistryService
                    .GetModelElementSerializatorByKey(element.Name.LocalName).DeserializeModelElement(element));
            }

            return modelElement;
        }
    }
}