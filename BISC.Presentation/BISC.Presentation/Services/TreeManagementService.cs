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
   public class TreeManagementService: ITreeManagementService
    {
       

        private readonly IMainTreeViewModel _mainTreeViewModel;
        private readonly INavigationService _navigationService;
        private Dictionary<Guid, Tuple<TreeItemIdentifier, ITreeItemViewModel>> _mainTreeViewModels
            =new Dictionary<Guid, Tuple<TreeItemIdentifier, ITreeItemViewModel>>();
    

    
        public TreeManagementService(IMainTreeViewModel mainTreeViewModel,INavigationService navigationService)
        {
            _mainTreeViewModel = mainTreeViewModel;
            _navigationService = navigationService;
        }

        public TreeItemIdentifier AddTreeItem(BiscNavigationParameters parameters, string viewName, Guid? parentId)
        {
            var newTreeItemGuid = Guid.NewGuid();
            TreeItemIdentifier treeItemIdentifier = new TreeItemIdentifier(parentId, newTreeItemGuid);
            TreeItemViewModel newItemViewModel=new TreeItemViewModel();
            newItemViewModel.DynamicRegionId = newTreeItemGuid;

            parameters.AddParameterByName(TreeItemIdentifier.Key,treeItemIdentifier);
            if (parentId != null)
            {
                if (_mainTreeViewModels.ContainsKey(parentId.Value))
                {
                    _mainTreeViewModels[parentId.Value].Item2.ChildItemViewModels.Add(newItemViewModel);
                }
            }
            else
            {
                _mainTreeViewModel.ChildItemViewModels.Add(newItemViewModel);
            }
            _mainTreeViewModels.Add(newTreeItemGuid,new Tuple<TreeItemIdentifier, ITreeItemViewModel>(treeItemIdentifier,newItemViewModel));
            _navigationService.NavigateViewToRegion(viewName, newTreeItemGuid.ToString(), parameters);
            return treeItemIdentifier;
        }

        public void DeleteTreeItem(TreeItemIdentifier treeItemId)
        {
            if(treeItemId.ItemId==null)return;
            if (treeItemId.ParentId == null)
            {
                _mainTreeViewModel.ChildItemViewModels.Remove(_mainTreeViewModels[treeItemId.ItemId.Value].Item2);
            }
            else
            {
                _mainTreeViewModels[treeItemId.ParentId.Value].Item2.ChildItemViewModels
                    .Remove(_mainTreeViewModels[treeItemId.ItemId.Value].Item2);

            }
        }
    }
}
