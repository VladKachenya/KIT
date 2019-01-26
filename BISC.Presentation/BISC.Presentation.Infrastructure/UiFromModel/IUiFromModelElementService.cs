using System;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Presentation.Infrastructure.UiFromModel
{
    public interface IUiFromModelElementService
    {
        TreeItemIdentifier HandleModelElement(IModelElement modelElement, TreeItemIdentifier uiParentId,string uiKey);
    }
}