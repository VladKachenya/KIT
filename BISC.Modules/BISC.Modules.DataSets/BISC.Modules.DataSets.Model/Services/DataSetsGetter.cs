using System;
using System.Collections.Generic;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Keys;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;

namespace BISC.Modules.DataSets.Model.Services
{
    public class DataSetsGetter : IConfigurableModelElementsGetter
    {
        private readonly IDataSetModelService _dataSetModelService;

        public DataSetsGetter(IDataSetModelService dataSetModelService)
        {
            _dataSetModelService = dataSetModelService;
        }

        public string ModuleName => InfrastructureKeys.ModulesKeys.DataSetModule;

        public IEnumerable<IModelElement> GetConfigurableModelElements(IDevice device, ISclModel sclModel)
        {
            return _dataSetModelService.GetDynamicDataSets(device.DeviceGuid, sclModel);
        }
    }
}