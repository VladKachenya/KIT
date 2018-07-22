using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Presentation.Infrastructure.UiFromModel
{
    public interface IUiFromModelElementRegistryService
    {
        void RegisterModelElement(IUiFromModelElementService modelElementUiService, string elementName);
        bool GetIsModelElementRegistered(string elementName);
        bool TryHandleModelElementInUiByKey(IModelElement modelElement);

    }
}