using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Presentation.Infrastructure.Events;
using BISC.Presentation.Infrastructure.Keys;
using BISC.Presentation.Infrastructure.Services;
using Prism.Events;

namespace BISC.Presentation.Services
{
   public class PresentationInitialization
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationService _navigationService;

        public PresentationInitialization(IEventAggregator eventAggregator, INavigationService navigationService)
        {

            _eventAggregator = eventAggregator;
            _navigationService = navigationService;
            _eventAggregator.GetEvent<ShellLoadedEvent>().Subscribe((args =>
            {
                _navigationService.NavigateViewToRegion(KeysForNavigation.ViewNames.MainTreeViewName,
                    KeysForNavigation.RegionNames.MainTreeRegionKey);
                _navigationService.NavigateViewToRegion(KeysForNavigation.ViewNames.MainTabHostViewName,
                    KeysForNavigation.RegionNames.MainTabHostRegionKey);
            }));

        }
    }
}
