using System.Collections.Generic;
using System.Threading.Tasks;
using BISC.Presentation.Infrastructure.Navigation;

namespace BISC.Presentation.Infrastructure.Services
{
    public interface INavigationService
    {
        void NavigateViewToRegion(string viewName, string regionName, BiscNavigationParameters navigationParameters =null);
        Task NavigateViewToGlobalRegion(string viewName, BiscNavigationParameters navigationParameters = null,string idOfDialogHost=null);
        void TryNavigateToWaitingRegion(string regionId);
        void DeactivateRegion(string regionId);
        void ActivateRegion(string regionName);
        void DisposeRegionViewModel(string regionName);
        void OpenInWindow(string viewName,string windowHeader, BiscNavigationParameters navigationParameters = null);

    }
}