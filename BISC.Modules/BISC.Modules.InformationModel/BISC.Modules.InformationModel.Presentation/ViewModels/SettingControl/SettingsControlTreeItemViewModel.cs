using System;
using System.Windows.Input;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.InformationModel.Presentation.ViewModels.SettingControl
{
    public class SettingsControlTreeItemViewModel : NavigationViewModelBase
    {
        private readonly ITabManagementService _tabManagementService;
        private BiscNavigationContext _navigationContext;
        private UiEntityIdentifier _detailsIdentifier;

        public SettingsControlTreeItemViewModel(ICommandFactory commandFactory, ITabManagementService tabManagementService)
            : base(null)
        {
            _tabManagementService = tabManagementService;
            NavigateToDetailsCommand = commandFactory.CreatePresentationCommand(OnNavigateToDetailsExecute);
        }

        private void OnNavigateToDetailsExecute()
        {

            var treeItemIdentifier = _navigationContext.BiscNavigationParameters.GetParameterByName<UiEntityIdentifier>(
                UiEntityIdentifier
                    .Key);
            _detailsIdentifier = new UiEntityIdentifier(Guid.NewGuid(), treeItemIdentifier);
            BiscNavigationParameters biscNavigationParameters = new BiscNavigationParameters();
            biscNavigationParameters.AddParameterByName("IED", _navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>("IED"));
            _tabManagementService.NavigateToTab(InfoModelKeys.SettingControlDetailsViewKey,
                biscNavigationParameters,
               $"Setting Groups {_navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>(DeviceKeys.DeviceModelKey).Name}",
               _detailsIdentifier);
        }

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _navigationContext = navigationContext;
            base.OnNavigatedTo(navigationContext);
        }

        public ICommand NavigateToDetailsCommand { get; }
    }
}
