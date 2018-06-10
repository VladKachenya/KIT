using System.Collections.Generic;
using BISC.Presentation.Infrastructure.Parameters;

namespace BISC.Presentation.Infrastructure.Services
{
   
    public interface ITabManagementService
    {
        void NavigateToTab(string viewName,List<NavigationParameter> navigationParameters,object owner);
        void CloseTabs(object owner);
    }
}