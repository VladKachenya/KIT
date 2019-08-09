using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Services;
using BISC.Interfaces;
using BISC.Presentation.BaseItems.Events;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.BaseItems.ViewModels.Behaviors;
using BISC.Presentation.Infrastructure.Events;
using BISC.Presentation.Infrastructure.Keys;
using BISC.Presentation.Infrastructure.Services;
using Prism.Commands;
using Prism.Events;

namespace BISC.ViewModel
{
    public class ShellViewModel : NavigationViewModelBase, IShellViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly ISaveCheckingService _saveCheckingService;
        private readonly IGlobalEventsService _globalEventsService;
        private readonly IInjectionContainer _container;
        private readonly ShellLoadedService _shellLoadedService;
        private bool _isLeftMenuEnabled;
        private string _applicationTitle;
        private GridLength _expanderRowHeight;
        private bool _isNotificationsExpanded;

        public ShellViewModel(
            INavigationService navigationService,
            ISaveCheckingService saveCheckingService,
            IGlobalEventsService globalEventsService,
            IInjectionContainer container,
            ShellLoadedService shellLoadedService)
            : base(null)
        {
            _navigationService = navigationService;
            _saveCheckingService = saveCheckingService;
            _globalEventsService = globalEventsService;
            _container = container;
            _shellLoadedService = shellLoadedService;
            ShellLoadedCommand = new DelegateCommand(OnShellLoaded);
            ShellClosingCommand = new DelegateCommand<object>(OnShellClosing);
            _globalEventsService.Subscribe<NotificationBarExpandEvent>(OnNotificationBarClosing);
            _globalEventsService.Subscribe<ShellBlockEvent>((x) =>
            {
                if (x.IsBlocked)
                {
                    if (x.Message != null)
                    {
                        BlockViewModelBehavior.SetBlock(x.Message, true);

                    }
                    else
                    {
                        BlockViewModelBehavior.SetBlock("Обновление", true);
                    }
                }
                else
                {
                    BlockViewModelBehavior.Unlock();
                }
            });
            BlockViewModelBehavior = new BlockViewModelBehavior();

        }


        private void OnNotificationBarClosing(NotificationBarExpandEvent obj)
        {
            if (obj.IsExpanded)
            {
                ExpanderRowHeight = new GridLength(200);

            }
            else
            {
                ExpanderRowHeight = GridLength.Auto;

            }
            IsNotificationsExpanded = obj.IsExpanded;
        }

        public bool IsNotificationsExpanded
        {
            get => _isNotificationsExpanded;
            set { SetProperty(ref _isNotificationsExpanded, value); }
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
            await OnClosing();
        }

        private async Task OnClosing()
        {
            await _container.ResolveType<IProjectManagementService>().SaveProjectAsync();
            Application.Current.Shutdown();
        }

        private void OnShellLoaded()
        {
            _globalEventsService.SendMessage(new ShellLoadedEvent());
            _shellLoadedService.OnShellLoadedAction?.Invoke();
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
