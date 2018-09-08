using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Common;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.InformationModel.Infrastucture;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Presentation.Interfaces;
using BISC.Modules.InformationModel.Presentation.Interfaces.Factories;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Navigation;

namespace BISC.Modules.InformationModel.Presentation.ViewModels
{
    public class InfoModelTreeItemDetailsViewModel : NavigationViewModelBase
    {
        private readonly IInfoModelTreeFactory _infoModelTreeFactory;
        private bool _isDeviceConnected;
        private bool _isLocalValuesShowing;
        private ObservableCollection<IInfoModelItemViewModel> _allIecTreeItems;
        private IInfoModelItemViewModel _selectedTreeItem;
        private bool _isFcSortChecked;

        public InfoModelTreeItemDetailsViewModel(IInfoModelTreeFactory infoModelTreeFactory)
        {
            _infoModelTreeFactory = infoModelTreeFactory;
        }

        public ObservableCollection<IInfoModelItemViewModel> AllIecTreeItems
        {
            get => _allIecTreeItems;
            set { SetProperty(ref _allIecTreeItems, value); }
        }

        public IInfoModelItemViewModel SelectedTreeItem
        {
            get => _selectedTreeItem;
            set { SetProperty(ref _selectedTreeItem, value); }
        }

        bool IsLocalValuesShowing
        {
            get => _isLocalValuesShowing;
            set { SetProperty(ref _isLocalValuesShowing, value); }
        }

        private bool IsDeviceConnected
        {
            get => _isDeviceConnected;
            set { SetProperty(ref _isDeviceConnected, value); }
        }

        public bool IsFcSortChecked
        {
            get => _isFcSortChecked;
            set => SetProperty(ref _isFcSortChecked , value);
        }

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            var ldevice = navigationContext.BiscNavigationParameters.GetParameterByName<ILDevice>(
                InfoModelKeys.ModelKeys.LDeviceKey);
            if (ldevice != null)
            {
                AllIecTreeItems = _infoModelTreeFactory.CreateLDeviceInfoModelTree(ldevice);
            }
            else if (navigationContext.BiscNavigationParameters.Any((parameter =>parameter.ParameterName=="IED" )))
            {
                List<ILDevice> devices=new List<ILDevice>();
                navigationContext.BiscNavigationParameters.GetParameterByName<IModelElement>("IED")
                    .GetAllChildrenOfType(ref devices);
                    AllIecTreeItems = _infoModelTreeFactory.CreateFullInfoModelTree(devices);
            }
            base.OnNavigatedTo(navigationContext);
        }

        protected override void OnNavigatedFrom(BiscNavigationContext navigationContext)
        {
            base.OnNavigatedFrom(navigationContext);
        }
    }
}
