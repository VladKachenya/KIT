using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Interfaces;
using BISC.Presentation.Views;

namespace BISC.Presentation.ViewModels.Tab
{
    public class TabHostViewModel :ViewModelBase, ITabHostViewModel
    {
        private ITabViewModel _activeTabViewModel;

        public TabHostViewModel()
        {
            TabViewModels = new ObservableCollection<ITabViewModel>();
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
