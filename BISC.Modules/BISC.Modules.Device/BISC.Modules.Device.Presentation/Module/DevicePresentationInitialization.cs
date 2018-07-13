using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Presentation.Interfaces.Services;
using BISC.Modules.Device.Presentation.ViewModels.Factories;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Device.Presentation.Module
{
   public class DevicePresentationInitialization
    {
        private readonly IInjectionContainer _injectionContainer;

        public DevicePresentationInitialization(IUserInterfaceComposingService userInterfaceComposingService,
            ICommandFactory commandFactory,IInjectionContainer injectionContainer)
        {
            _injectionContainer = injectionContainer;
            userInterfaceComposingService.AddGlobalCommand(commandFactory.CreateDelegateCommand(OnDeviceAdding, null), "Добавить устройство");
        }

        private void OnDeviceAdding()
        {
            _injectionContainer.ResolveType<IDeviceAddingService>().OpenDeviceAddingView();
        }
    }
}
