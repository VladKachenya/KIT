using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Modules.Device.Infrastructure.Keys;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Device.Presentation.Services.Helpers;
using BISC.Presentation.BaseItems.Commands;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Navigation;

namespace BISC.Modules.Device.Presentation.ViewModels
{
   public class ReconnectDeviceViewModel:NavigationViewModelBase
    {
        private readonly ICommandFactory _commandFactory;
        private readonly IDeviceReconnectionService _deviceReconnectionService;
        private ReconnectDeviceContext _reconnectDeviceContext;

        public ReconnectDeviceViewModel(ICommandFactory commandFactory,IDeviceReconnectionService deviceReconnectionService)
        {
            _commandFactory = commandFactory;
            _deviceReconnectionService = deviceReconnectionService;
            CancelCommand = commandFactory.CreatePresentationCommand(OnCancel);
            SelectShowConflictsCommand = commandFactory.CreatePresentationCommand(OnSelectShowConflicts);
        }

        private void OnSelectShowConflicts()
        {
            _deviceReconnectionService.ReconnectDevice(_reconnectDeviceContext.Device,
                _reconnectDeviceContext.DeviceTreeItemIdentifier);
            DialogCommands.CloseDialogCommand?.Execute(null, null);
        }

        #region Overrides of NavigationViewModelBase

        protected override void OnNavigatedTo(BiscNavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            _reconnectDeviceContext = navigationContext.BiscNavigationParameters.GetParameterByName<ReconnectDeviceContext>(DeviceKeys
                .ReconnectDeviceContextKey);
        }

        #endregion

        private void OnCancel()
        {
            DialogCommands.CloseDialogCommand?.Execute(null,null);
        }

        public ICommand SelectLoadFromDeviceCommand { get; }
        public ICommand SelectUploadFromProjectCommand { get; }
        public ICommand SelectShowConflictsCommand { get; }
        public ICommand CancelCommand { get; }

        
    }
}
