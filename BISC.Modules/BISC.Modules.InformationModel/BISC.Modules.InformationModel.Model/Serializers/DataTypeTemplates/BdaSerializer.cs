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
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DaType;
using BISC.Modules.InformationModel.Model.DataTypeTemplates.DaType;

namespace BISC.Modules.InformationModel.Model.Serializers.DataTypeTemplates
{
   public class BdaSerializer:IModelElementSerializer<IBda>
    {
        private readonly DefaultModelElementSerializer _defaultModelElementSerializer;

        public BdaSerializer(DefaultModelElementSerializer defaultModelElementSerializer)
        {
            _defaultModelElementSerializer = defaultModelElementSerializer;
        }

        public XElement SerializeModelElement(IModelElement modelElement)
        {
            IBda bda = modelElement as IBda;
            XElement bdaXElement = _defaultModelElementSerializer.SerializeModelElement(modelElement);
            bdaXElement.SetXAttribute("bType", bda.BType);
            bdaXElement.SetXAttribute("name", bda.Name);
            bdaXElement.SetXAttribute("type", bda.Type);
            return bdaXElement;
        }

        public IBda DeserializeModelElement(XElement xElement)
        {
            Bda bda = new Bda();
            _defaultModelElementSerializer.FillDeserialisedModelElement(bda, xElement);

            bda.BType = xElement.GetXAttribute("bType");
            bda.Name = xElement.GetXAttribute("name");
            bda.Type = xElement.GetXAttribute("type");


            return bda;
        }
    }
}
