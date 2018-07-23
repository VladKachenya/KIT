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
    public class DaTypeSerializer:IModelElementSerializer<IDaType>
    {
        private readonly DefaultModelElementSerializer _defaultModelElementSerializer;

        public DaTypeSerializer(DefaultModelElementSerializer defaultModelElementSerializer)
        {
            _defaultModelElementSerializer = defaultModelElementSerializer;
        }


        public XElement SerializeModelElement(IModelElement modelElement)
        {
            IDaType daType = modelElement as IDaType;
            XElement daTypeXElement = _defaultModelElementSerializer.SerializeModelElement(modelElement);
            daTypeXElement.SetXAttribute("id", daType.Id);
            daTypeXElement.FillChildXElements("BDA", new BdaSerializer(_defaultModelElementSerializer),
                daType.Bdas);

            return daTypeXElement;
        }

        public IDaType DeserializeModelElement(XElement xElement)
        {
            DaType daType = new DaType();
            _defaultModelElementSerializer.FillDeserialisedModelElement(daType, xElement);
            daType.Id = xElement.GetXAttribute("id");
            xElement.FillChildElements("BDA",new BdaSerializer(_defaultModelElementSerializer),daType.Bdas );
            return daType;
        }
    }
}
