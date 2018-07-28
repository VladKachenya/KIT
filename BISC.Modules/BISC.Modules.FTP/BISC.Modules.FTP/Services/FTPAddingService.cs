using BISC.Modules.FTP.Infrastructure.Keys;
using BISC.Modules.FTP.Infrastructure.Serviсes;
using BISC.Presentation.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.FTP.Services
{
    public class FTPAddingService : IFTPAddingServise
    {
        private INavigationService _navigationService;

        public FTPAddingService(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public async void OpenFTPServiceView()
        {
            await _navigationService.NavigateViewToGlobalRegion(FTPKeys.FTPServiceViewKey);
        }

    }
}
