using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BISC.Infrastructure.Global.Modularity
{
    public interface IModelElementSerializer
    {
        XElement SerializeModelElement(IModelElement modelElement);
        IModelElement DeserializeModelElement(XElement xElement);
    }
}
