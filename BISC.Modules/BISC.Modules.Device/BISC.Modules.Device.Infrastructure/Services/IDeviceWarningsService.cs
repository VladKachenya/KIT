using System;
using System.Collections.Generic;

namespace BISC.Modules.Device.Infrastructure.Services
{
    public interface IDeviceWarningsService
    {
        void SetWarningOfDevice(Guid deviceGuid, string warningTag, string warningMassage = null);
        void ClearDeviceWarningsOfDevice(Guid deviceGuid);
        void RemoveDeviceWarning(Guid deviceGuid, string warningTag);
        bool GetIsDeviceWarningRegistered(Guid deviceGuid);
        bool GetIsDeviceWarningRegistered(Guid deviceGuid, string warningTag);
        string GetWarningMassage(Guid deviceGuid, string warningTag);
        List<string> GetWarningMassagesOfDevice(Guid deviceGuid);


    }
}