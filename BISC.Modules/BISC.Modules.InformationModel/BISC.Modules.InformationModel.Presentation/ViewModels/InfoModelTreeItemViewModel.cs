using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture;
using BISC.Modules.InformationModel.Presentation.Interfaces;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.InformationModel.Presentation.ViewModels
{
    public class InfoModelTreeItemViewModel : NavigationViewModelBase, IInfoModelTreeItemViewModel
    {
        private readonly ITabManagementService _tabManagementService;
        private BiscNavigationContext _navigationContext;

        public InfoModelTreeItemViewModel(ICommandFactory commandFactory, ITabManagementService tabManagementService)
            : base(null)
        {
            _tabManagementService = tabManagementService;
            NavigateToDetailsCommand = commandFactory.CreatePresentationCommand(OnNavigateToDetailsExecute);
        }

        private void OnNavigateToDetailsExecute()
        {
            _tabManagementService.NavigateToTab(InfoModelKeys.InfoModelTreeItemDetailsViewKey,
                _navigationContext.BiscNavigationParameters,
                $"Model {_navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>(DeviceKeys.DeviceModelKey).Name}",
                _navigationContext.BiscNavigationParameters.GetParameterByName<UiEntityIdentifier>(UiEntityIdentifier
                    .Key));
        }

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            _navigationContext = navigationContext;
            base.OnNavigatedTo(navigationContext);
        }

        public ICommand NavigateToDetailsCommand { get; }
    }
}
