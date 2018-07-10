using BISC.Infrastructure.Global.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure;

namespace BISC.Modules.InformationModel.Model.Elements
{
   public abstract class InformationModelElementBase:IModelElement
    {
        public InformationModelElementBase()
        {

        }
        public abstract string ElementName { get; }
    }
}
