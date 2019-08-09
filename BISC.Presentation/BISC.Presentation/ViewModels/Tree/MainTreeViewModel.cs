using BISC.Infrastructure.Global.Services;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Interfaces.Tree;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BISC.Presentation.BaseItems.Events;
using BISC.Presentation.Infrastructure.Keys;

namespace BISC.Presentation.ViewModels.Tree
{
    public class MainTreeViewModel : ViewModelBase, IMainTreeViewModel
    {
        private readonly ISaveCheckingService _saveCheckingService;
        private readonly IProjectService _projectService;
        private readonly IGlobalEventsService _globalEventsService;
        private readonly ObservableCollection<ITreeItemViewModel> _childItemViewModels;

        public MainTreeViewModel(ISaveCheckingService saveCheckingService, ICommandFactory commandFactory,
            IProjectService projectService, IGlobalEventsService globalEventsService)
        {
            _saveCheckingService = saveCheckingService;
            _projectService = projectService;
            _globalEventsService = globalEventsService;
            _saveCheckingService.AddSaveCheckingEntity(new SaveCheckingEntity(ChangeTracker, "Дерево устройств проекта", null, new Guid(KeysForNavigation.AppGuids.NullGuid)));
            _globalEventsService.Subscribe<NavigationViewModelActiveEvent>(OnNavigationViewModelActive);

            SetProperty(ref _childItemViewModels, new ObservableCollection<ITreeItemViewModel>(), false, nameof(ChildItemViewModels));
        }

        private async Task SaveAsync()
        {
            _projectService.SaveCurrentProject();
        }


        public ObservableCollection<ITreeItemViewModel> ChildItemViewModels => _childItemViewModels;

        #region override ViewModelBase
        protected override void OnDisposing()
        {
            _globalEventsService.Unsubscribe<NavigationViewModelActiveEvent>(OnNavigationViewModelActive);
            base.OnDisposing();
        }
        #endregion

        #region private methods

        private void OnNavigationViewModelActive(NavigationViewModelActiveEvent navigationViewModelActiveEvent)
        {
            if (navigationViewModelActiveEvent != null)
            {
                SelectTreeItem(_childItemViewModels, navigationViewModelActiveEvent.TreeItemIdentifier);
            }
        }

        private bool SelectTreeItem(ObservableCollection<ITreeItemViewModel> itemViewModels, Guid DynamicRegionId)
        {
            var resItem = itemViewModels.SingleOrDefault(ti => ti.DynamicRegionId == DynamicRegionId);
            if (resItem == null)
            {
                foreach (var treeItemViewModel in itemViewModels)
                {
                    if (SelectTreeItem(treeItemViewModel.ChildItemViewModels, DynamicRegionId))
                    {
                        treeItemViewModel.IsExpanded = true;
                        return true;
                    }
                }
            }
            else
            {
                resItem.IsSelected = true;
                return true;
            }
            return false;
        }

        #endregion
    }
}
