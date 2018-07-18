using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Tree;
using BISC.Presentation.Interfaces.Tree;

namespace BISC.Presentation.ViewModels.Tree
{
  public  class TreeItemViewModel:ViewModelBase,ITreeItemViewModel
    {
        public TreeItemViewModel()
        {
            ChildItemViewModels=new ObservableCollection<ITreeItemViewModel>();
        }
        private Guid _dynamicRegionId;
        private string _viewName;

        public ObservableCollection<ITreeItemViewModel> ChildItemViewModels { get; }

        public Guid DynamicRegionId
        {
            get => _dynamicRegionId;
            set
            {
                _dynamicRegionId = value;
                OnPropertyChanged();
            }
        }

    
    }
}
