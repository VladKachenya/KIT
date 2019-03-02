using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Presentation.Docking;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Interfaces;
using BISC.Presentation.ViewModels.Tab;

namespace BISC.Presentation.Services
{
    public class TabManagementService: ITabManagementService
    {
        private readonly ITabHostViewModel _tabHostViewModel;
        private readonly INavigationService _navigationService;
        private readonly Func<ITabViewModel> _tabViewModelFactory;
        private readonly ISaveCheckingService _saveCheckingService;
        private readonly Dictionary<UiEntityIdentifier,ITabViewModel> _tabViewModelsDictionary=new Dictionary<UiEntityIdentifier, ITabViewModel>();
        public TabManagementService(ITabHostViewModel tabHostViewModel,INavigationService navigationService,Func<ITabViewModel> tabViewModelFactory,ISaveCheckingService saveCheckingService)
        {
            _tabHostViewModel = tabHostViewModel;
            _navigationService = navigationService;
            _tabViewModelFactory = tabViewModelFactory;
            _saveCheckingService = saveCheckingService;
        }
        public void NavigateToTab(string viewName, BiscNavigationParameters navigationParameters, string header, UiEntityIdentifier owner)
        {
            var newTab = _tabViewModelFactory.Invoke();
            Guid newTabId = Guid.NewGuid();
            newTab.TabHeader = header;
            newTab.TabRegionName = newTabId.ToString();
            if (_tabViewModelsDictionary.ContainsKey(owner))
            {
                _tabHostViewModel.ActiveTabViewModel = _tabViewModelsDictionary[owner];
               return;
            }
            else
            {
                _tabViewModelsDictionary.Add(owner,newTab);
                _tabHostViewModel.ActiveTabViewModel = newTab;
            }

            if (navigationParameters == null)
            {
                navigationParameters=new BiscNavigationParameters();
            }
                
            navigationParameters.AddParameterByName(UiEntityIdentifier.Key, new UiEntityIdentifier(newTabId,owner));

            _tabHostViewModel.TabViewModels.Insert(0,newTab);
            _navigationService.NavigateViewToRegion(viewName,newTabId.ToString(),navigationParameters);
        }

        public void CloseTab(UiEntityIdentifier owner)
        {
            if (_tabViewModelsDictionary.ContainsKey(owner))
            {
                _tabViewModelsDictionary[owner].Dispose();
                _tabHostViewModel.TabViewModels.Remove(_tabViewModelsDictionary[owner]);
                _tabViewModelsDictionary.Remove(owner);
                _saveCheckingService.RemoveSaveCheckingEntityByOwner(owner.ItemId.ToString());
            }
        }

        public void CloseTab(string regioId)
        {
            var tabVm = _tabViewModelsDictionary.FirstOrDefault((pair => pair.Value.TabRegionName.ToString() == regioId));
            if (tabVm.Value!=null)
            {
                tabVm.Value.Dispose();
                _tabHostViewModel.TabViewModels.Remove(tabVm.Value);
                _tabViewModelsDictionary.Remove(tabVm.Key);
                _saveCheckingService.RemoveSaveCheckingEntityByOwner(regioId);
            }
        }

        public void CloseTabWithChildren(string regionId)
        {
            var idsToRemove=new List<UiEntityIdentifier>();
            foreach (var identifier in _tabViewModelsDictionary.Keys)
            {
                if (IsTreeItemIdentifierIsChildOfRegion(regionId, identifier))
                {
                    idsToRemove.Add(identifier);
                }
            }
            foreach (var identifierToRemove in idsToRemove)
            {
                CloseTab(identifierToRemove);
            }
        }

        private bool IsTreeItemIdentifierIsChildOfRegion(string regionId,UiEntityIdentifier uiEntityIdentifier)
        {
            if (uiEntityIdentifier.ItemId.ToString() == regionId)
            {
                return true;
            }
            if (uiEntityIdentifier.ParenUiEntityIdentifier == null)
            {
                return false;
            }

            return IsTreeItemIdentifierIsChildOfRegion(regionId, uiEntityIdentifier.ParenUiEntityIdentifier);
        }
    }
}
