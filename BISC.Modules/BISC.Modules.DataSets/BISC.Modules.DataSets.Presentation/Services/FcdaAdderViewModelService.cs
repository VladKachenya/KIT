using BISC.Modules.DataSets.Infrastructure.Keys;
using BISC.Modules.DataSets.Infrastructure.ViewModels.Services;
using BISC.Presentation.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.DataSets.Presentation.Services
{
    public class FcdaAdderViewModelService : IFcdaAdderViewModelService
    {
        private INavigationService _navigationService;

        public FcdaAdderViewModelService(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public async void OpenFcdaAdderView()
        {
            await _navigationService.NavigateViewToGlobalRegion(DatasetKeys.DatasetViewModelKeys.FcdaAdderViewModel);
        }
    }
}
