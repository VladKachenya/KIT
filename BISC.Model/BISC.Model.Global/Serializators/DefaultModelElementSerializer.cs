using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Xml.Linq;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Model.Global.Model;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BISC.Model.Global.Serializators
{
    public class DefaultModelElementSerializer<T> : IModelSerializer<T> where T : IModelElement
    {
        private readonly IModelElementsRegistryService _modelElementsRegistryService;

        public DefaultModelElementSerializer(IModelElementsRegistryService modelElementsRegistryService)
        {
            _modelElementsRegistryService = modelElementsRegistryService;
        }

        private List<Tuple<Type, string>> _collectionTypes = new List<Tuple<Type, String>>();
        protected void RegisterModelElementCollection(Type collectionType, string name = null)
        {
            _collectionTypes.Add(new Tuple<Type, string>(collectionType, name));
        }
        private List<Tuple<string, string>> _properties = new List<Tuple<string, string>>();

        protected void RegisterProperty(string propertyName, string name)
        {
            _properties.Add(new Tuple<string, string>(propertyName, name));
        }
        private string _valuePropertyName = null;
        protected void RegisterValueToProperty(string propertyName)
        {
            _valuePropertyName = propertyName;
        }

        protected DefaultModelElementSerializer()
        {
            _modelElementsRegistryService = StaticContainer.CurrentContainer.ResolveType<IModelElementsRegistryService>();
        }


        public virtual XElement SerializeModelElement(T modelElement)

        {
            if (!(modelElement is ModelElement))
            {
                throw new Exception("Элемент должен быть зарегистрирован");
            }

            XElement xElement;

            if (!string.IsNullOrEmpty((modelElement as ModelElement).Namespace))
            {
                xElement = new XElement("{" + (modelElement as ModelElement).Namespace + "}" + modelElement.ElementName);
            }
            else
            {
                xElement = new XElement(modelElement.ElementName);
            }
            FillXElementCustomProperties(xElement, modelElement);
            foreach (var modelElementChildElement in modelElement.ChildModelElements)
            {
                var childElement = _modelElementsRegistryService.SerializeModelElement(modelElementChildElement);

                xElement.Add(childElement);
            }

            foreach (var attribute in (modelElement as ModelElement).ModelElementAttributes)
            {
                xElement.SetAttributeValue(attribute.Name, attribute.Value);
            }


            return xElement;
        }

        private void FillXElementCustomProperties(XElement xElement, T modelElement)
        {
            var modelElementProperties = modelElement.GetType().GetProperties();

            if (_properties.Count > 0)
            {
                foreach (var property in _properties)
                {
                  
                    foreach (var propertyInfo in modelElementProperties)
                    {
                        if (property.Item1 != propertyInfo.Name) continue;
                        var propertyValue = propertyInfo.GetValue(modelElement);
                        if (propertyValue is IModelElement)
                        {
                            modelElement.ChildModelElements.Add(propertyValue as IModelElement);
                            break;
                        }
                        else
                        {
                            if (TrySetAttributeValue(modelElement, property.Item2,
                                propertyInfo.GetValue(modelElement)?.ToString()))
                                break;
                        }
                    }
                }
            }

            if (_collectionTypes.Count > 0)
            {
                if (_collectionTypes.Count > 0)
                {
                    foreach (var collectionType in _collectionTypes)
                    {
                        foreach (var propertyInfo in modelElementProperties)
                        {
                            if ((collectionType.Item2 != null) && (propertyInfo.Name != collectionType.Item2)) continue;
                            if ((propertyInfo.Name == nameof(IModelElement.ChildModelElements))) continue;

                            Type type = propertyInfo.PropertyType;
                            if (type.IsGenericType && type.GetGenericTypeDefinition()
                                == typeof(List<>))
                            {
                                Type itemType = type.GetGenericArguments()[0];
                                if (itemType == collectionType.Item1 || collectionType.Item1.GetInterface(itemType.ToString()) != null)
                                {
                                    var elementsOfType =
                                        modelElement.ChildModelElements.Where((element => element.GetType() == collectionType.Item1)).ToList();
                                    
                                    foreach (var element in elementsOfType)
                                    {
                                        modelElement.ChildModelElements.Remove(element);
                                    }
                                    modelElement.ChildModelElements.AddRange((IEnumerable<IModelElement>)propertyInfo.GetValue(modelElement) );
                                    

                                }
                            }

                        }
                    }
                }
            }
            if (_valuePropertyName != null)
            {
                var property = modelElement.GetType().GetProperty(_valuePropertyName);
                xElement.Value = property.GetValue(modelElement).ToString();
            }
        }

        private bool TrySetAttributeValue(IModelElement modelElement, string attributeName, string attributeValue)
        {
            if (attributeValue == null) return true;
            var attribute =
                modelElement.ModelElementAttributes.FirstOrDefault(
                    (xAttribute => xAttribute.Name == attributeName));
            if (attribute != null)
            {
                attribute.Value = attributeValue;
                return true;
            }
            else
            {
                modelElement.ModelElementAttributes.Add(new XAttribute(attributeName,attributeValue));
                return true;
            }

            return false;
        }

        public virtual IModelElement GetConcreteObject()
        {
            return new ModelElement();
        }

        public T DeserializeModelElement(XElement xElement)
        {
            IModelElement modelElement = GetConcreteObject();
            (modelElement as ModelElement).ElementName = xElement.Name.LocalName;
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
                var t = _modelElementsRegistryService.DeserializeModelElement<IModelElement>(element);
                modelElement.ChildModelElements.Add(t);
            }

            FillModelElementCustomProperties(modelElement, xElement);

            modelElement.ChildModelElements.ForEach((element => element.ParentModelElement = modelElement));
            return (T)modelElement;
        }


        private void FillModelElementCustomProperties(IModelElement modelElement, XElement xElement)
        {
            var modelElementProperties = modelElement.GetType().GetProperties();

            if (_collectionTypes.Count > 0)
            {
                foreach (var collectionType in _collectionTypes)
                {
                    foreach (var propertyInfo in modelElementProperties)
                    {
                        if ((collectionType.Item2 != null) && (propertyInfo.Name != collectionType.Item2)) continue;
                        if ((propertyInfo.Name == nameof(IModelElement.ChildModelElements))) continue;

                        Type type = propertyInfo.PropertyType;
                        if (type.IsGenericType && type.GetGenericTypeDefinition()
                            == typeof(List<>))
                        {
                            Type itemType = type.GetGenericArguments()[0];
                            if (itemType == collectionType.Item1 || collectionType.Item1.GetInterface(itemType.ToString()) != null)
                            {
                                var elementsOfType =
                                    modelElement.ChildModelElements.Where((element => element.GetType() == collectionType.Item1)).ToList();
                                foreach (var element in elementsOfType)
                                {
                                    type.GetMethod("Add").Invoke(propertyInfo.GetValue(modelElement), new[] { element });
                                }
                            }
                        }

                    }
                }
            }
            if (_properties.Count > 0)
            {
                foreach (var property in _properties)
                {
                    foreach (var propertyInfo in modelElementProperties)
                    {
                        if (property.Item1 == propertyInfo.Name)
                        {
                            object value = modelElement.ModelElementAttributes.FirstOrDefault((attribute =>
                                attribute.Name == property.Item2))?.Value;
                            if (value == null)
                            {
                                try
                                {
                                    value = modelElement.ChildModelElements.FirstOrDefault((me =>
                                        me.ElementName == property.Item2));
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                }
                               
                            }
                            SetProperty(propertyInfo, modelElement, value);
                            break;
                        }
                    }
                }
            }
            if (_valuePropertyName != null)
            {
                var property = modelElement.GetType().GetProperty(_valuePropertyName);
                SetProperty(property, modelElement, xElement.Value);
            }

        }



        private void SetProperty(PropertyInfo propertyInfo, object objectToSetProp, object value)
        {
            if (propertyInfo != null)
            {
                if (propertyInfo.PropertyType == typeof(int))
                {
                    propertyInfo?.SetValue(objectToSetProp, Convert.ToInt32(value));
                }
                else if (propertyInfo.PropertyType == typeof(bool?))
                {
                    propertyInfo?.SetValue(objectToSetProp, Convert.ToBoolean(value));
                }
                else
                {
                    propertyInfo?.SetValue(objectToSetProp, value);

                }
            }
        }

        public XElement SerializeSimpleModelElement(IModelElement modelElement)
        {
            return SerializeModelElement((T)modelElement);
        }
    }
}