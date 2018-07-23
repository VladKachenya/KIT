using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BISC.Model.Global.Common;
using BISC.Model.Global.Serializators;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType;
using BISC.Modules.InformationModel.Model.DataTypeTemplates.DoType;

namespace BISC.Modules.InformationModel.Model.Serializers.DataTypeTemplates
{
   public class DoTypeSerializer:IModelElementSerializer<IDoType>
    {
        private readonly DefaultModelElementSerializer _defaultModelElementSerializer;

        public DoTypeSerializer(DefaultModelElementSerializer defaultModelElementSerializer)
        {
            _defaultModelElementSerializer = defaultModelElementSerializer;
        }
        
        #region Implementation of IModelElementSerializer<out IDoType>

        public XElement SerializeModelElement(IModelElement modelElement)
        {
            IDoType doType = modelElement as IDoType;
            XElement doTypeXElement = _defaultModelElementSerializer.SerializeModelElement(modelElement);
            doTypeXElement.SetXAttribute("cdc", doType.Cdc);
            doTypeXElement.SetXAttribute("id", doType.Id);
            doTypeXElement.FillChildXElements("DA", new DaSerializer(_defaultModelElementSerializer), doType.DaList);
            doTypeXElement.FillChildXElements("SDO", new SdoSerializer(_defaultModelElementSerializer), doType.SdoList);
            return doTypeXElement;
        }

        public IDoType DeserializeModelElement(XElement xElement)
        {
            DoType doType = new DoType();
            _defaultModelElementSerializer.FillDeserialisedModelElement(doType, xElement);
            doType.Id = xElement.GetXAttribute("id");
            doType.Cdc = xElement.GetXAttribute("cdc");
            xElement.FillChildElements("DA", new DaSerializer(_defaultModelElementSerializer), doType.DaList);
            xElement.FillChildElements("SDO", new SdoSerializer(_defaultModelElementSerializer), doType.SdoList);
            return doType;
        }

        #endregion
    }
}
