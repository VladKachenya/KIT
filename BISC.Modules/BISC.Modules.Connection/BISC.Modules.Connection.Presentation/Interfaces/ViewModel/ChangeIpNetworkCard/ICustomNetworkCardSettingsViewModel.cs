using System.Windows.Input;

namespace BISC.Modules.Connection.Presentation.Interfaces.ViewModel.ChangeIpNetworkCard
{
    public interface ICustomNetworkCardSettingsViewModel
    {
        string NetWorkCardName { get; set; }
        string Ip { get; set; }
        string SubnetMask { get; set; }
        bool IsGetIpautomatically { get; set; }

        ICommand SetNetworkCardSettingsCommand { get; }
    }
}