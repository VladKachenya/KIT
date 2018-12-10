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
    public class ConnectionPresentationViewAddingServise : IConnectionPresentationViewAddingServise
    {
        private INavigationService _navigationService;
        public ConnectionPresentationViewAddingServise(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public async void OpenPingsView()
        {
            await _navigationService.NavigateViewToGlobalRegion(ConnectionKeys.PingViewKey);
        }

        public async void OpenChangeIpNetworkCardView()
        {
            await _navigationService.NavigateViewToGlobalRegion(ConnectionKeys.ChangeIpNetworkCardViewKey);

        }
    }
}
