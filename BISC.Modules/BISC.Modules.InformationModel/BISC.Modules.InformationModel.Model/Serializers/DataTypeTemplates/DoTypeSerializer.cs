using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture.DataTypeTemplates.DoType;

namespace BISC.Modules.InformationModel.Model.Serializers.DataTypeTemplates
{
   public class DoTypeSerializer:IModelElementSerializer<IDoType>
    {
        public DoTypeSerializer()
        {
            
        }
        
        #region Implementation of IModelElementSerializer<out IDoType>

        public XElement SerializeModelElement(IModelElement modelElement)
        {
            throw new NotImplementedException();
        }

        public IDoType DeserializeModelElement(XElement xElement)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
