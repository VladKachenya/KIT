using System;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Tree;

namespace BISC.Presentation.Infrastructure.Services
{
    public interface ITreeManagementService
    {
        UiEntityIdentifier AddTreeItem(BiscNavigationParameters parameters,string viewName, UiEntityIdentifier parentUiEntityIdentifier,string tag=null);
        void DeleteTreeItem(UiEntityIdentifier uiEntityId);
	    UiEntityIdentifier GetDeviceTreeItem(Guid deviceGuid);

        void ClearMainTree();

    }
}