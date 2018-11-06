namespace BISC.Modules.Device.Infrastructure.Services
{
    public interface IDeviceWarningsService
    {
        void SetWarningOfDevice(string deviceName, string warningTag);
        void ClearDeviceWarningsOfDevice(string deviceName);
        void RemoveDeviceWarning(string deviceName, string warningTag);
        bool GetIsDeviceWarningRegistered(string deviceName, string warningTag);
    }
}