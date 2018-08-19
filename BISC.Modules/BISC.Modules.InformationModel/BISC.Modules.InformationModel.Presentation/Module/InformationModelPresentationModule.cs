using BISC.Infrastructure.Global.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Modules.InformationModel.Infrastucture;
using BISC.Modules.InformationModel.Presentation.Factories;
using BISC.Modules.InformationModel.Presentation.Helpers;
using BISC.Modules.InformationModel.Presentation.Interfaces;
using BISC.Modules.InformationModel.Presentation.Interfaces.Factories;
using BISC.Modules.InformationModel.Presentation.Interfaces.Helpers;
using BISC.Modules.InformationModel.Presentation.Services;
using BISC.Modules.InformationModel.Presentation.ViewModels;
using BISC.Modules.InformationModel.Presentation.ViewModels.InfoModelTree;
using BISC.Modules.InformationModel.Presentation.Views;
using BISC.Presentation.Infrastructure.UiFromModel;

namespace BISC.Modules.InformationModel.Presentation.Module
{
    public class InformationModelPresentationModule : IAppModule
    {
        private readonly IInjectionContainer _injectionContainer;

        public InformationModelPresentationModule(IInjectionContainer injectionContainer)
        {
            _injectionContainer = injectionContainer;
        }


        #region Implementation of IAppModule

        public void Initialize()
        {
            _injectionContainer.RegisterType<LDeviceUiHandlingService>();
            _injectionContainer.RegisterType<InfoModelUiHandlingService>();
            _injectionContainer.RegisterType<InfoModelTreeItemViewModel>();
            _injectionContainer.RegisterType<InfoModelTreeItemDetailsViewModel>();
            _injectionContainer.RegisterType<LDeviceTreeItemViewModel>();
            _injectionContainer.RegisterType<IInfoModelTreeFactory,InfoModelTreeFactory>();
            _injectionContainer.RegisterType<LDeviceInfoModelItemViewModel>();
            _injectionContainer.RegisterType<LogicalNodeZeroInfoModelItemViewModel>();
            _injectionContainer.RegisterType<LogicalNodeInfoModelItemViewModel>();
            _injectionContainer.RegisterType<ITreeItemDetailsBuilder, TreeItemDetailsBuilder>();
            _injectionContainer.RegisterType<DoiInfoModelItemViewModel>();
            _injectionContainer.RegisterType<DaiInfoModelItemViewModel>();
            _injectionContainer.RegisterType<SdiInfoModelItemViewModel>();

            _injectionContainer.RegisterType<object, InfoModelTreeItemView>(InfoModelKeys.InfoModelTreeItemViewKey);
            _injectionContainer.RegisterType<object, InformationModelDetailsView>(InfoModelKeys.InfoModelTreeItemDetailsViewKey);
            _injectionContainer.RegisterType<object, LDeviceTreeItemView>(InfoModelKeys.LdeviceTreeItemViewKey);

            InitializeUiServices(_injectionContainer.ResolveType<IUiFromModelElementRegistryService>());
        }

        private void InitializeUiServices(IUiFromModelElementRegistryService uiFromModelElementRegistryService)
        {
            uiFromModelElementRegistryService.RegisterModelElement(_injectionContainer.ResolveType<InfoModelUiHandlingService>(),"IED");
            uiFromModelElementRegistryService.RegisterModelElement(_injectionContainer.ResolveType<LDeviceUiHandlingService>(), InfoModelKeys.ModelKeys.LDeviceKey);

        }

        #endregion
    }
}