using System;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Presentation.Infrastructure.UiFromModel
{
    public interface IUiFromModelElementService
    {
        void HandleModelElement(IModelElement modelElement, TreeItemIdentifier uiParentId,string uiKey);
    }
}