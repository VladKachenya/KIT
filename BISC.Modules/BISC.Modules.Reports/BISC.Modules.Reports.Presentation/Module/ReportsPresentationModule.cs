using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Modules.Reports.Infrastructure.Keys;
using BISC.Modules.Reports.Infrastructure.Presentation.Factorys;
using BISC.Modules.Reports.Infrastructure.Presentation.ViewModels;
using BISC.Modules.Reports.Presentation.Factorys;
using BISC.Modules.Reports.Presentation.Services;
using BISC.Modules.Reports.Presentation.ViewModels;
using BISC.Modules.Reports.Presentation.ViewModels.ReportElementsViewModels;
using BISC.Modules.Reports.Presentation.Views;
using BISC.Presentation.Infrastructure.UiFromModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Reports.Infrastructure.Presentation.Services;
using BISC.Modules.Reports.Model.Services;
using BISC.Modules.Reports.Presentation.Commands;

namespace BISC.Modules.Reports.Presentation.Module
{
    public class ReportsPresentationModule : IAppModule
    {
        private readonly IInjectionContainer _injectionContainer;

        public ReportsPresentationModule(IInjectionContainer injectionContainer)
        {
            _injectionContainer = injectionContainer;
        }

        public void Initialize()
        {
            _injectionContainer.RegisterType<ReportsUiHandlingService>();

            _injectionContainer.ResolveType<IUiFromModelElementRegistryService>().RegisterModelElement(_injectionContainer.ResolveType<ReportsUiHandlingService>(), "IED");
            _injectionContainer.RegisterType<IReportControlViewModel, ReportControlViewModel>();
            _injectionContainer.RegisterType<object, ReportsDetailsView>(ReportsKeys.ReportsPresentationKeys.ReportsDetailsView);
            _injectionContainer.RegisterType<object, ReportsTreeItemView>(ReportsKeys.ReportsPresentationKeys.ReportsTreeItemView);
            _injectionContainer.RegisterType<object, ReportsConflictsWindow>(ReportsKeys.ReportsPresentationKeys.ReportsConflictsWindow);

            _injectionContainer.RegisterType<IElementConflictResolver, ReportsConflictResolver>(Guid.NewGuid().ToString());
            _injectionContainer.RegisterType<ReportsConflictsViewModel>();

            _injectionContainer.RegisterType<ReportsTreeItemViewModel>();
            _injectionContainer.RegisterType<ReportsDetailsViewModel>();
            _injectionContainer.RegisterType<IOprionalFildsViewModel, OprionalFildsViewModel>();
            _injectionContainer.RegisterType<IReportEnabledViewModel, ReportEnabledViewModel>();
            _injectionContainer.RegisterType<ITriggerOptionsViewModel, TriggerOptionsViewModel>();
            _injectionContainer.RegisterType<ReportsSavingCommand>();
            _injectionContainer.RegisterType<IReportControlFactoryViewModel, ReportControlFactoryViewModel>(true);
            _injectionContainer.RegisterType<IReportVeiwModelService, ReportVeiwModelService>();

        }
    }
}
