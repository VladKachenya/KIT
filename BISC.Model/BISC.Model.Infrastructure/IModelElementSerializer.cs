using System.Xml.Linq;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Model.Infrastructure
{
    public interface IModelElementSerializer<out T> where T: IModelElement
    {
        XElement SerializeModelElement(IModelElement modelElement);
        T DeserializeModelElement(XElement xElement);
    }
}
