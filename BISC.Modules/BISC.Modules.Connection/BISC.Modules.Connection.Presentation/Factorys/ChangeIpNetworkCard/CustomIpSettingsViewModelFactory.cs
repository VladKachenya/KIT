using System.Collections.Generic;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Connection.Presentation.Interfaces.Factorys.ChangeIpNetworkCard;
using BISC.Modules.Connection.Presentation.Interfaces.ViewModel.ChangeIpNetworkCard;

namespace BISC.Modules.Connection.Presentation.Factorys.ChangeIpNetworkCard
{
    public class CustomIpSettingsViewModelFactory : ICustomIpSettingsViewModelFactory
    {
        #region private filds
        private readonly IInjectionContainer _injectionContainer;
        private readonly IUniqueNameService _uniqueNameService;
        private readonly INetworkCardSettingsService _networkCardSettingsService;

        #endregion

        #region Ctor

        public CustomIpSettingsViewModelFactory(IInjectionContainer injectionContainer, IUniqueNameService uniqueNameService, 
            INetworkCardSettingsService networkCardSettingsService)
        {
            _injectionContainer = injectionContainer;
            _uniqueNameService = uniqueNameService;
            _networkCardSettingsService = networkCardSettingsService;
        }

        #endregion

        #region Implementation of ICustomIpSettingsViewModelFactory
        public ICustomIpSettingsViewModel CreateCustomIpSettingsViewModel(List<string> existingSettingsName, string nameBody = "NewIpSettings")
        {
            var newSettings = _injectionContainer.ResolveType<ICustomIpSettingsViewModel>();
            newSettings.SettingsNamе =
                _uniqueNameService.GetUniqueName(existingSettingsName, nameBody);
            var availableNetworkCards = _networkCardSettingsService.GetNamesAvailableNetworkCards();
            foreach (var networkCard in availableNetworkCards)
            {
                var newCardSettin = _injectionContainer.ResolveType<ICustomNetworkCardSettingsViewModel>();
                newCardSettin.NetWorkCardName = networkCard;
                newSettings.NetworkCardSettingsViewModels.Add(newCardSettin);
            }
            return newSettings;
        }
        #endregion

    }
}