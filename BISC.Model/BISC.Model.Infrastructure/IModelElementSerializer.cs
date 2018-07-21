using System.Xml.Linq;

namespace BISC.Model.Infrastructure
{
    public interface IModelElementSerializer<out T> where T: IModelElement
    {
        XElement SerializeModelElement(IModelElement modelElement);
        T DeserializeModelElement(XElement xElement);
    }
}
