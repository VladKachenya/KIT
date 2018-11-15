using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Services;

namespace BISC.Modules.DataSets.Model.Services
{
   public class DatasetsConflictResolver: IElementConflictResolver
    {
        private readonly IDatasetModelService _datasetModelService;
        private readonly IDeviceModelService _deviceModelService;

        public DatasetsConflictResolver(IDatasetModelService datasetModelService,IDeviceModelService deviceModelService)
        {
            _datasetModelService = datasetModelService;
            _deviceModelService = deviceModelService;
        }



        #region Implementation of IElementConflictResolver

        public string ConflictName => "DataSets";
        public bool GetIfConflictsExists(string deviceName, ISclModel sclModelInDevice, ISclModel sclModelInProject)
        {
            var deviceInsclModelInDevice = _deviceModelService.GetDeviceByName(sclModelInDevice, deviceName);
            var devicesclModelInProject = _deviceModelService.GetDeviceByName(sclModelInProject, deviceName);

            var datasetsInDevice = _datasetModelService.GetAllDataSetOfDevice(deviceInsclModelInDevice);
            var datasetsInProject = _datasetModelService.GetAllDataSetOfDevice(devicesclModelInProject);

            if (datasetsInProject.Count != datasetsInDevice.Count)
            {
                return true;
            }

            foreach (var datasetInDevice in datasetsInDevice)
            {
                var datasetInProject = datasetsInProject.FirstOrDefault((ds => ds.Name == datasetInDevice.Name));
                if (datasetInProject == null) return true;
                if (!datasetInProject.ModelElementCompareTo(datasetInProject))
                {
                    return true;
                }
            }
            return false;

        }

        #endregion
    }
}
