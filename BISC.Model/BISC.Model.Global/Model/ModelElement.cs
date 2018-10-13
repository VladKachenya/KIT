using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;
using BISC.Infrastructure.Global.Modularity;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Model.Global.Model
{
    [DebuggerDisplay("{ElementName} , Children [{ChildModelElements.Count}] ,Attributes [{ModelElementAttributes.Count}]")]

    public class ModelElement: IModelElement
    {
        public ModelElement()
        {
            ModelElementAttributes=new List<XAttribute>();
            ChildModelElements=new List<IModelElement>();
        }
        public string ElementName { get; set; }
        public IModelElement ParentModelElement { get; set; }
        public string Namespace { get; set; }
        public List<IModelElement> ChildModelElements { get;}
        public List<XAttribute> ModelElementAttributes { get; }

        public virtual bool ModelElementCompareTo(IModelElement obj)
        {
            if (obj == null) return false;
            if (obj.ElementName != ElementName) return false;
            if (obj.Namespace != Namespace) return false;
            return true;
        }
    }

   

}
