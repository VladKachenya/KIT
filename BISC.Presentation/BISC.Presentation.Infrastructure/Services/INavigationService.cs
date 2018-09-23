using System.Collections.Generic;
using System.Threading.Tasks;
using BISC.Presentation.Infrastructure.Navigation;

namespace BISC.Presentation.Infrastructure.Services
{
    public interface INavigationService
    {
        void NavigateViewToRegion(string viewName, string regionName, BiscNavigationParameters navigationParameters =null);
        Task NavigateViewToGlobalRegion(string viewName, BiscNavigationParameters navigationParameters = null);
        void TryNavigateToWaitingRegion(string regionId);
        void NavigateFromRegion(string regionId);

    }
}