using System;
using System.Collections.Generic;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Keys;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Services;

namespace BISC.Modules.DataSets.Model.Services
{
    public class DataSetConfigurableModelElementsGetter : IConfigurableModelElementsGetter
    {
        private readonly IDataSetModelService _dataSetModelService;

        public DataSetConfigurableModelElementsGetter(IDataSetModelService dataSetModelService)
        {
            _dataSetModelService = dataSetModelService;
        }

        public string ModuleName => InfrastructureKeys.ModulesKeys.DataSetModule;

        public IEnumerable<IModelElement> GetConfigurableModelElements(Guid deviceGuid, ISclModel sclModel)
        {
            return _dataSetModelService.GetDynamicDataSets(deviceGuid, sclModel);
        }
    }
}