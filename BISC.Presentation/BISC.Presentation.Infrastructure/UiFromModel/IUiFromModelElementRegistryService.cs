﻿using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Elements;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Presentation.Infrastructure.UiFromModel
{
    public interface IUiFromModelElementRegistryService
    {
        void RegisterModelElement(IUiFromModelElementService modelElementUiService,string uiKey);
        bool TryHandleModelElementInUiByKey(IModelElement modelElement,UiEntityIdentifier parentUiEntityIdentifier,string uiKey);

    }
}