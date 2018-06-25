using BISC.Infrastructure.Global.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.InformationModel.Model.Elements
{
   public abstract class InformationModelElementBase:IModelElement
    {
        public InformationModelElementBase()
        {
            ChildElements=new List<IModelElement>();
        }
        public abstract string ElementName { get; }
        public List<IModelElement> ChildElements { get; }
    }
}
