using BISC.Modules.Connection.Presentation.Interfaces.ViewModel.ChangeIpNetworkCard;
using BISC.Presentation.BaseItems.ViewModels;

namespace BISC.Modules.Connection.Presentation.ViewModels.ChangeIpNetworkCard
{
    public class NetworkCardSettingsViewModel : ViewModelBase, INetworkCardSettingsViewModel
    {
        #region private filds
        private string _settingsName;
        private string _ip;
        private string _dns;
        #endregion

        #region Ctor

        public NetworkCardSettingsViewModel()
        {
                
        }

        #endregion

        #region Implementation of INetworkCardSettingsViewModel

        public string SettingsName
        {
            get => _settingsName;
            set => SetProperty(ref _settingsName, value);
        }

        public string Ip
        {
            get => _ip;
            set => SetProperty(ref _ip, value);

        }

        public string Dns
        {
            get => _dns;
            set => SetProperty(ref _dns, value);

        }
        #endregion
    }
}