using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Model.Infrastructure;
using BISC.Modules.Reports.Infrastructure.Keys;
using BISC.Modules.Reports.Infrastructure.Services;
using BISC.Modules.Reports.Model.Serializers;
using BISC.Modules.Reports.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Reports.Model.Module
{
    public class ReportsModelModule : IAppModule
    {
        private readonly IInjectionContainer _injectionContainer;

        public ReportsModelModule(IInjectionContainer injectionContainer)
        {
            _injectionContainer = injectionContainer;
        }


        #region Implementation of IAppModule

        public void Initialize()
        {
            var modelElementsRegistryService = _injectionContainer.ResolveType<IModelElementsRegistryService>();
            modelElementsRegistryService.RegisterModelElement(new ReportControlSerializer(), ReportsKeys.ReportsModelKeys.ReportControlModelKey);
            modelElementsRegistryService.RegisterModelElement(new RptEnabledSerializer(), ReportsKeys.ReportsModelKeys.RptEnabledModelKey);
            modelElementsRegistryService.RegisterModelElement(new OptFieldsSerializer(), ReportsKeys.ReportsModelKeys.OptFieldsModelKey);
            modelElementsRegistryService.RegisterModelElement(new TrgOpsSerializer(), ReportsKeys.ReportsModelKeys.TrgOpsModelKey);

            _injectionContainer.RegisterType<IReportsModelService, ReportsModelService>();
            
        }

        #endregion
    }
}
