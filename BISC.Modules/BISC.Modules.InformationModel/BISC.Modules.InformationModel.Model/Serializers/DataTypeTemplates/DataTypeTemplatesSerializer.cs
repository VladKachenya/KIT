using System;
using System.Xml.Linq;
using BISC.Infrastructure.Global.Modularity;
using BISC.Model.Global.Model;
using BISC.Model.Global.Serializators;
using BISC.Model.Infrastructure;
using BISC.Modules.InformationModel.Infrastucture;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates;

namespace BISC.Modules.InformationModel.Model.Serializers.DataTypeTemplates
{
    public class DataTypeTemplatesSerializer : IModelElementSerializer<IDataTypeTemplates>
    {
        public DataTypeTemplatesSerializer()
        {
        }


        #region Implementation of IModelElementSerializer

        public XElement SerializeModelElement(IModelElement modelElement)
        {
            IDataTypeTemplates dataTypeTemplates = modelElement as IDataTypeTemplates;
            XElement dataTypeTemplatesElement = new XElement(InfoModelKeys.DataTypeTemplateKeys.DataTypeTemplatesModelItemKey);
            DaTypeSerializer daTypeSerializer = new DaTypeSerializer();
            DoTypeSerializer doTypeSerializer = new DoTypeSerializer();
            EnumTypeSerializer enumTypeSerializer = new EnumTypeSerializer();
            LNodeTypeSerializer lNodeTypeSerializer = new LNodeTypeSerializer();

            foreach (var lNodeType in dataTypeTemplates.LNodeTypes)
            {
                dataTypeTemplatesElement.Add(lNodeTypeSerializer.SerializeModelElement(lNodeType));
            }
            foreach (var doType in dataTypeTemplates.DoTypes)
            {
                dataTypeTemplatesElement.Add(doTypeSerializer.SerializeModelElement(doType));
            }
            foreach (var daType in dataTypeTemplates.DaTypes)
            {
                dataTypeTemplatesElement.Add(daTypeSerializer.SerializeModelElement(daType));
            }
            foreach (var enumType in dataTypeTemplates.EnumTypes)
            {
                dataTypeTemplatesElement.Add(enumTypeSerializer.SerializeModelElement(enumType));
            }

            return dataTypeTemplatesElement;
        }

        public IDataTypeTemplates DeserializeModelElement(XElement xElement)
        {
            IDataTypeTemplates dataTypeTemplates = new Model.DataTypeTemplates.DataTypeTemplates();
            DaTypeSerializer daTypeSerializer = new DaTypeSerializer();
            DoTypeSerializer doTypeSerializer = new DoTypeSerializer();
            EnumTypeSerializer enumTypeSerializer = new EnumTypeSerializer();
            LNodeTypeSerializer lNodeTypeSerializer = new LNodeTypeSerializer();
            foreach (var xInnerElement in xElement.Elements())
            {
                switch (xInnerElement.Name.LocalName)
                {
                    case InfoModelKeys.DataTypeTemplateKeys.LNodeTypeModelItemKey:
                        dataTypeTemplates.LNodeTypes.Add(lNodeTypeSerializer.DeserializeModelElement(xInnerElement));
                        break;
                    case InfoModelKeys.DataTypeTemplateKeys.DOTypeModelItemKey:
                        dataTypeTemplates.DoTypes.Add(doTypeSerializer.DeserializeModelElement(xInnerElement));
                        break;
                    case InfoModelKeys.DataTypeTemplateKeys.DATypeModelItemKey:
                        dataTypeTemplates.DaTypes.Add(daTypeSerializer.DeserializeModelElement(xInnerElement));
                        break;
                    case InfoModelKeys.DataTypeTemplateKeys.EnumTypeModelItemKey:
                        dataTypeTemplates.EnumTypes.Add(enumTypeSerializer.DeserializeModelElement(xInnerElement));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException($"DataTypeTemplate with key {xInnerElement.Name.LocalName} cannot be deserialized");
                        break;
                }
            }
            return dataTypeTemplates;
        }

        #endregion
    }
}
