using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Presentation.Services
{
    public class TabManagementService: ITabManagementService
    {
        
        public void NavigateToTab(string viewName, List<NavigationParameter> navigationParameters, object owner)
        {
            throw new NotImplementedException();
        }

        public void CloseTabs(object owner)
        {
            throw new NotImplementedException();
        }
    }
}
