using System.Collections.Generic;
using System.Xml.Linq;
using BISC.Infrastructure.Global.Modularity;

namespace BISC.Model.Infrastructure.Elements
{
    public interface IDefaultModelElement : IModelElement
    {
        string Namespace { get; set; }
        List<IModelElement> ChildModelElements { get; }
        List<XAttribute> ModelElementAttributes { get; }
    }
}