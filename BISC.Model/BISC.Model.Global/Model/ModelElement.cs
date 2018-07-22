using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;
using BISC.Infrastructure.Global.Modularity;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Model.Global.Model
{
    [DebuggerDisplay("{ElementName} , Children [{ChildModelElements.Count}] ,Attributes [{ModelElementAttributes.Count}]")]

    public class ModelElement:IModelElement
    {
        public ModelElement()
        {
            ModelElementAttributes=new List<XAttribute>();
            ChildModelElements=new List<IModelElement>();
        }
        public string ElementName { get; set; }
        public string Namespace { get; set; }
        public virtual List<IModelElement> ChildModelElements { get;}
        public List<XAttribute> ModelElementAttributes { get; }
    }

   

}
