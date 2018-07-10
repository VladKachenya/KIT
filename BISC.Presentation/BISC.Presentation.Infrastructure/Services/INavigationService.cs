using System.Collections.Generic;
using BISC.Presentation.Infrastructure.Parameters;

namespace BISC.Presentation.Infrastructure.Services
{
    public interface INavigationService
    {
        void NavigateViewToRegion(string viewName, string regionName, List<NavigationParameter> navigationParameters=null);
        void NavigateViewToGlobalRegion(string viewName, List<NavigationParameter> navigationParameters = null);

    }
}