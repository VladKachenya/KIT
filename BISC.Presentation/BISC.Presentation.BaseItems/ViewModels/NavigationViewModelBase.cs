using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;
using BISC.Infrastructure.Global.Services;
using BISC.Presentation.BaseItems.ViewModels.Behaviors;
using BISC.Presentation.Infrastructure.Navigation;
using Prism.Regions;
using Prism;
using BISC.Presentation.BaseItems.Events;
using BISC.Presentation.Infrastructure.HelperEntities;

namespace BISC.Presentation.BaseItems.ViewModels
{
    public abstract class NavigationViewModelBase : ComplexViewModelBase, INavigationAware, IActiveAware
    {
        /// <summary>
        /// Global event service. Can be null.
        /// </summary>
        private readonly IGlobalEventsService _globalEventsService;
        private bool _isActive;
        private bool _isNavigatedTo = false;
        private bool _isReportWarning;
        private Guid? _parientTreeItemId;

        protected NavigationViewModelBase(IGlobalEventsService globalEventsService)
        {
            _globalEventsService = globalEventsService;
            BlockViewModelBehavior = new BlockViewModelBehavior();
            WarningsCollection = new ObservableCollection<string>();
        }

        public bool IsReportWarning
        {
            get => _isReportWarning;
            set => SetProperty(ref _isReportWarning, value);
        }
        public ObservableCollection<string> WarningsCollection { get; }


        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (!_isNavigatedTo) return;
                if (_isActive == value) return;
                _isActive = value;
                OnPropertyChanged();
                if (value)
                {
                    try
                    {
                        OnActivate();
                    }
                    catch (Exception e)
                    {
                        // ignored
                    }
                }
                else
                {
                    try
                    {
                        OnDeactivate();
                    }
                    catch (Exception e)
                    {
                        // ignored
                    }
                }
                if (Dispatcher.CurrentDispatcher == Application.Current.Dispatcher)
                {
                    IsActiveChanged?.Invoke(this, null);
                }
                else
                {
                    Application.Current.Dispatcher.Invoke(
                        () => IsActiveChanged?.Invoke(this, null)
                    );
                }
            }
        }

        public virtual void OnActivate()
        {
            if (_parientTreeItemId.HasValue)
            {
                _globalEventsService?.SendMessage(new NavigationViewModelActiveEvent(_parientTreeItemId.Value));
            }
        }
        public virtual void OnDeactivate()
        {

        }
        public event EventHandler IsActiveChanged;

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return IsNavigationTarget(BiscNavigationContext.FromNavigationContext(navigationContext));
        }

        protected virtual bool IsNavigationTarget(BiscNavigationContext navigationContext)
        {
            return true;
        }

        void INavigationAware.OnNavigatedFrom(NavigationContext navigationContext)
        {
            OnNavigatedFrom(BiscNavigationContext.FromNavigationContext(navigationContext));
            _isNavigatedTo = false;
        }
        protected virtual void OnNavigatedFrom(BiscNavigationContext navigationContext)
        {

        }
        void INavigationAware.OnNavigatedTo(NavigationContext navigationContext)
        {
            _isNavigatedTo = true;
            OnNavigatedTo(BiscNavigationContext.FromNavigationContext(navigationContext));
        }
        protected virtual void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            var treeItemIdentifier = navigationContext.BiscNavigationParameters
                .GetParameterByName<UiEntityIdentifier>(UiEntityIdentifier.Key);
            while (treeItemIdentifier != null)
            {
                if (treeItemIdentifier.IsTreeItem)
                {
                    _parientTreeItemId = treeItemIdentifier.ItemId;
                    break;
                }

                treeItemIdentifier = treeItemIdentifier.ParenUiEntityIdentifier;
            }
        }

    }
}