using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Services;

namespace BISC.Modules.Gooses.Model.Services
{
   public class GoosesControlsConflictResolver: IElementConflictResolver
    {
        private readonly IDeviceModelService _deviceModelService;
        private readonly IGoosesModelService _goosesModelService;

        public GoosesControlsConflictResolver(IDeviceModelService deviceModelService,IGoosesModelService goosesModelService)
        {
            _deviceModelService = deviceModelService;
            _goosesModelService = goosesModelService;
        }



        #region Implementation of IElementConflictResolver

        public string ConflictName => "Goose Controls";
        public bool GetIfConflictsExists(string deviceName, ISclModel sclModelInDevice, ISclModel sclModelInProject)
        {
            var deviceInsclModelInDevice = _deviceModelService.GetDeviceByName(sclModelInDevice, deviceName);
            var devicesclModelInProject = _deviceModelService.GetDeviceByName(sclModelInProject, deviceName);


            var gooseControlsInDevice = _goosesModelService.GetGooseControlsOfDevice(deviceInsclModelInDevice);
            var gooseControlsInProject = _goosesModelService.GetGooseControlsOfDevice(devicesclModelInProject);

            
            if (gooseControlsInDevice.Count != gooseControlsInProject.Count)
            {
                return true;
            }


            foreach (var gooseControlInDevice in gooseControlsInDevice)
            {
                var gooseControlInProject = gooseControlsInProject.FirstOrDefault((gc => gc.Name == gooseControlInDevice.Name));
                if (gooseControlInProject == null) return true;
                if (!gooseControlInProject.ModelElementCompareTo(gooseControlInDevice))
                {
                    return true;
                }
            }
            return false;

        }

        #endregion
    }
}
