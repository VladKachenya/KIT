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

        public void SetWarningOfDevice(string deviceName, string warningTag)
        {
            if (GetIsDeviceWarningRegistered(deviceName, warningTag))
            {
                return;
            }
            else
            {
                _deviceWarnings.Add(new DeviceWarning(deviceName, warningTag));
                _globalEventsService.SendMessage(new DeviceWarningsChanged(deviceName));
            }
        }

        public void ClearDeviceWarningsOfDevice(string deviceName)
        {
            var warningsToRemove = _deviceWarnings.Where((warning => warning.DeviceName == deviceName)).ToList();
            foreach (var warningToRemove in warningsToRemove)
            {
                {
                    _deviceWarnings.Remove(warningToRemove);
                }
            }
        }

        public void RemoveDeviceWarning(string deviceName, string warningTag)
        {
            var warningsToRemove = _deviceWarnings
                .Where((warning => warning.DeviceName == deviceName && warning.WarningTag == warningTag)).ToList();
            foreach (var warningToRemove in warningsToRemove)
            {
                {
                    _deviceWarnings.Remove(warningToRemove);
                }
            }
            if (warningsToRemove.Any())
            {
                _globalEventsService.SendMessage(new DeviceWarningsChanged(deviceName));
            }
        }

        public bool GetIsDeviceWarningRegistered(string deviceName, string warningTag)
        {
            return _deviceWarnings.Any(
                (warning => warning.DeviceName == deviceName && warning.WarningTag == warningTag));
        }

        #endregion
    }
}