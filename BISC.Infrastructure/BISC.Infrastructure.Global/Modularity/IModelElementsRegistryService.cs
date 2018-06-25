using System.Collections.Generic;
using System.Xml.Linq;

namespace BISC.Infrastructure.Global.Modularity
{
    public interface IModelElementsRegistryService
    {
        void RegisterModelElement(IModelElementSerializator modelElementSerializator, string elementName);
        bool GetIsModelElementRegistered(string elementName);
        IModelElementSerializator GetModelElementSerializatorByKey(string elementName,bool isDefaultSerializatorAllowed=true);

    }

    public interface IModelElement
    {
        string ElementName { get; }
        List<IModelElement> ChildElements { get; }
    }

    public interface IModelElementSerializator
    {
        XElement SerializeModelElement(IModelElement modelElement);
        IModelElement DeserializeModelElement(XElement xElement);
    }
}