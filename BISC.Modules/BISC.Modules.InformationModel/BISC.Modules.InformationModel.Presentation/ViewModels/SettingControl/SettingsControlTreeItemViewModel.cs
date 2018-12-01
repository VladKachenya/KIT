using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.InformationModel.Presentation.ViewModels.SettingControl
{
   public class SettingsControlTreeItemViewModel:NavigationViewModelBase
   {
       private readonly ITabManagementService _tabManagementService;
       private BiscNavigationContext _navigationContext;

       public SettingsControlTreeItemViewModel(ICommandFactory commandFactory, ITabManagementService tabManagementService)
       {
           _tabManagementService = tabManagementService;
           NavigateToDetailsCommand = commandFactory.CreatePresentationCommand(OnNavigateToDetailsExecute);
       }

       private void OnNavigateToDetailsExecute()
       {
           _tabManagementService.NavigateToTab(InfoModelKeys.SettingControlDetailsViewKey,
               _navigationContext.BiscNavigationParameters,
               $"Setting Groups {_navigationContext.BiscNavigationParameters.GetParameterByName<IDevice>(DeviceKeys.DeviceModelKey).Name}",
               _navigationContext.BiscNavigationParameters.GetParameterByName<TreeItemIdentifier>(TreeItemIdentifier
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
