using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BISC.Model.Global.Common;
using BISC.Model.Global.Model.Communication;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;

namespace BISC.Model.Global.Serializators.Communication
{
   public class CommunicationSerializer:DefaultModelElementSerializer<ISclCommunicationModel>
    {

        public CommunicationSerializer()
        {
        }
        public override IModelElement GetConcreteObject()
        {
            return new SclCommunicationModel();
        }

    }
}
