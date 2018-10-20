using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.InformationModel.Infrastucture;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Navigation;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.InformationModel.Presentation.ViewModels
{
   public class LDeviceTreeItemViewModel:NavigationViewModelBase
   {
       private readonly ITabManagementService _tabManagementService;
       private ILDevice _lDevice;
       private string _lDeviceName;
       private BiscNavigationContext _navigationContext;

       public LDeviceTreeItemViewModel(ICommandFactory commandFactory,ITabManagementService tabManagementService)
       {
           _tabManagementService = tabManagementService;
           NavigateToDetailsCommand = commandFactory.CreatePresentationCommand(OnNavigateToDetailsExecute);
       }

       private void OnNavigateToDetailsExecute()
       {
           _tabManagementService.NavigateToTab(InfoModelKeys.InfoModelTreeItemDetailsViewKey,
               _navigationContext.BiscNavigationParameters,
               $"Logical Device {LDeviceName} устройства {_lDevice.Inst}",
               _navigationContext.BiscNavigationParameters.GetParameterByName<TreeItemIdentifier>(TreeItemIdentifier
                   .Key));
        }

       protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
       {
           _navigationContext = navigationContext;
            _lDevice = navigationContext.BiscNavigationParameters.GetParameterByName<ILDevice>(InfoModelKeys.ModelKeys.LDeviceKey);
            LDeviceName = _lDevice.Inst;
            base.OnNavigatedTo(navigationContext);
        }

       public string LDeviceName
       {
           get => _lDeviceName;
           set { SetProperty(ref _lDeviceName, value); }
       }

       public ICommand NavigateToDetailsCommand { get; }

   }
}
