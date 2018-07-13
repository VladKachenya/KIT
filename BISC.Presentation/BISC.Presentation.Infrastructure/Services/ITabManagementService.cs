using System.Collections.Generic;
using BISC.Presentation.Infrastructure.Navigation;

namespace BISC.Presentation.Infrastructure.Services
{
   
    public interface ITabManagementService
    {
        void NavigateToTab(string viewName, BiscNavigationParameters navigationParameters,object owner);
        void CloseTabs(object owner);
    }
}