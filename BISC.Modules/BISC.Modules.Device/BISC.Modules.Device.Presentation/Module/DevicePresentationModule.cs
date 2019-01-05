using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Device.Infrastructure.Commands;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Loading;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Device.Presentation.Commands;
using BISC.Modules.Device.Presentation.Interfaces;
using BISC.Modules.Device.Presentation.Interfaces.Factories;
using BISC.Modules.Device.Presentation.Interfaces.Services;
using BISC.Modules.Device.Presentation.Services;
using BISC.Modules.Device.Presentation.ViewModels;
using BISC.Modules.Device.Presentation.ViewModels.Conflicts;
using BISC.Modules.Device.Presentation.ViewModels.Factories;
using BISC.Modules.Device.Presentation.ViewModels.Restart;
using BISC.Modules.Device.Presentation.ViewModels.Tree;
using BISC.Modules.Device.Presentation.Views;
using BISC.Modules.Device.Presentation.Views.Conflicts;
using BISC.Modules.Device.Presentation.Views.Restart;
using BISC.Presentation.Infrastructure.Commands;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Infrastructure.UiFromModel;

namespace BISC.Modules.Device.Presentation.Module
{
    public class DevicePresentationModule : IAppModule
    {

        private readonly IInjectionContainer _injectionContainer;

        public DevicePresentationModule(IInjectionContainer injectionContainer )
        {
            _injectionContainer = injectionContainer;
        }

        public void Initialize()
        {
            _injectionContainer.RegisterType<IDeviceAddingService, DeviceAddingService>(true);
            _injectionContainer.RegisterType<object, DeviceAddingView>(DeviceKeys.DeviceAddingViewKey);
            _injectionContainer.RegisterType<object, DeviceTreeItemView>(DeviceKeys.DeviceTreeItemViewKey);
            _injectionContainer.RegisterType<object, DeviceDetailsView>(DeviceKeys.DeviceDetailsViewKey);
            _injectionContainer.RegisterType<object, DeviceConnectingView>(DeviceKeys.DeviceConnectingViewKey);
            _injectionContainer.RegisterType<object, DeviceFromFileAddingView>(DeviceKeys.DeviceFromFileAddingViewKey);
            _injectionContainer.RegisterType<object, DeviceLoadingTreeItemView>(DeviceKeys.DeviceLoadingTreeItemViewKey);
            _injectionContainer.RegisterType<object, DeviceRestartView>(DeviceKeys.DeviceRestartViewKey);
            _injectionContainer.RegisterType<object, DeviceConflictsView>(DeviceKeys.DeviceConflictsViewKey);
            _injectionContainer.RegisterType<object, ReconnectDeviceView>(DeviceKeys.ReconnectDeviceViewKey);
            _injectionContainer.RegisterType<object, ReconnectDeviceTreeItemView>(DeviceKeys.ReconnectDeviceTreeItemViewKey);

            _injectionContainer.RegisterType<DeviceAddingViewModel>();
            _injectionContainer.RegisterType<IFileViewModel, FileViewModel>();
            _injectionContainer.RegisterType<IFileViewModelFactory, FileViewModelFactory>();
            _injectionContainer.RegisterType<IDeviceViewModelFactory, DeviceViewModelFactory>();
            _injectionContainer.RegisterType<IDeviceViewModel, DeviceViewModel>();
            _injectionContainer.RegisterType<IDeviceWarningsService, DeviceWarningsService>(true);
            _injectionContainer.RegisterType<ISaveBeforeRestartCommand, SaveBeforeRestartCommand>();

            _injectionContainer.RegisterType<DeviceManualConflictViewModel>();
            _injectionContainer.RegisterType<DeviceConflictsViewModel>();
            _injectionContainer.RegisterType<DeviceConflictFactory>();
            _injectionContainer.RegisterType<ReconnectDeviceTreeItemViewModel>();

            _injectionContainer.RegisterType<ReconnectDeviceViewModel>();

            _injectionContainer.RegisterType<DeviceTreeItemViewModel>();
            _injectionContainer.RegisterType<DeviceDetailsViewModel>();
            _injectionContainer.RegisterType<IDeviceReconnectionService, DeviceReconnectionService>();

            _injectionContainer.RegisterType<DeviceFromFileAddingViewModel>();
            _injectionContainer.RegisterType<DeviceConnectingViewModel>();

            _injectionContainer.RegisterType<DeviceRestartViewModel>();

            

            _injectionContainer.RegisterType<DeviceLoadingTreeItemViewModel>();
            _injectionContainer.RegisterType<IDeviceLoadingService, DeviceLoadingService>();

            var presentationInitialization = _injectionContainer.ResolveType(typeof(DevicePresentationInitialization)) as DevicePresentationInitialization;

        }


    }
}