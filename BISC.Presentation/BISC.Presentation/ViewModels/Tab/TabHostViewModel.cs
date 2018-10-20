using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Services;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Events;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Interfaces;
using BISC.Presentation.Views;
using CommonServiceLocator;
using Prism.Regions;

namespace BISC.Presentation.ViewModels.Tab
{
    public class TabHostViewModel : ViewModelBase, ITabHostViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IGlobalEventsService _globalEventsService;
        private readonly ILoggingService _loggingService;
        private ITabViewModel _activeTabViewModel;

        public TabHostViewModel(INavigationService navigationService, IGlobalEventsService globalEventsService,ILoggingService loggingService)
        {
            _navigationService = navigationService;
            _globalEventsService = globalEventsService;
            _loggingService = loggingService;
            TabViewModels = new ObservableCollection<ITabViewModel>();
            TabViewModels.CollectionChanged += TabViewModels_CollectionChanged;
        }

        private void TabViewModels_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                var regionName = (e.OldItems[0] as ITabViewModel)?.TabRegionName;
                _globalEventsService.SendMessage(new RegionDisposingEvent(regionName));
            }
        }

        public ObservableCollection<ITabViewModel> TabViewModels { get; }

        public ITabViewModel ActiveTabViewModel
        {
            get => _activeTabViewModel;
            set
            {
                if(_activeTabViewModel!=null)
                _navigationService.DeactivateRegion(_activeTabViewModel.TabRegionName);
                _activeTabViewModel = value;
                _navigationService.ActivateRegion(value.TabRegionName);
                _loggingService.LogUserAction($"Пользователь переместился на вкладку: {value.TabHeader}");
                OnPropertyChanged();
            }
        }


    }
}
