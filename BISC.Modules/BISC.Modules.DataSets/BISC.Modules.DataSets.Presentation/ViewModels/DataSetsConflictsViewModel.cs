using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.DataSets.Infrastructure.Keys;
using BISC.Modules.DataSets.Infrastructure.ViewModels;
using BISC.Modules.DataSets.Presentation.ViewModels.Helpers;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Navigation;

namespace BISC.Modules.DataSets.Presentation.ViewModels
{
   public class DataSetsConflictsViewModel:NavigationViewModelBase
    {
        private DatasetsConflictContext _datasetsConflictContext;

        public DataSetsConflictsViewModel()
        {
            DataSetsCollectionInProject=new ObservableCollection<IDataSetViewModel>();
            DataSetsCollectionInDevice = new ObservableCollection<IDataSetViewModel>();
        }
        public ObservableCollection<IDataSetViewModel> DataSetsCollectionInDevice { get; }
        public ObservableCollection<IDataSetViewModel> DataSetsCollectionInProject { get; }

        #region Overrides of NavigationViewModelBase

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            _datasetsConflictContext =
                navigationContext.BiscNavigationParameters.GetParameterByName<DatasetsConflictContext>(DatasetKeys
                    .DatasetViewModelKeys.DatasetsConflictContextKey);
            DataSetsCollectionInDevice.Clear();
            foreach (var dataSetViewModel in _datasetsConflictContext.DatasetViewModelsInDevice)
            {
                dataSetViewModel.IsChanged = dataSetViewModel.ChangeTracker.GetIsModifiedRecursive();
                DataSetsCollectionInDevice.Add(dataSetViewModel);
            }
            DataSetsCollectionInProject.Clear();
            foreach (var dataSetViewModel in _datasetsConflictContext.DatasetViewModelsInProject)
            {
                dataSetViewModel.IsChanged = dataSetViewModel.ChangeTracker.GetIsModifiedRecursive();
                DataSetsCollectionInProject.Add(dataSetViewModel);
            }
        }

        #endregion
    }
}
