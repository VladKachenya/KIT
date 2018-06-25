using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BISC.Infrastructure.Global.Modularity;

namespace BISC.Model.Global.Default
{
   public class DefaultModelElementSerializator:IModelElementSerializator
    {
        private readonly IModelElementsRegistryService _modelElementsRegistryService;

        public DefaultModelElementSerializator(IModelElementsRegistryService modelElementsRegistryService)
        {
            _modelElementsRegistryService = modelElementsRegistryService;
        }


        public XElement SerializeModelElement(IModelElement modelElement)
        {
            XElement xElement = new XElement(modelElement.ElementName);
            foreach (var modelElementChildElement in modelElement.ChildElements)
            {
                xElement.Add(_modelElementsRegistryService
                    .GetModelElementSerializatorByKey(modelElementChildElement.ElementName)
                    .SerializeModelElement(modelElementChildElement));
            }
            return xElement;
        }

        public IModelElement DeserializeModelElement(XElement xElement)
        {
            IModelElement modelElement = _modelElementsRegistryService
                .GetModelElementSerializatorByKey(xElement.Name.LocalName).DeserializeModelElement(xElement);
            return modelElement;
        }
    }
}
