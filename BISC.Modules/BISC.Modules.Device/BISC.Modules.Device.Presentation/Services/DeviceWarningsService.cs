using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Device.Infrastructure.Events;
using BISC.Modules.Device.Infrastructure.HelpClasses;
using BISC.Modules.Device.Infrastructure.Services;

namespace BISC.Modules.Device.Presentation.Services
{
    public class DeviceWarningsService : IDeviceWarningsService
    {
        private readonly IGlobalEventsService _globalEventsService;
        private List<DeviceWarning> _deviceWarnings;

        public DeviceWarningsService(IGlobalEventsService globalEventsService)
        {
            _globalEventsService = globalEventsService;
            _deviceWarnings = new List<DeviceWarning>();
        }

        #region Implementation of IDeviceWarningsService

        public void SetWarningOfDevice(Guid deviceGuid, string warningTag, string deviceMassage = null)
        {
            if (GetIsDeviceWarningRegistered(deviceGuid, warningTag))
            {
                return;
            }
            else
            {
                _deviceWarnings.Add(new DeviceWarning(deviceGuid, warningTag, deviceMassage));
                _globalEventsService.SendMessage(new DeviceWarningsChanged(deviceGuid));
            }
        }

        public void ClearDeviceWarningsOfDevice(Guid deviceGuid)
        {
            var warningsToRemove = _deviceWarnings.Where((warning => warning.DeviceGuid == deviceGuid)).ToList();
            foreach (var warningToRemove in warningsToRemove)
            {
                {
                    _deviceWarnings.Remove(warningToRemove);
                }
            }
        }

        public void RemoveDeviceWarning(Guid deviceGuid, string warningTag)
        {
            var warningsToRemove = _deviceWarnings
                .Where((warning => warning.DeviceGuid == deviceGuid && warning.WarningTag == warningTag)).ToList();
            foreach (var warningToRemove in warningsToRemove)
            {
                {
                    _deviceWarnings.Remove(warningToRemove);
                }
            }
            if (warningsToRemove.Any())
            {
                _globalEventsService.SendMessage(new DeviceWarningsChanged(deviceGuid));
            }
        }

        public bool GetIsDeviceWarningRegistered(Guid deviceGuid, string warningTag)
        {
            return _deviceWarnings.Any(
                (warning => warning.DeviceGuid == deviceGuid && warning.WarningTag == warningTag));
        }

        public bool GetIsDeviceWarningRegistered(Guid deviceGuid)
        {
            return _deviceWarnings.Any( (warning => warning.DeviceGuid == deviceGuid) );
        }

        public string GetWarningMassage(Guid deviceGuid, string warningTag)
        {
            return _deviceWarnings
                .FirstOrDefault(warning => warning.DeviceGuid == deviceGuid && warning.WarningTag == warningTag)
                ?.WarningMasage;
        }

        public List<string> GetWarningMassagesOfDevice(Guid deviceGuid)
        {
            return _deviceWarnings.Where(warning => warning.DeviceGuid == deviceGuid).Select(warning => warning.WarningMasage).ToList();
        }

        #endregion
    }
}