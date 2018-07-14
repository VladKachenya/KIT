namespace BISC.Modules.Connection.Infrastructure.DeviceConnection
{
    public interface IDeviceConnection
    {
        string Ip { get;}
        bool IsPingable { get; set; }
        bool TryConnect();
        void Disconnect();

    }
}