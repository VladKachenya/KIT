using BISC.Modules.Connection.Infrastructure.Keys;
using BISC.Modules.Connection.Presentation.Interfaces.Services;
using BISC.Presentation.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Connection.Presentation.Services
{
    public class PingAddingServise : IPingAddingServise
    {
        private INavigationService _navigationService;
        public PingAddingServise(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public void OpenPingsView()
        {
            _navigationService.NavigateViewToGlobalRegion(ConnectionKeys.PingAddingViewKey);
        }
    }
}
