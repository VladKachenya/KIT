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
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.LNodeType;
using BISC.Modules.InformationModel.Model.DataTypeTemplates.LNodeType;

namespace BISC.Modules.InformationModel.Model.Serializers.DataTypeTemplates
{
    public class DoSerializer : IModelElementSerializer<IDo>
    {
        private readonly DefaultModelElementSerializer _defaultModelElementSerializer;

        public DoSerializer(DefaultModelElementSerializer defaultModelElementSerializer)
        {
            _defaultModelElementSerializer = defaultModelElementSerializer;
        }
        public IDo DeserializeModelElement(XElement xElement)
        {
            Do doElement = new Do();
            _defaultModelElementSerializer.FillDeserialisedModelElement(doElement,xElement);
            doElement.Name = xElement.GetXAttribute("name");
            doElement.Type = xElement.GetXAttribute("type");
            return doElement;
        }

        public XElement SerializeModelElement(IModelElement modelElement)
        {
            IDo doElement=modelElement as IDo;
            XElement doXElement = _defaultModelElementSerializer.SerializeModelElement(modelElement);
            doXElement.SetXAttribute("name", doElement.Name);
            doXElement.SetXAttribute("type", doElement.Type);

            return doXElement;



        }
    }
}
