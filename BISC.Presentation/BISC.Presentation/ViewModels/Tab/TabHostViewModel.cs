using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BISC.Infrastructure.Global.IoC;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Interfaces;
using BISC.Presentation.Views;
using CommonServiceLocator;
using Prism.Regions;

namespace BISC.Presentation.ViewModels.Tab
{
    public class TabHostViewModel :ViewModelBase, ITabHostViewModel
    {
        private readonly INavigationService _navigationService;
        private ITabViewModel _activeTabViewModel;

        public TabHostViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            TabViewModels = new ObservableCollection<ITabViewModel>();
            TabViewModels.CollectionChanged += TabViewModels_CollectionChanged;
        }

        private void TabViewModels_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                var regionName = (e.OldItems[0] as ITabViewModel)?.TabRegionName;
                _navigationService.NavigateFromRegion(regionName);
            }
        }

        public ObservableCollection<ITabViewModel> TabViewModels { get; }

        public ITabViewModel ActiveTabViewModel
        {
            get => _activeTabViewModel;
            set
            {
                _activeTabViewModel = value;
                OnPropertyChanged();
            }
        }
    }
}
