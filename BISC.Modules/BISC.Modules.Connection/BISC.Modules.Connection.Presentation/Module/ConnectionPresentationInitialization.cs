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
        private readonly IConnectionPresentationViewAddingServise _connectionPresentationViewAddingServise;

        public ConnectionPresentationInitialization(IUserInterfaceComposingService userInterfaceComposingService, ICommandFactory commandFactory,
            IInjectionContainer injectionContainer, IConnectionPresentationViewAddingServise connectionPresentationViewAddingServise)
        {
            _injectionContainer = injectionContainer;
            _connectionPresentationViewAddingServise = connectionPresentationViewAddingServise;
            userInterfaceComposingService.AddGlobalCommand(commandFactory.CreatePresentationCommand(OnPingsPanelAdding, null), "Управление PING", null, true, false);
            userInterfaceComposingService.AddGlobalCommand(commandFactory.CreatePresentationCommand(OnChangeIpNetworkCardAdding, () => false), "Изменение IP сетевой карты", IconsKeys.EthernetIconKey, true, false);
        }

        private void OnPingsPanelAdding()
        {
            _connectionPresentationViewAddingServise.OpenPingsView();
        }

        private void OnChangeIpNetworkCardAdding()
        {
            _connectionPresentationViewAddingServise.OpenChangeIpNetworkCardView();
        }

    }
}
