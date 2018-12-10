namespace BISC.Modules.Connection.Presentation.Interfaces.ViewModel.ChangeIpNetworkCard
{
    public interface INetworkCardSettingsViewModel
    {
        string SettingsName { get; set; }
        string Ip { get; set; }
        string Dns { get; set; }
    }
}