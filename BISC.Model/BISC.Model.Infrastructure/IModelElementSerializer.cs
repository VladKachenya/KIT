using System.Xml.Linq;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Model.Infrastructure
{
    public interface IModelElementSerializer<in T> where T : IModelElement
    {
        XElement SerializeModelElement(T modelElement);
    }


    public interface IModelElementDeSerializer<out T> where T : IModelElement
    {
        T DeserializeModelElement(XElement xElement);
    }

    public interface IModelSerializer<T> : IModelElementDeSerializer<T>, IModelElementSerializer<T> where T : IModelElement
    {

    }

}
