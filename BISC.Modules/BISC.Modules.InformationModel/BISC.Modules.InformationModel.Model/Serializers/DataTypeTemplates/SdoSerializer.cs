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
   public class SdoSerializer:IModelElementSerializer<ISdo>
    {
        private readonly DefaultModelElementSerializer _defaultModelElementSerializer;

        public SdoSerializer(DefaultModelElementSerializer defaultModelElementSerializer)
        {
            _defaultModelElementSerializer = defaultModelElementSerializer;
        }
        public XElement SerializeModelElement(IModelElement modelElement)
        {
            ISdo sdo = modelElement as ISdo;
            XElement sdoXElement = _defaultModelElementSerializer.SerializeModelElement(modelElement);
            sdoXElement.SetXAttribute("name", sdo.Name);
            sdoXElement.SetXAttribute("type", sdo.Type);
            return sdoXElement;
        }

        public ISdo DeserializeModelElement(XElement xElement)
        {
            Sdo sdo = new Sdo();
            _defaultModelElementSerializer.FillDeserialisedModelElement(sdo, xElement);
            sdo.Name = xElement.GetXAttribute("name");
            sdo.Type = xElement.GetXAttribute("type");
            return sdo; 
        }
    }
}
