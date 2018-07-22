using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BISC.Model.Global.Serializators;
using BISC.Model.Infrastructure;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.LNodeType;
using BISC.Model.Global.Common;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Model.DataTypeTemplates.LNodeType;

namespace BISC.Modules.InformationModel.Model.Serializers.DataTypeTemplates
{
    public class LNodeTypeSerializer : IModelElementSerializer<ILNodeType>
    {
        private readonly DefaultModelElementSerializer _defaultModelElementSerializer;

        public LNodeTypeSerializer(DefaultModelElementSerializer defaultModelElementSerializer)
        {
            _defaultModelElementSerializer = defaultModelElementSerializer;
        }



        public XElement SerializeModelElement(IModelElement modelElement)
        {
            ILNodeType nodeType = modelElement as ILNodeType;
            XElement lnodeTypeXElement = _defaultModelElementSerializer.SerializeModelElement(modelElement);
            lnodeTypeXElement.SetXAttribute("id", nodeType.Id);
            lnodeTypeXElement.FillChildXElements("DO", new DoSerializer(_defaultModelElementSerializer),nodeType.DoList);
           
            return lnodeTypeXElement;
        }

        public ILNodeType DeserializeModelElement(XElement xElement)
        {
            LNodeType nodeType = new LNodeType();
            _defaultModelElementSerializer.FillDeserialisedModelElement(nodeType, xElement);
            nodeType.Id = xElement.GetXAttribute("id");
            xElement.FillChildElements("DO", new DoSerializer(_defaultModelElementSerializer), nodeType.DoList);


            return nodeType;
        }
    }
}
