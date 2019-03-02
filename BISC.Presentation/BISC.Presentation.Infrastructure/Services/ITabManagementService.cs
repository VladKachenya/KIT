using System;
using System.Collections.Generic;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Navigation;

namespace BISC.Presentation.Infrastructure.Services
{
   
    public interface ITabManagementService
    {
        void NavigateToTab(string viewName, BiscNavigationParameters navigationParameters,string header, UiEntityIdentifier owner);
        void CloseTab(UiEntityIdentifier owner);
        void CloseTab(string regionId);
        void CloseTabWithChildren(string regionId);

    }
}