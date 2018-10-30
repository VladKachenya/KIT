using System.Xml.Linq;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Serializing;

namespace BISC.Model.Infrastructure
{
    public interface IModelElementSerializer<in T>:IModelElementSerializer where T : IModelElement
    {
        XElement SerializeModelElement(T modelElement,SerializingType serializingType);
    }

    public interface IModelElementSerializer
    {
        XElement SerializeSimpleModelElement(IModelElement modelElement, SerializingType serializingType);
    }


    public interface IModelElementDeSerializer<out T> where T : IModelElement
    {
        T DeserializeModelElement(XElement xElement);
    }



    public interface IModelSerializer<T> : IModelElementDeSerializer<T>, IModelElementSerializer<T> where T : IModelElement
    {

    }

}
