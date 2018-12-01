using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Navigation;

namespace BISC.Modules.Gooses.Presentation.ViewModels.GooseControls
{
   public class GooseControlsConflictsViewModel:NavigationViewModelBase
    {
        private GooseControlsConflictContext _gooseControlsConflictContext;

        public GooseControlsConflictsViewModel()
        {
            GooseControlsCollectionInProject=new ObservableCollection<GooseControlViewModel>();
            GooseControlsollectionInDevice=new ObservableCollection<GooseControlViewModel>();
        }

        public ObservableCollection<GooseControlViewModel> GooseControlsollectionInDevice { get; }
        public ObservableCollection<GooseControlViewModel> GooseControlsCollectionInProject { get; }

        #region Overrides of NavigationViewModelBase

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            _gooseControlsConflictContext= navigationContext.BiscNavigationParameters.GetParameterByName<GooseControlsConflictContext>(
                GooseKeys.GoosePresentationKeys.GooseControlsConflictContext);
            GooseControlsollectionInDevice.Clear();
            GooseControlsCollectionInProject.Clear();

            foreach (var gooseControlViewModel in _gooseControlsConflictContext.GooseControlsollectionInDevice)
            {
                gooseControlViewModel.IsEditable = false;
                gooseControlViewModel.IsChanged = gooseControlViewModel.ChangeTracker.GetIsModifiedRecursive();
                GooseControlsollectionInDevice.Add(gooseControlViewModel);
            }
            foreach (var gooseControlViewModel in _gooseControlsConflictContext.GooseControlsCollectionInProject)
            {
                gooseControlViewModel.IsEditable = false;
                gooseControlViewModel.IsChanged = gooseControlViewModel.ChangeTracker.GetIsModifiedRecursive();
                GooseControlsCollectionInProject.Add(gooseControlViewModel);
            }
        }

        #endregion
    }
}
