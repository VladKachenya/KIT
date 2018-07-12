namespace BISC.Modules.Connection.Infrastructure.DeviceConnection
{
    public interface IDeviceConnection
    {
        string Ip { get; set; }
        bool IsPingable { get; set; }
        bool TryConnect();
        void Disconnect();

    }
}