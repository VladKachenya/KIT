using System;
using System.Xml.Linq;
using BISC.Infrastructure.Global.Modularity;
using BISC.Model.Global.Model;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Model.Global.Serializators
{
    public class DefaultModelElementSerializer : IModelElementSerializer<IModelElement>
    {
        private readonly IModelElementsRegistryService _modelElementsRegistryService;

        public DefaultModelElementSerializer(IModelElementsRegistryService modelElementsRegistryService)
        {
            _modelElementsRegistryService = modelElementsRegistryService;
        }


        public XElement SerializeModelElement(IModelElement modelElement)
        {
            if (!(modelElement is ModelElement))
            {
                throw new Exception("Элемент должен быть зарегистрирован");
            }
            
            XElement xElement;
            if (!string.IsNullOrEmpty((modelElement as ModelElement).Namespace))
            {
                xElement = new XElement("{"+(modelElement as ModelElement).Namespace+"}"+modelElement.ElementName);
             
            }
            else
            {
                xElement = new XElement(modelElement.ElementName);
            }
            foreach (var modelElementChildElement in (modelElement as ModelElement).ChildModelElements)
            {
                xElement.Add(_modelElementsRegistryService
                    .GetModelElementSerializatorByKey(modelElementChildElement.ElementName)
                    .SerializeModelElement(modelElementChildElement));
            }

            foreach (var attribute in (modelElement as ModelElement).ModelElementAttributes)
            {
                xElement.SetAttributeValue(attribute.Name,attribute.Value);
            }

            return xElement;
        }

        public IModelElement DeserializeModelElement(XElement xElement)
        {
            ModelElement modelElement = new ModelElement();
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

            modelElement.ChildModelElements.ForEach((element =>element.ParentModelElement=modelElement ));
            return modelElement;
        }

        public void FillDeserialisedModelElement(ModelElement existingModelElementodelElement,XElement xElement)
        {
            ModelElement newModelElement = DeserializeModelElement(xElement) as ModelElement;
            existingModelElementodelElement.ChildModelElements.AddRange(newModelElement.ChildModelElements);
            existingModelElementodelElement.ModelElementAttributes.AddRange(newModelElement.ModelElementAttributes);
            existingModelElementodelElement.Namespace = newModelElement.Namespace;
            existingModelElementodelElement.ElementName = newModelElement.ElementName;
        }
    }
}