using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BISC.Model.Global.Model.Communication;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project.Communication;

namespace BISC.Model.Global.Serializators.Communication
{
   public class GseSerializer:DefaultModelElementSerializer<IGse>
    {
      
        public GseSerializer( )
        {
            RegisterProperty(nameof(IGse.LdInst), "ldInst");
            RegisterProperty(nameof(IGse.CbName), "cbName");

        }
        public override IModelElement GetConcreteObject()
        {
            return new Gse();
        }

    }

}
