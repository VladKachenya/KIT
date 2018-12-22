namespace BISC.Modules.Connection.Infrastructure.Events
{
    public class LossConnectionEvent
    {
        public string Ip { get; }
        public LossConnectionEvent(string ip)
        {
            Ip = ip;
        }
    }
}