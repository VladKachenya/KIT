using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Services;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Events;

namespace BISC.Presentation.ViewModels
{
   public class NotificationBarViewModel:NavigationViewModelBase
    {
        private readonly IGlobalEventsService _globalEventsService;
        private bool _isNotificationsExpanded;

        public NotificationBarViewModel(IGlobalEventsService globalEventsService)
        {
            _globalEventsService = globalEventsService;
        }

        public bool IsNotificationsExpanded
        {
            get => _isNotificationsExpanded;
            set
            {
                SetProperty(ref _isNotificationsExpanded , value);
                if (!value)
                {
                    _globalEventsService.SendMessage(new NotificationBarClosingEvent());
                }
            }
        }
    }
}
