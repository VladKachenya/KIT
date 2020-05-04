using System;
using System.Collections.Generic;
using System.Linq;
using BISC.Model.Infrastructure.Elements;
using BISC.Model.Infrastructure.Keys;
using BISC.Model.Infrastructure.Project;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.InformationModel.Infrastucture.Elements;
using BISC.Modules.InformationModel.Infrastucture.Keys;
using BISC.Modules.InformationModel.Infrastucture.Services;

namespace BISC.Modules.InformationModel.Model.Services
{
    public class DbValuesGetter : IConfigurableModelElementsGetter
    {
        private readonly IInfoModelService _infoModelService;

        public DbValuesGetter(IInfoModelService infoModelService)
        {
            _infoModelService = infoModelService;
        }

        public string ModuleName => InfrastructureKeys.ModulesKeys.InformationModelModule;
        public IEnumerable<IModelElement> GetConfigurableModelElements(IDevice device, ISclModel sclModel)
        {
            var dois = _infoModelService.GetAllDoiWithDbRecursive(device);
            List<IDai> daiList = new List<IDai>();
            foreach (var doi in dois)
            {
                daiList.AddRange(_infoModelService.GetAllFcsWithDai(doi.DaiCollection, doi.SdiCollection)
                    .Where(t => string.Equals(t.Item1, InformationModelKeys.DataAttributeHeaderKeys.dbFc, StringComparison.CurrentCultureIgnoreCase))
                    .Select(t => t.Item2));
            }
            return daiList;
        }
    }
}