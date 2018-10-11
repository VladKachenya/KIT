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
        public IModelElement ParentModelElement { get; set; }
        public string Namespace { get; set; }
        public List<IModelElement> ChildModelElements { get;}
        public List<XAttribute> ModelElementAttributes { get; }

        public virtual int CompareTo(object obj)
        {
            if (!(obj is IModelElement)) return -1;
            var element = obj as IModelElement;
            if (element.ElementName != ElementName) return -1;
            if (element.Namespace != Namespace) return -1;
            return 1;
        }
    }

   

}
