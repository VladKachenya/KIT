using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Infrastructure.Tree;
using BISC.Presentation.Interfaces.Tree;
using BISC.Presentation.ViewModels.Tree;

namespace BISC.Presentation.Services
{
    public class TreeManagementService : ITreeManagementService
    {
        private readonly IMainTreeViewModel _mainTreeViewModel;
        private readonly INavigationService _navigationService;
        private readonly ITabManagementService _tabManagementService;
        private Dictionary<Guid, Tuple<UiEntityIdentifier, ITreeItemViewModel>> _mainTreeViewModels
            = new Dictionary<Guid, Tuple<UiEntityIdentifier, ITreeItemViewModel>>();



        public TreeManagementService(IMainTreeViewModel mainTreeViewModel, INavigationService navigationService,
            ITabManagementService tabManagementService)
        {
            _mainTreeViewModel = mainTreeViewModel;
            _navigationService = navigationService;
            _tabManagementService = tabManagementService;
        }

        public UiEntityIdentifier AddTreeItem(BiscNavigationParameters parameters, string viewName, UiEntityIdentifier parentUiEntityIdentifier, string tag = null,int? index=null)
        {
            var newTreeItemGuid = Guid.NewGuid();
            UiEntityIdentifier uiEntityIdentifier = new UiEntityIdentifier(newTreeItemGuid, parentUiEntityIdentifier, tag);
            TreeItemViewModel newItemViewModel = new TreeItemViewModel();
            newItemViewModel.DynamicRegionId = newTreeItemGuid;

            parameters.AddParameterByName(UiEntityIdentifier.Key, uiEntityIdentifier);
            if (parentUiEntityIdentifier != null && parentUiEntityIdentifier.ItemId.HasValue)
            {
                if (_mainTreeViewModels.ContainsKey(parentUiEntityIdentifier.ItemId.Value))
                {
                    _mainTreeViewModels[parentUiEntityIdentifier.ItemId.Value].Item2.ChildItemViewModels.Add(newItemViewModel);
                }
            }
            else
            {
                if ((index == null)||(_mainTreeViewModel.ChildItemViewModels.Count-1<index))
                {
                    _mainTreeViewModel.ChildItemViewModels.Add(newItemViewModel);
                }
                else
                {
                    _mainTreeViewModel.ChildItemViewModels.Insert(index.Value,newItemViewModel);
                }
            }
            _mainTreeViewModels.Add(newTreeItemGuid, new Tuple<UiEntityIdentifier, ITreeItemViewModel>(uiEntityIdentifier, newItemViewModel));
            _navigationService.NavigateViewToRegion(viewName, newTreeItemGuid.ToString(), parameters);
            return uiEntityIdentifier;
        }

        private bool GetIsChildRecursive(UiEntityIdentifier child, UiEntityIdentifier parent)
        {
            if (child.ParenUiEntityIdentifier == null) return false;

            if (parent.ItemId != null && child.ParenUiEntityIdentifier.ItemId != null &&
                child.ParenUiEntityIdentifier.ItemId.Value == parent.ItemId.Value)
            {
                return true;
            }
            return GetIsChildRecursive(child.ParenUiEntityIdentifier, parent);
        }

        public void DeleteTreeItem(UiEntityIdentifier uiEntityId)
        {
            if (uiEntityId.ItemId == null) return;
            if (!_mainTreeViewModels.ContainsKey(uiEntityId.ItemId.Value)) return;
            var parent = _mainTreeViewModels.Values.First((tuple => tuple.Item1.ItemId.Value == uiEntityId.ItemId.Value)).Item1;
            var treeItemIdsToRemove = new List<UiEntityIdentifier>();

            foreach (var mainTreeViewModel in _mainTreeViewModels)
            {
                if (GetIsChildRecursive(mainTreeViewModel.Value.Item1, parent))
                {
                    treeItemIdsToRemove.Add(mainTreeViewModel.Value.Item1);
                }
            }
            foreach (var treeItemIdentifierToRemove in treeItemIdsToRemove)
            {
                _mainTreeViewModels.Remove(treeItemIdentifierToRemove.ItemId.Value);
                _navigationService.DisposeRegionViewModel(treeItemIdentifierToRemove.ItemId.Value.ToString());
            }


            if (uiEntityId.ParenUiEntityIdentifier?.ItemId == null)
            {
                _mainTreeViewModel.ChildItemViewModels.Remove(_mainTreeViewModels[uiEntityId.ItemId.Value].Item2);
            }
            else
            {
                _mainTreeViewModels[uiEntityId.ParenUiEntityIdentifier.ItemId.Value].Item2.ChildItemViewModels
                    .Remove(_mainTreeViewModels[uiEntityId.ItemId.Value].Item2);
            }
            _mainTreeViewModels.Remove(uiEntityId.ItemId.Value);

            _navigationService.DisposeRegionViewModel(uiEntityId.ItemId.Value.ToString());

        }

        public int GetTreeItemIndex(UiEntityIdentifier uiEntityId)
        {
            if (!_mainTreeViewModels.ContainsKey(uiEntityId.ItemId.Value)) return -1;
            return _mainTreeViewModel.ChildItemViewModels.IndexOf(_mainTreeViewModels[uiEntityId.ItemId.Value].Item2);
        }

        public UiEntityIdentifier GetDeviceTreeItem(Guid deviceGuid)
        {
            var deviceTreeItem = _mainTreeViewModels.FirstOrDefault((pair => pair.Value.Item1.Tag == deviceGuid.ToString()));
            return deviceTreeItem.Value?.Item1;
        }

        public void ClearMainTree()
        {
            while (_mainTreeViewModel.ChildItemViewModels.Count != 0)
            {
                var element = _mainTreeViewModel.ChildItemViewModels.First();
                var itemId = _mainTreeViewModels[element.DynamicRegionId].Item1;
                _tabManagementService.CloseTabWithChildren(itemId.ItemId.ToString());
                DeleteTreeItem(itemId);
            }
        }
    }
}
