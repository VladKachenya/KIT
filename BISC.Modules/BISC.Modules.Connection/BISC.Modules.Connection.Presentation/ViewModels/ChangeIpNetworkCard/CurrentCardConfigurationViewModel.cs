using BISC.Modules.Connection.Presentation.Interfaces.ViewModel.ChangeIpNetworkCard;
using BISC.Presentation.BaseItems.ViewModels;

namespace BISC.Modules.Connection.Presentation.ViewModels.ChangeIpNetworkCard
{
    public class CurrentCardConfigurationViewModel : ViewModelBase, ICurrentCardConfigurationViewModel
    {
        #region Privat Filds
        private string _netWorkCardName;
        private string _ip;
        private string _subnet;
        private string _gateway;
        private string _dns;
        private bool _dhcpEnabled;
        #endregion

        #region Ctor

        public CurrentCardConfigurationViewModel()
        {
            
        }

        #endregion
        #region Implementation of ICurrentCardConfigurationViewModel
        public string NetWorkCardName
        {
            get => _netWorkCardName;
            set => SetProperty(ref _netWorkCardName, value);
        }

        public string Ip
        {
            get => _ip;
            set => SetProperty(ref _ip, value);

        }

        public string Subnet
        {
            get => _subnet;
            set => SetProperty(ref _subnet, value);

        }

        public string Gateway
        {
            get => _gateway;
            set => SetProperty(ref _gateway, value);

        }

        public string Dns
        {
            get => _dns;
            set => SetProperty(ref _dns, value);

        }

        public bool DhcpEnabled
        {
            get => _dhcpEnabled;
            set => SetProperty(ref _dhcpEnabled, value);

        }
        #endregion
    }
}