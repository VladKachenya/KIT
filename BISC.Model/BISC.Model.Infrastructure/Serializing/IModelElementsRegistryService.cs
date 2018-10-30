using System.Xml.Linq;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Serializing;

namespace BISC.Model.Infrastructure
{
    public interface IModelElementsRegistryService
    {
        void RegisterModelElement<T>(IModelSerializer<T> modelElementSerializer, string elementName) where T : IModelElement;
        bool GetIsModelElementRegistered(string elementName);

        T DeserializeModelElement<T>(XElement xElement, bool isDefaultSerializatorAllowed = true) where T : IModelElement;
        XElement SerializeModelElement<T>(T modelElement, SerializingType serializingType, bool isDefaultSerializatorAllowed = true) where T : IModelElement;


    }



}