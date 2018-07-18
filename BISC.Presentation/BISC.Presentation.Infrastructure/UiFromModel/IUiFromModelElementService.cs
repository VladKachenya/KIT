using BISC.Model.Infrastructure;

namespace BISC.Presentation.Infrastructure.UiFromModel
{
    public interface IUiFromModelElementService
    {
        void HandleModelElement(IModelElement modelElement);
    }
}