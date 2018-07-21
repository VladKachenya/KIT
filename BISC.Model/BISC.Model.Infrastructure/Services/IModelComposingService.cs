using System.Xml.Linq;
using BISC.Model.Infrastructure.Project;

namespace BISC.Model.Infrastructure.Services
{
    public interface IModelComposingService
    {
        ISclModel DeserializeModelFromFile(XElement mainElement);
        void SerializeModelInFile(string filePath,IModelElement modelElement);
    }
}