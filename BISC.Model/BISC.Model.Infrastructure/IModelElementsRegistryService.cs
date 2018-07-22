using BISC.Model.Infrastructure.Elements;

namespace BISC.Model.Infrastructure
{
    public interface IModelElementsRegistryService
    {
        void RegisterModelElement(IModelElementSerializer<IModelElement> modelElementSerializer, string elementName);
        bool GetIsModelElementRegistered(string elementName);
        IModelElementSerializer<IModelElement> GetModelElementSerializatorByKey(string elementName,bool isDefaultSerializatorAllowed=true);

    }

   
  
}