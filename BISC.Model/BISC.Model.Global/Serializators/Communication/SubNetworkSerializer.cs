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
    public class SubNetworkSerializer:DefaultModelElementSerializer<ISubNetwork>
    {
        public SubNetworkSerializer()
        {
            RegisterModelElementCollection(typeof(ConnectedAccessPoint));
            RegisterProperty(nameof(ISubNetwork.Desc),"desc");
            RegisterProperty(nameof(ISubNetwork.Type), "type");
            RegisterProperty(nameof(ISubNetwork.Name), "name");

        }
        public override IModelElement GetConcreteObject()
        {
            return new SubNetwork();
        }
    }
}
