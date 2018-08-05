using System.Xml.Linq;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Model.Infrastructure
{
    public interface IModelElementSerializer<in T>:IModelElementSerializer where T : IModelElement
    {
        XElement SerializeModelElement(T modelElement);
    }

    public interface IModelElementSerializer
    {
        XElement SerializeSimpleModelElement(IModelElement modelElement);
    }


    public interface IModelElementDeSerializer<out T> where T : IModelElement
    {
        T DeserializeModelElement(XElement xElement);
    }



    public interface IModelSerializer<T> : IModelElementDeSerializer<T>, IModelElementSerializer<T> where T : IModelElement
    {

    }

}
