using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Model.Global.Model;
using BISC.Model.Global.Project;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;

namespace BISC.Model.Global.Serializators
{
   public class BiscProjectSerializer : DefaultModelElementSerializer<IBiscProject>
    {
        public BiscProjectSerializer()
        {
            RegisterProperty(nameof(IBiscProject.MainSclModel), "SCL");
            RegisterProperty(nameof(IBiscProject.CustomElements), "CustomElements");

        }
        public override IModelElement GetConcreteObject()
        {
            return new BiscProject();
        }

    }
}
