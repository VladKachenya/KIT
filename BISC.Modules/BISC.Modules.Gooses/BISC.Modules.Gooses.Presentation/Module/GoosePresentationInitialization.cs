using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Presentation.BaseItems.Events;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Gooses.Presentation.Module
{
   public class GoosePresentationInitialization
    {
        private readonly IInjectionContainer _injectionContainer;
        private UiEntityIdentifier _subscriptionIdentifier;

        public GoosePresentationInitialization(IGlobalEventsService globalEventsService, IInjectionContainer injectionContainer)
        {
            _injectionContainer = injectionContainer;
            globalEventsService.Subscribe<ShellLoadedEvent>(ActivatePresentation);
            _subscriptionIdentifier = new UiEntityIdentifier(Guid.NewGuid(), null);
        }

        private void ActivatePresentation(ShellLoadedEvent obj)
        {
            var userInterfaceComposingService =
                _injectionContainer.ResolveType<IUserInterfaceComposingService>();
            var commandFactory =
                _injectionContainer.ResolveType<ICommandFactory>();


            userInterfaceComposingService.AddGlobalCommand(commandFactory.CreatePresentationCommand(OnOpenGooseSubscriptions),
                "Подписки Goose всех устройств", IconsKeys.LanPendingIconKey, true, true);
        }

        private void OnOpenGooseSubscriptions()
        {
            ITabManagementService tabManagementService= _injectionContainer.ResolveType<ITabManagementService>();
            tabManagementService.NavigateToTab(GooseKeys.GoosePresentationKeys.GooseSubscriptionTabKey, null, $"Подписка Goose всех устройств", _subscriptionIdentifier);
        }
    }
}
