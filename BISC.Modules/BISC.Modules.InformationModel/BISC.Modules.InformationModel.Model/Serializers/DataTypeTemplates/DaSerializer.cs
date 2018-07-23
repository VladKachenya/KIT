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
   public class DaSerializer:IModelElementSerializer<IDa>
    {
        private readonly DefaultModelElementSerializer _defaultModelElementSerializer;

        public DaSerializer(DefaultModelElementSerializer defaultModelElementSerializer)
        {
            _defaultModelElementSerializer = defaultModelElementSerializer;
        }
        public XElement SerializeModelElement(IModelElement modelElement)
        {
            IDa da = modelElement as IDa;
            XElement daXElement = _defaultModelElementSerializer.SerializeModelElement(modelElement);
            daXElement.SetXAttribute("bType", da.BType);
            daXElement.SetXAttribute("fc", da.Fc);
            daXElement.SetXAttribute("type", da.Type);
            daXElement.SetXAttribute("name", da.Name);
            return daXElement;
        }

        public IDa DeserializeModelElement(XElement xElement)
        {
            Da da = new Da();
            _defaultModelElementSerializer.FillDeserialisedModelElement(da, xElement);
            da.BType = xElement.GetXAttribute("bType");
            da.Type = xElement.GetXAttribute("type");
            da.Fc = xElement.GetXAttribute("fc");
            da.Name = xElement.GetXAttribute("name");

            return da;
        }
    }
}
