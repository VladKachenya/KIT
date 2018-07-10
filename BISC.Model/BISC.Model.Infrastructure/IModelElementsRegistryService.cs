using System.Collections.Generic;
using System.Xml.Linq;

namespace BISC.Infrastructure.Global.Modularity
{
    public interface IModelElementsRegistryService
    {
        void RegisterModelElement(IModelElementSerializer modelElementSerializer, string elementName);
        bool GetIsModelElementRegistered(string elementName);
        IModelElementSerializer GetModelElementSerializatorByKey(string elementName,bool isDefaultSerializatorAllowed=true);

    }

    public interface IModelElement
    {
        string ElementName { get; }
    }

  
}