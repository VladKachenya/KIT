using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Logging.Infrastructure.Events;
using BISC.Modules.Logging.Infrastructure.ViewModels;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Events;
using BISC.Presentation.Infrastructure.Factories;

namespace BISC.Modules.Logging
{
    public class NotificationBarViewModel : NavigationViewModelBase
    {
        private readonly IGlobalEventsService _globalEventsService;
        private bool _isNotificationsExpanded;
        private GridLength _expandableRowHeight;
        private string _lastMessage;
        private Timer _lastMessageHidingTimer;



        public NotificationBarViewModel(IGlobalEventsService globalEventsService,ICommandFactory commandFactory)
            : base(null)
        {
            _globalEventsService = globalEventsService;
            ExpandedChangeCommand = commandFactory.CreatePresentationCommand(OnExpandedChange);
            _globalEventsService.Subscribe<LogEvent>(OnLogEvent);
            LogMessages=new ObservableCollection<LogMessageViewModel>();
            _lastMessageHidingTimer = new Timer(HideLastMessage, null, Timeout.Infinite, Timeout.Infinite);

        }

        private void HideLastMessage(object state)
        {
            LastMessage=String.Empty;
        }

        private void OnLogEvent(LogEvent logEvent)
        {
            LastMessage = logEvent.Message.Message;
            _lastMessageHidingTimer.Change(15000, Timeout.Infinite);
            Application.Current.Dispatcher.BeginInvoke(new Action((() =>
            {
                LogMessages.Insert(0, new LogMessageViewModel(logEvent.Message));

            })));
        }

        private void OnExpandedChange()
        {
            IsNotificationsExpanded = !IsNotificationsExpanded;
        }

        public ObservableCollection<LogMessageViewModel> LogMessages { get; }

        public bool IsNotificationsExpanded
        {
            get => _isNotificationsExpanded;
            set
            {
                SetProperty(ref _isNotificationsExpanded, value);
                _globalEventsService.SendMessage(new NotificationBarExpandEvent(value));
             
            }
        }

        public string LastMessage
        {
            get => _lastMessage;
            set => SetProperty(ref _lastMessage , value,true);
        }

        public ICommand ExpandedChangeCommand { get; }
    }
}