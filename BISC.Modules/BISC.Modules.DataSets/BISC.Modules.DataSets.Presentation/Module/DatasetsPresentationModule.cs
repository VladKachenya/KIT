using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Modules.DataSets.Infrastructure.Keys;
using BISC.Modules.DataSets.Infrastructure.ViewModels;
using BISC.Modules.DataSets.Infrastructure.ViewModels.Factorys;
using BISC.Modules.DataSets.Infrastructure.ViewModels.Services;
using BISC.Modules.DataSets.Presentation.Factorys;
using BISC.Modules.DataSets.Presentation.Services;
using BISC.Modules.DataSets.Presentation.Services.Interfaces;
using BISC.Modules.DataSets.Presentation.ViewModels;
using BISC.Modules.DataSets.Presentation.Views;
using BISC.Presentation.Infrastructure.UiFromModel;

namespace BISC.Modules.DataSets.Presentation.Module
{
   public class DatasetsPresentationModule:IAppModule
    {
        private readonly IInjectionContainer _injectionContainer;

        public DatasetsPresentationModule(IInjectionContainer injectionContainer)
        {
            _injectionContainer = injectionContainer;
        }


        #region Implementation of IAppModule

        public void Initialize()
        {
            _injectionContainer.RegisterType<DataSetsUiHandlingService>();
            _injectionContainer.ResolveType<IUiFromModelElementRegistryService>().RegisterModelElement(_injectionContainer.ResolveType<DataSetsUiHandlingService>(), "IED");

            _injectionContainer.RegisterType<object, DataSetsTreeItemView>(DatasetKeys.DatasetViewModelKeys.DataSetsTreeItemView);
            _injectionContainer.RegisterType<object, DataSetsDetailsView>(DatasetKeys.DatasetViewModelKeys.DataSetsDetailsView);
            _injectionContainer.RegisterType<object, FcdaAdderView>(DatasetKeys.DatasetViewModelKeys.FcdaAdderViewModel);

            _injectionContainer.RegisterType<IFcdaAdderViewModelService, FcdaAdderViewModelService>();
            _injectionContainer.RegisterType<IFcdaViewModelFactory, FcdaViewModelFactory>(true);
            _injectionContainer.RegisterType<IDatasetViewModelFactory, DatasetViewModelFactory>(true);
            _injectionContainer.RegisterType<DataSetsTreeItemViewModel>();
            _injectionContainer.RegisterType<DataSetsDetailsViewModel>();
            _injectionContainer.RegisterType<IFcdaViewModel, FcdaViewModel>();
            _injectionContainer.RegisterType<IDataSetViewModel, DataSetViewModel>();
            _injectionContainer.RegisterType<IFcdaAdderViewModel, FcdaAdderViewModel>();
            _injectionContainer.RegisterType<IDataSetSavingService, DataSetSavingService>();

        }

        #endregion
    }
}
