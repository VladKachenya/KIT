using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Presentation.Infrastructure.UiFromModel
{
    public interface IUiFromModelElementRegistryService
    {
        void RegisterModelElement(IUiFromModelElementService modelElementUiService,string uiKey);
        bool TryHandleModelElementInUiByKey(IModelElement modelElement,TreeItemIdentifier parentTreeItemIdentifier,string uiKey);

    }
}