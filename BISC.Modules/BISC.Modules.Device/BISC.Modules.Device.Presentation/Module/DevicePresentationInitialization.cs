using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Presentation.Interfaces.Services;
using BISC.Modules.Device.Presentation.ViewModels.Factories;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Infrastructure.UiFromModel;

namespace BISC.Modules.Device.Presentation.Module
{
   public class DevicePresentationInitialization
    {
        private readonly IInjectionContainer _injectionContainer;
        private readonly IUiFromModelElementRegistryService _uiFromModelElementRegistryService;

        public DevicePresentationInitialization(IUserInterfaceComposingService userInterfaceComposingService,
            ICommandFactory commandFactory,IInjectionContainer injectionContainer
            ,IUiFromModelElementRegistryService uiFromModelElementRegistryService)
        {
            _injectionContainer = injectionContainer;
            _uiFromModelElementRegistryService = uiFromModelElementRegistryService;
            userInterfaceComposingService.AddGlobalCommand(commandFactory.CreatePresentationCommand(OnDeviceAdding, null), "Добавить устройство", IconsKeys.ServerPlusIconKey);
            _uiFromModelElementRegistryService.RegisterModelElement(injectionContainer.ResolveType<IDeviceAddingService>(), ModelKeys.SclModelKey);
        }

        private void OnDeviceAdding()
        {
            _injectionContainer.ResolveType<IDeviceAddingService>().OpenDeviceAddingView();
        }
    }
}
