using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BISC.Infrastructure.Global.Services;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Events;
using BISC.Presentation.Infrastructure.Factories;

namespace BISC.Presentation.ViewModels
{
    public class NotificationBarViewModel : NavigationViewModelBase
    {
        private readonly IGlobalEventsService _globalEventsService;
        private bool _isNotificationsExpanded;
        private GridLength _expandableRowHeight;

        public NotificationBarViewModel(IGlobalEventsService globalEventsService,ICommandFactory commandFactory)
        {
            _globalEventsService = globalEventsService;
            ExpandedChangeCommand = commandFactory.CreatePresentationCommand(OnExpandedChange);
        }

        private void OnExpandedChange()
        {
            IsNotificationsExpanded = !IsNotificationsExpanded;
        }


        public bool IsNotificationsExpanded
        {
            get => _isNotificationsExpanded;
            set
            {
                SetProperty(ref _isNotificationsExpanded, value);
                _globalEventsService.SendMessage(new NotificationBarExpandEvent(value));
             
            }
        }
        public ICommand ExpandedChangeCommand { get; }
    }
}