using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Presentation.BaseItems.ViewModels;
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
        private Dictionary<Guid, Tuple<TreeItemIdentifier, ITreeItemViewModel>> _mainTreeViewModels
            = new Dictionary<Guid, Tuple<TreeItemIdentifier, ITreeItemViewModel>>();



        public TreeManagementService(IMainTreeViewModel mainTreeViewModel, INavigationService navigationService,
            ITabManagementService tabManagementService)
        {
            _mainTreeViewModel = mainTreeViewModel;
            _navigationService = navigationService;
            _tabManagementService = tabManagementService;
        }

        public TreeItemIdentifier AddTreeItem(BiscNavigationParameters parameters, string viewName, TreeItemIdentifier parentTreeItemIdentifier,string tag=null)
        {
            var newTreeItemGuid = Guid.NewGuid();
            TreeItemIdentifier treeItemIdentifier = new TreeItemIdentifier(newTreeItemGuid, parentTreeItemIdentifier,tag);
            TreeItemViewModel newItemViewModel = new TreeItemViewModel();
            newItemViewModel.DynamicRegionId = newTreeItemGuid;

            parameters.AddParameterByName(TreeItemIdentifier.Key, treeItemIdentifier);
            if (parentTreeItemIdentifier != null && parentTreeItemIdentifier.ItemId.HasValue)
            {
                if (_mainTreeViewModels.ContainsKey(parentTreeItemIdentifier.ItemId.Value))
                {
                    _mainTreeViewModels[parentTreeItemIdentifier.ItemId.Value].Item2.ChildItemViewModels.Add(newItemViewModel);
                }
            }
            else
            {
                _mainTreeViewModel.ChildItemViewModels.Add(newItemViewModel);
            }
            _mainTreeViewModels.Add(newTreeItemGuid, new Tuple<TreeItemIdentifier, ITreeItemViewModel>(treeItemIdentifier, newItemViewModel));
            _navigationService.NavigateViewToRegion(viewName, newTreeItemGuid.ToString(), parameters);
            return treeItemIdentifier;
        }


        private bool GetIsChildRecursive(TreeItemIdentifier child, TreeItemIdentifier parent)
        {
            if (child.ParenTreeItemIdentifier == null) return false;

            if (parent.ItemId != null && child.ParenTreeItemIdentifier.ItemId != null &&
                child.ParenTreeItemIdentifier.ItemId.Value == parent.ItemId.Value)
            {
                return true;
            }
            return GetIsChildRecursive(child.ParenTreeItemIdentifier, parent);
        }

        public void DeleteTreeItem(TreeItemIdentifier treeItemId)
        {
            if (treeItemId.ItemId == null) return;
            if (!_mainTreeViewModels.ContainsKey(treeItemId.ItemId.Value)) return;
            var parent = _mainTreeViewModels.Values.First((tuple => tuple.Item1.ItemId.Value == treeItemId.ItemId.Value)).Item1;
            var treeItemIdsToRemove=new List<TreeItemIdentifier>();

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


            if (treeItemId.ParenTreeItemIdentifier?.ItemId == null)
            {
                _mainTreeViewModel.ChildItemViewModels.Remove(_mainTreeViewModels[treeItemId.ItemId.Value].Item2);
            }
            else
            {
                _mainTreeViewModels[treeItemId.ParenTreeItemIdentifier.ItemId.Value].Item2.ChildItemViewModels
                    .Remove(_mainTreeViewModels[treeItemId.ItemId.Value].Item2);
            }
            _mainTreeViewModels.Remove(treeItemId.ItemId.Value);

            _navigationService.DisposeRegionViewModel(treeItemId.ItemId.Value.ToString());

        }

	    public TreeItemIdentifier GetDeviceTreeItem(string deviceName)
	    {
		    var deviceTreeItem = _mainTreeViewModels.FirstOrDefault((pair => pair.Value.Item1.Tag == deviceName));
		    return deviceTreeItem.Value.Item1;
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
