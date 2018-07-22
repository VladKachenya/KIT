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
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.EnumType;
using BISC.Modules.InformationModel.Model.DataTypeTemplates.EnumType;

namespace BISC.Modules.InformationModel.Model.Serializers.DataTypeTemplates
{
   public class EnumValSerializer:IModelElementSerializer<IEnumVal>
    {
        private readonly DefaultModelElementSerializer _defaultModelElementSerializer;

        public EnumValSerializer(DefaultModelElementSerializer defaultModelElementSerializer)
        {
            _defaultModelElementSerializer = defaultModelElementSerializer;
        }

        public XElement SerializeModelElement(IModelElement modelElement)
        {
            IEnumVal enumVal = modelElement as IEnumVal;
            XElement enumTypeXElement = _defaultModelElementSerializer.SerializeModelElement(modelElement);
            enumTypeXElement.SetXAttribute("ord", enumVal.Ord.ToString());
            enumTypeXElement.SetValue(enumVal.Value);
            return enumTypeXElement;
        }

        public IEnumVal DeserializeModelElement(XElement xElement)
        {
            EnumVal enumVal = new EnumVal();
            _defaultModelElementSerializer.FillDeserialisedModelElement(enumVal, xElement);
            enumVal.Ord =int.Parse(xElement.GetXAttribute("ord"));
            enumVal.Value = xElement.Value;
            return enumVal;
        }
    }
}
