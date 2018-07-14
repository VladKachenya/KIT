using System.Collections.Generic;
using BISC.Presentation.Infrastructure.Navigation;

namespace BISC.Presentation.Infrastructure.Services
{
    public interface INavigationService
    {
        void NavigateViewToRegion(string viewName, string regionName, BiscNavigationParameters navigationParameters =null);
        void NavigateViewToGlobalRegion(string viewName, BiscNavigationParameters navigationParameters = null);
        void TryNavigateToWaitingRegion(string regionId);
    }
}