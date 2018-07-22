using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Model.Global.Common
{
    public static class Extesions
    {
        public static string GetXAttribute(this XElement element, string attributeName)
        {
            return element.Attribute(attributeName).Value;
        }
        public static XElement SetXAttribute(this XElement element, string attributeName, string attributeValue)
        {
            element.SetAttributeValue(attributeName, attributeValue);
            return element;
        }

        public static XElement CleanChildXElements(this XElement element, string elementName)
        {
            foreach (var xElement in element.Elements(elementName))
            {
                xElement.Remove();
            }

            return element;
        }

     

        public static XElement FillChildXElements<T>(this XElement element, string elementName, IModelElementSerializer<IModelElement> serializer, List<T> modelElements) where T : IModelElement
        {
            element.CleanChildXElements(elementName);
            foreach (var modelElement in modelElements)
            {

                element.Add(serializer.SerializeModelElement(modelElement));
            }
            return element;
        }


        public static XElement FillChildElements<T>(this XElement element, string elementName, IModelElementSerializer<IModelElement> serializer, List<T> modelElements) where T : IModelElement
        {
            element.CleanChildXElements(elementName);
            foreach (var innerXElement in element.Elements())
            {
                if (innerXElement.Name.LocalName != elementName) continue;

                modelElements.Add((T)serializer.DeserializeModelElement(innerXElement));
            }
            return element;
        }

    }
}
