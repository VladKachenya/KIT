using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Model.Infrastructure;
using BISC.Modules.DataSets.Infrastructure.Factorys;
using BISC.Modules.DataSets.Infrastructure.Keys;
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.DataSets.Model.Factorys;
using BISC.Modules.DataSets.Model.Serializers;
using BISC.Modules.DataSets.Model.Services;
using BISC.Modules.Device.Infrastructure.Loading;
using BISC.Modules.Device.Infrastructure.Services;

namespace BISC.Modules.DataSets.Model.Module
{
    public class DatasetsModelModule : IAppModule
    {
        private readonly IInjectionContainer _injectionContainer;

        public DatasetsModelModule(IInjectionContainer injectionContainer)
        {
            _injectionContainer = injectionContainer;
        }


        #region Implementation of IAppModule

        public void Initialize()
        {
            var modelElementsRegistryService = _injectionContainer.ResolveType<IModelElementsRegistryService>();
            modelElementsRegistryService.RegisterModelElement(new DataSetSerializer(), DatasetKeys.DatasetModelKeys.DataSetModelKey);
            modelElementsRegistryService.RegisterModelElement(new FcdaSerializer(), DatasetKeys.DatasetModelKeys.FcdaModelKey);
            _injectionContainer.RegisterType<IDeviceElementLoadingService,DatasetsLoadingService>(Guid.NewGuid().ToString());
            _injectionContainer.RegisterType<IDatasetModelService, DatasetModelService>();

            _injectionContainer.RegisterType<IFcdaFactory, FcdaFactory>(true);
            _injectionContainer.RegisterType<IDataSetFactory, DataSetFactory>(true);
        }

        #endregion
    }
}