using System.Windows.Input;
using BISC.Modules.Connection.Infrastructure.Services;
using BISC.Modules.Connection.Presentation.Interfaces.ViewModel.ChangeIpNetworkCard;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;

namespace BISC.Modules.Connection.Presentation.ViewModels.ChangeIpNetworkCard
{
    public class CustomNetworkCardSettingsViewModel : ViewModelBase, ICustomNetworkCardSettingsViewModel
    {
        private readonly INetworkCardSettingsService _networkCardSettingsService;

        #region private filds
        private string _settingsName;
        private string _ip;
        private string _subnetMask = "255.255.255.0";
        private bool _isGetIpAutomatically;

        #endregion

        #region Ctor

        public CustomNetworkCardSettingsViewModel(ICommandFactory commandFactory, INetworkCardSettingsService networkCardSettingsService)
        {
            _networkCardSettingsService = networkCardSettingsService;
            SetNetworkCardSettingsCommand = commandFactory.CreatePresentationCommand(OnSetNetworkCardSettings);
        }

        #endregion

        #region private methods

        private void OnSetNetworkCardSettings()
        {

        }

        private bool IsSetNetworkCardSettings()
        {
            return true;
        }

        #endregion

        #region Implementation of ICustomNetworkCardSettingsViewModel

        public string NetWorkCardName
        {
            get => _settingsName;
            set => SetProperty(ref _settingsName, value);
        }

        public string Ip
        {
            get => _ip;
            set => SetProperty(ref _ip, value);

        }

        public string SubnetMask
        {
            get => _subnetMask;
            set => SetProperty(ref _subnetMask, value);
        }

        public bool IsGetIpautomatically
        {
            get => _isGetIpAutomatically;
            set => SetProperty(ref _isGetIpAutomatically, value);
        }

        public  ICommand SetNetworkCardSettingsCommand { get; }

        #endregion
    }
}