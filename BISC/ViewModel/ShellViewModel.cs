using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BISC.Infrastructure.Global.Services;
using BISC.Interfaces;
using BISC.Presentation.BaseItems.Events;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Events;
using BISC.Presentation.Infrastructure.Keys;
using BISC.Presentation.Infrastructure.Services;
using Prism.Commands;
using Prism.Events;

namespace BISC.ViewModel
{
   public class ShellViewModel:ViewModelBase, IShellViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationService _navigationService;
        private readonly IProjectService _projectService;
        private readonly ISaveCheckingService _saveCheckingService;
        private readonly IGlobalEventsService _globalEventsService;
        private bool _isLeftMenuEnabled;
        private string _applicationTitle;
        private GridLength _expanderRowHeight;
        private bool _isNotificationsExpanded;

        public ShellViewModel(IEventAggregator eventAggregator,INavigationService navigationService,
            IProjectService projectService, ISaveCheckingService saveCheckingService,IGlobalEventsService globalEventsService)
        {
           
               _eventAggregator = eventAggregator;
            _navigationService = navigationService;
            _projectService = projectService;
            _saveCheckingService = saveCheckingService;
            _globalEventsService = globalEventsService;
            ShellLoadedCommand = new DelegateCommand(OnShellLoaded);
            ShellClosingCommand=new DelegateCommand<object>(OnShellClosing);
            _globalEventsService.Subscribe<NotificationBarExpandEvent>(OnNotificationBarClosing);
        }

        private void OnNotificationBarClosing(NotificationBarExpandEvent obj)
        {
            ExpanderRowHeight = GridLength.Auto;
            IsNotificationsExpanded = obj.IsExpanded;
        }

        public bool IsNotificationsExpanded
        {
            get => _isNotificationsExpanded;
            set { SetProperty(ref _isNotificationsExpanded,value); }
        }

        public GridLength ExpanderRowHeight
        {
            get => _expanderRowHeight;
            set
            {
                SetProperty(ref _expanderRowHeight, value);
            }
        }


        private async void OnShellClosing(object obj)
        {
            (obj as CancelEventArgs).Cancel = true;
            OnClosing();
        }

        private async Task OnClosing()
        {
            var result =await _saveCheckingService.SaveAllUnsavedEntities();
            if (!result.IsCancelled)
            {
               Application.Current.Shutdown();
            }
        }

        private void OnShellLoaded()
        {
            _eventAggregator.GetEvent<ShellLoadedEvent>().Publish(new ShellLoadedEventArgs());
            ApplicationTitle = $"Bemn Intellectual Substation Control (Текущий проект: {_projectService.GetCurrentProjectPath(false)})";

        }


        public ICommand ShellLoadedCommand { get; }
        public ICommand ShellClosingCommand { get; }

  

        public string ApplicationTitle
        {
            get => _applicationTitle;
            set
            {
               
                SetProperty(ref _applicationTitle, value);
            }
        }
    }
}
