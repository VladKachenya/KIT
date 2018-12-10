namespace BISC.Modules.Connection.Presentation.Interfaces.ViewModel.ChangeIpNetworkCard
{
    public interface ICurrentCardConfigurationViewModel
    {
        string NetWorkCardName { get; set; }
        string Ip { get; set; }
        string Subnet { get; set; }
        string Gateway { get; set; }
        string Dns { get; set; }
        bool DhcpEnabled { get; set; }
    }
}