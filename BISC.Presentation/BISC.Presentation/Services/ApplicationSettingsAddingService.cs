using BISC.Presentation.Infrastructure.Keys;
using BISC.Presentation.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Presentation.Services
{
    public class ApplicationSettingsAddingService : IApplicationSettingsAddingService
    {
        private INavigationService _navigationService;
        public ApplicationSettingsAddingService(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public async void OpenApplicatoinSettingsView()
        {
             await _navigationService.NavigateViewToGlobalRegion(KeysForNavigation.ViewNames.ApplicationSettingsViewName);
        }
    }
}
