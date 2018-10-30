using System.Xml.Linq;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Project;
using BISC.Model.Infrastructure.Serializing;

namespace BISC.Model.Infrastructure.Services
{
    public interface IModelComposingService
    {
        ISclModel DeserializeModelFromFile(XElement mainElement);
        void SerializeModelInFile(string filePath,IModelElement modelElement,SerializingType serializingType);
    }
}