using System.Collections.Generic;

namespace BISC.Modules.Device.Infrastructure.Services
{
    public interface IDeviceWarningsService
    {
        void SetWarningOfDevice(string deviceName, string warningTag, string warningMassage = null);
        void ClearDeviceWarningsOfDevice(string deviceName);
        void RemoveDeviceWarning(string deviceName, string warningTag);
        bool GetIsDeviceWarningRegistered(string deviceName);
        bool GetIsDeviceWarningRegistered(string deviceName, string warningTag);
        string GetWarningMassage(string deviceName, string warningTag);
        List<string> GetWarningMassagesOfDevice(string deviceName);


    }
}