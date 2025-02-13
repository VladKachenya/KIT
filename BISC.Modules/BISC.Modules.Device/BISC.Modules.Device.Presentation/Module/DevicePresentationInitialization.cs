﻿using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Services;
using BISC.Model.Infrastructure.Keys;
using BISC.Modules.Device.Presentation.Interfaces.Services;
using BISC.Presentation.Infrastructure.Factories;
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
            userInterfaceComposingService.AddGlobalCommand(commandFactory.CreatePresentationCommand(OnDeviceAdding, null), "Добавить устройство", IconsKeys.ServerPlusIconKey,true,true);
            _uiFromModelElementRegistryService.RegisterModelElement(injectionContainer.ResolveType<IDeviceAddingService>(), InfrastructureKeys.ModelKeys.SclModelKey);
        }

        private void OnDeviceAdding()
        {
            _injectionContainer.ResolveType<IDeviceAddingService>().OpenDeviceAddingView();
        }
    }
}
