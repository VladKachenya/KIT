using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BISC.Model.Infrastructure;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.LNodeType;

namespace BISC.Modules.InformationModel.Model.Serializers.DataTypeTemplates
{
   public class LNodeTypeSerializer : IModelElementSerializer<ILNodeType>
    {
        public XElement SerializeModelElement(IModelElement modelElement)
        {
            throw new NotImplementedException();
        }

        public ILNodeType DeserializeModelElement(XElement xElement)
        {
            throw new NotImplementedException();
        }
    }
}
