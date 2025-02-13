﻿using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Modules.Reports.Infrastructure.Factorys;
using BISC.Modules.Reports.Infrastructure.Keys;
using BISC.Modules.Reports.Infrastructure.Services;
using BISC.Modules.Reports.Model.Factorys;
using BISC.Modules.Reports.Model.Serializers;
using BISC.Modules.Reports.Model.Services;
using System;
using BISC.Model.Infrastructure.Keys;
using BISC.Model.Infrastructure.Serializing;
using BISC.Modules.Device.Infrastructure.Loading;
using BISC.Modules.Device.Infrastructure.Services;

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
            _injectionContainer.RegisterType<IReportControlsFactory, ReportControlsFactory>();
            _injectionContainer.RegisterType<IReportControlNameService, ReportControlNameService>();
            _injectionContainer.RegisterType<IReportConfRevisionService, ReportConfRevisionService>();
            _injectionContainer.RegisterType<IConfigurationParser, ReportConfigurationParser>(InfrastructureKeys.ModulesKeys.ReportModule);
            _injectionContainer.RegisterType<IConfigurableModelElementsGetter, ReportControlsGetter>(Guid.NewGuid().ToString());

            
            _injectionContainer.RegisterType<IFtpReportModelService, FtpReportModelService>();
            _injectionContainer.RegisterType<IDeviceElementLoadingService, ReportControlLoadingService>(Guid.NewGuid().ToString());


        }

        #endregion
    }
}
