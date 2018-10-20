using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Services;
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
        private readonly ILoggingService _loggingService;
        private bool _isDeviceConnected;
        private bool _isLocalValuesShowing;
        private ObservableCollection<IInfoModelItemViewModel> _allIecTreeItems;
        private IInfoModelItemViewModel _selectedTreeItem;
        private bool _isFcSortChecked;
        private BiscNavigationParameters _biscNavigationParameters;

        public InfoModelTreeItemDetailsViewModel(IInfoModelTreeFactory infoModelTreeFactory,ILoggingService loggingService)
        {
            _infoModelTreeFactory = infoModelTreeFactory;
            _loggingService = loggingService;
        }

        public ObservableCollection<IInfoModelItemViewModel> AllIecTreeItems
        {
            get => _allIecTreeItems;
            set { SetProperty(ref _allIecTreeItems, value); }
        }

        public IInfoModelItemViewModel SelectedTreeItem
        {
            get => _selectedTreeItem;
            set { SetProperty(ref _selectedTreeItem, value,true); }
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
            set
            {
                if (_isFcSortChecked != value)
                {
                    SetProperty(ref _isFcSortChecked, value);
                    _loggingService.LogUserAction($"Пользователь выставил сортировку по FC в состояние {value}");
                    UpdateInfoModelTree();
                }

                SetProperty(ref _isFcSortChecked, value);
            }
        }

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _biscNavigationParameters = navigationContext.BiscNavigationParameters;
            UpdateInfoModelTree();
            base.OnNavigatedTo(navigationContext);
        }

        private void UpdateInfoModelTree()
        {
            var ldevice = _biscNavigationParameters.GetParameterByName<ILDevice>(
                InfoModelKeys.ModelKeys.LDeviceKey);
            if (ldevice != null)
            {
                AllIecTreeItems =
                    _infoModelTreeFactory.CreateLDeviceInfoModelTree(ldevice, IsFcSortChecked, AllIecTreeItems);
            }
            else if (_biscNavigationParameters.Any((parameter => parameter.ParameterName == "IED")))
            {
                List<ILDevice> devices = new List<ILDevice>();
                _biscNavigationParameters.GetParameterByName<IModelElement>("IED")
                    .GetAllChildrenOfType(ref devices);
                AllIecTreeItems =
                    _infoModelTreeFactory.CreateFullInfoModelTree(devices, IsFcSortChecked, AllIecTreeItems);
            }
        }


        protected override void OnNavigatedFrom(BiscNavigationContext navigationContext)
        {
            base.OnNavigatedFrom(navigationContext);
        }
    }
}