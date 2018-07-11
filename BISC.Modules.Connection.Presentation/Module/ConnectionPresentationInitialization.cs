using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Connection.Presentation.Interfaces.Services;
using BISC.Modules.Connection.Presentation.Services;
using BISC.Presentation.Infrastructure.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Connection.Presentation.Module
{
    public class ConnectionPresentationInitialization
    {
        private readonly IInjectionContainer _injectionContainer;

        public ConnectionPresentationInitialization(IUserInterfaceComposingService userInterfaceComposingService, ICommandFactory commandFactory, IInjectionContainer injectionContainer)
        {
            _injectionContainer = injectionContainer;
            userInterfaceComposingService.AddGlobalCommand(commandFactory.CreateDelegateCommand(OnPingsPanelAdding, null), "Управление PING");
        }

        private void OnPingsPanelAdding()
        {
            _injectionContainer.ResolveType<IPingAddingServise>().OpenPingsView();
        }

    }
}
