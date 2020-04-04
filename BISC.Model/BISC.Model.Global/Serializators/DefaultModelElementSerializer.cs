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
using System.Text;
using System.Xml;
using System.Xml.Schema;
using BISC.Model.Global.Common;
using BISC.Model.Infrastructure.Keys;
using BISC.Model.Infrastructure.Serializing;

namespace BISC.Model.Global.Serializators
{

    public class SerializableProperty
    {
        public SerializableProperty(string propertyName, string name, SerializingType serializingType)
        {
            PropertyName = propertyName;
            Name = name;
            SerializingType = serializingType;
        }

        public string PropertyName { get; }
        public string Name { get; }
        public SerializingType SerializingType { get; }
    }


    public class DefaultModelElementSerializer<T> : IModelSerializer<T> where T : IModelElement
    {
        private readonly IModelElementsRegistryService _modelElementsRegistryService;

        public DefaultModelElementSerializer(IModelElementsRegistryService modelElementsRegistryService)
        {
            _modelElementsRegistryService = modelElementsRegistryService;
        }


        private List<SerializableProperty> _properties = new List<SerializableProperty>();

        protected void RegisterProperty(string propertyName, string name, SerializingType serializingType = SerializingType.Basic)
        {
            _properties.Add(new SerializableProperty(propertyName, name, serializingType));
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


        public virtual XElement SerializeModelElement(T modelElement, SerializingType serializingType)
        {
            if (!(modelElement is ModelElement))
            {
                throw new Exception("Элемент должен быть зарегистрирован");
            }
            XElement xElement;
            XNamespace iecNameSpace = "http://www.iec.ch/61850/2003/SCL";
            if (modelElement.ElementName == InfrastructureKeys.ModelKeys.SclModelKey)
            {
                StringBuilder stringSclXElement = new StringBuilder();

                XmlWriterSettings xSettings = new XmlWriterSettings
                {
                    ConformanceLevel = System.Xml.ConformanceLevel.Fragment
                };

                XmlWriter xw = System.Xml.XmlWriter.Create(stringSclXElement, xSettings);

                xw.WriteStartElement(InfrastructureKeys.ModelKeys.SclModelKey, "http://www.iec.ch/61850/2003/SCL");
                xw.WriteAttributeString("xsi", "schemaLocation",
                    "http://www.3w.org/2001/XMLSchema-instance", "http://www.iec.ch/61850/2003/SCL SCL.xsd");

                xw.WriteEndElement();
                xw.Flush();

                xElement = XElement.Parse(stringSclXElement.ToString());
            }
            else
            {
                xElement = new XElement(iecNameSpace + modelElement.ElementName);
            }

            FillXElementCustomProperties(xElement, modelElement, serializingType);

            foreach (var modelElementChildElement in modelElement.ChildModelElements)
            {
                var childElement = _modelElementsRegistryService.SerializeModelElement(modelElementChildElement, serializingType);

                xElement.Add(childElement);
            }

            foreach (var attribute in (modelElement as ModelElement).ModelElementAttributes)
            {
                xElement.SetAttributeValue(attribute.Name, attribute.Value);
            }


            return xElement;
        }

        private void FillXElementCustomProperties(XElement xElement, T modelElement, SerializingType serializingType)
        {
            var modelElementProperties = modelElement.GetType().GetProperties();

            if (_properties.Count > 0)
            {
                foreach (var property in _properties)
                {
                    if (serializingType == SerializingType.Basic &&
                        property.SerializingType == SerializingType.Extended)
                    {
                        var attrToDel = modelElement.ModelElementAttributes.FirstOrDefault(
                            (attribute => attribute.Name == property.Name));
                        if (attrToDel != null)
                        {
                            modelElement.ModelElementAttributes.Remove(attrToDel);
                        }
                        continue;
                    }
                    foreach (var propertyInfo in modelElementProperties)
                    {
                        if (property.PropertyName != propertyInfo.Name) continue;
                        var propertyValue = propertyInfo.GetValue(modelElement);
                        if (propertyValue is IModelElement)
                        {
                            if (!modelElement.ChildModelElements.Contains(propertyValue))
                                modelElement.ChildModelElements.Add(propertyValue as IModelElement);
                            break;
                        }
                        else
                        {
                            if (TrySetAttributeValue(modelElement, property.Name,
                                propertyInfo.GetValue(modelElement)?.ToString()))
                                break;
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
                modelElement.ModelElementAttributes.Add(new XAttribute(attributeName, attributeValue));
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
                t.ParentModelElement = modelElement;
                modelElement.ChildModelElements.Add(t);
            }

            FillModelElementCustomProperties(modelElement, xElement);

            modelElement.ChildModelElements.ForEach((element => element.ParentModelElement = modelElement));
            return (T)modelElement;
        }


        private void FillModelElementCustomProperties(IModelElement modelElement, XElement xElement)
        {
            var modelElementProperties = modelElement.GetType().GetProperties();

            if (_properties.Count > 0)
            {
                foreach (var property in _properties)
                {
                    foreach (var propertyInfo in modelElementProperties)
                    {
                        if (property.PropertyName == propertyInfo.Name)
                        {
                            object value = modelElement.ModelElementAttributes.FirstOrDefault((attribute =>
                                attribute.Name == property.Name))?.Value;
                            if (value == null)
                            {
                                try
                                {
                                    value = modelElement.ChildModelElements.FirstOrDefault((me =>
                                        me.ElementName == property.Name));
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

                if (propertyInfo.PropertyType == typeof(int?))
                {
                    propertyInfo?.SetValue(objectToSetProp, Convert.ToInt32(value));
                }
                else if (propertyInfo.PropertyType == typeof(int))
                {
                    propertyInfo?.SetValue(objectToSetProp, Convert.ToInt32(value));
                }
                else if (propertyInfo.PropertyType == typeof(bool?))
                {
                    propertyInfo?.SetValue(objectToSetProp, Convert.ToBoolean(value));
                }
                else if (propertyInfo.PropertyType == typeof(bool))
                {
                    propertyInfo?.SetValue(objectToSetProp, Convert.ToBoolean(value));
                }
                else if (propertyInfo.PropertyType == typeof(long))
                {
                    propertyInfo?.SetValue(objectToSetProp, Convert.ToInt64(value));
                }
                else
                {
                    try
                    {
                        propertyInfo?.SetValue(objectToSetProp, value);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                }
            }
        }



        public XElement SerializeSimpleModelElement(IModelElement modelElement, SerializingType serializingType)
        {
            return SerializeModelElement((T)modelElement, serializingType);
        }

    }
}