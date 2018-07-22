using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BISC.Model.Infrastructure;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DaType;

namespace BISC.Modules.InformationModel.Model.Serializers.DataTypeTemplates
{
    public class DaTypeSerializer:IModelElementSerializer<IDaType>
    {
        public XElement SerializeModelElement(IModelElement modelElement)
        {
            throw new NotImplementedException();
        }

        public IDaType DeserializeModelElement(XElement xElement)
        {
            throw new NotImplementedException();
        }
    }
}
