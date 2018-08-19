using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            if (!element.Attributes().Any((attribute => attribute.Name.LocalName == attributeName))) return null;
            return element.Attribute(attributeName).Value;
        }
        public static XElement SetXAttribute(this XElement element, string attributeName, string attributeValue)
        {
            if (attributeValue == null) return element;
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

        public static IModelElement FillChildModelElements<T>(this IModelElement modelElement, List<T> childElementsList)
        {
            childElementsList.Clear();
            foreach (var childModelElement in modelElement.ChildModelElements)
            {
                if (childModelElement is T childModelElementToAdd)
                {
                    childElementsList.Add(childModelElementToAdd);
                }
            }
            return modelElement;
        }
        public static IModelElement ReplaceChildElements<T>(this IModelElement modelElement, List<T> childElementsList)
        {
           var elementsToRemove= modelElement.ChildModelElements.Where((element => element is T)).ToList();
           elementsToRemove.ForEach((elementToRemove) =>modelElement.ChildModelElements.Remove(elementToRemove));
            foreach (var childModelElement in modelElement.ChildModelElements)
            {
                if (childModelElement is T childModelElementToAdd)
                {
                    childElementsList.Add(childModelElementToAdd);
                }
            }
            return modelElement;
        }


      
    }
}
