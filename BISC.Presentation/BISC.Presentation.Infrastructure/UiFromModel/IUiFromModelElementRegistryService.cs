using BISC.Model.Infrastructure;

namespace BISC.Presentation.Infrastructure.UiFromModel
{
    public interface IUiFromModelElementRegistryService
    {
        void RegisterModelElement(IUiFromModelElementService modelElementUiService, string elementName);
        bool GetIsModelElementRegistered(string elementName);
        bool TryHandleModelElementInUiByKey(IModelElement modelElement);

    }
}