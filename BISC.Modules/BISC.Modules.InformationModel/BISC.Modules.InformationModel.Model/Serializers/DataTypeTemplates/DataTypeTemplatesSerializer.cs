using System;
using System.Xml.Linq;
using BISC.Infrastructure.Global.Modularity;
using BISC.Model.Global.Model;
using BISC.Model.Global.Serializators;
using BISC.Model.Infrastructure;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates;

namespace BISC.Modules.InformationModel.Model.Serializers.DataTypeTemplates
{
   public class DataTypeTemplatesSerializer:IModelElementSerializer<IDataTypeTemplates>
    {
        private readonly DefaultModelElementSerializer _defaultModelElementSerializer;

        public DataTypeTemplatesSerializer(DefaultModelElementSerializer defaultModelElementSerializer)
        {
            _defaultModelElementSerializer = defaultModelElementSerializer;
        }


        #region Implementation of IModelElementSerializer

        public XElement SerializeModelElement(IModelElement modelElement)
        {
            IDataTypeTemplates dataTypeTemplates=modelElement as IDataTypeTemplates;
            





            XElement xElement = _defaultModelElementSerializer.SerializeModelElement(modelElement);
            return xElement;
        }

        public IDataTypeTemplates DeserializeModelElement(XElement xElement)
        {
            IDataTypeTemplates dataTypeTemplates = new Model.DataTypeTemplates.DataTypeTemplates();
            
            DefaultModelElement defaultModelElement = _defaultModelElementSerializer.DeserializeModelElement(xElement) as DefaultModelElement;
            return dataTypeTemplates;
        }

        #endregion
    }
}
