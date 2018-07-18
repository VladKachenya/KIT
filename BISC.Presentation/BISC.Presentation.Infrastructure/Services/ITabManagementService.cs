using System;
using System.Collections.Generic;
using BISC.Presentation.Infrastructure.Navigation;

namespace BISC.Presentation.Infrastructure.Services
{
   
    public interface ITabManagementService
    {
        void NavigateToTab(string viewName, BiscNavigationParameters navigationParameters,string header, TreeItemIdentifier owner);
        void CloseTab(TreeItemIdentifier owner);
        void CloseTab(string regionId);

    }
}