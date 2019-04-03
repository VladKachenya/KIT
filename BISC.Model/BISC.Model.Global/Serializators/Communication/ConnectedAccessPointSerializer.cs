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
using BISC.Model.Infrastructure.Serializing;

namespace BISC.Model.Global.Serializators.Communication
{
   public class ConnectedAccessPointSerializer:DefaultModelElementSerializer<IConnectedAccessPoint>
    {
     
        public ConnectedAccessPointSerializer(IModelElementsRegistryService modelElementsRegistryService) : base(modelElementsRegistryService)
        {
            //RegisterModelElementCollection(typeof(SclAddress));
            //RegisterModelElementCollection(typeof(Gse));
            RegisterProperty(nameof(ConnectedAccessPoint.ApName), "apName");
            RegisterProperty(nameof(ConnectedAccessPoint.IedName), "iedName");

        }
        public override IModelElement GetConcreteObject()
        {
            return new ConnectedAccessPoint();
        }

    }
}
