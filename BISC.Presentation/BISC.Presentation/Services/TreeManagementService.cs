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
        private Dictionary<Guid, ITreeItemViewModel> _mainTreeViewModels=new Dictionary<Guid, ITreeItemViewModel>();
        private readonly string _parentTreeItemKey = "ParentTreeItem";

        public TreeManagementService(IMainTreeViewModel mainTreeViewModel,INavigationService navigationService)
        {
            _mainTreeViewModel = mainTreeViewModel;
            _navigationService = navigationService;
        }

        public void AddTreeItem(BiscNavigationParameters parameters, string viewName, Guid? parentId)
        {
            var newTreeItemGuid = Guid.NewGuid();
            TreeItemViewModel newItemViewModel=new TreeItemViewModel();
            newItemViewModel.DynamicRegionId = newTreeItemGuid;


            if (parentId != null)
            {
                if (_mainTreeViewModels.ContainsKey(parentId.Value))
                {
                    _mainTreeViewModels[parentId.Value].ChildItemViewModels.Add(newItemViewModel);
                }
            }
            else
            {
                _mainTreeViewModel.ChildItemViewModels.Add(newItemViewModel);
            }
            _mainTreeViewModels.Add(newTreeItemGuid,newItemViewModel);
            _navigationService.NavigateViewToRegion(viewName, newTreeItemGuid.ToString(), parameters);

        }

        public void DeleteTreeItem(Guid treeItemId)
        {
            throw new NotImplementedException();
        }
    }
}
