using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Modules.Device.Infrastructure.Services;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Modules.Gooses.Model.Services;
using BISC.Modules.Gooses.Presentation.Commands;
using BISC.Modules.Gooses.Presentation.Factories;
using BISC.Modules.Gooses.Presentation.Interfaces;
using BISC.Modules.Gooses.Presentation.Interfaces.Factories;
using BISC.Modules.Gooses.Presentation.Services;
using BISC.Modules.Gooses.Presentation.ViewModels.GooseControls;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix.Entities;
using BISC.Modules.Gooses.Presentation.ViewModels.Tabs;
using BISC.Modules.Gooses.Presentation.ViewModels.Tree;
using BISC.Modules.Gooses.Presentation.Views;
using BISC.Modules.Gooses.Presentation.Views.Tabs;
using BISC.Modules.Gooses.Presentation.Views.Tree;
using BISC.Presentation.Infrastructure.UiFromModel;
using System;
using BISC.Modules.Device.Infrastructure.Saving;
using BISC.Modules.Gooses.Presentation.Interfaces.Services;
using BISC.Modules.Gooses.Presentation.Services.SavingServices;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix.Table;

namespace BISC.Modules.Gooses.Presentation.Module
{
    public class GoosesPresentationModule : IAppModule
    {
        private readonly IInjectionContainer _injectionContainer;

        public GoosesPresentationModule(IInjectionContainer injectionContainer)
        {
            _injectionContainer = injectionContainer;
        }

        #region Implementation of IAppModule

        public void Initialize()
        {
            _injectionContainer.RegisterType<GoosesUiHandlingService>();
            _injectionContainer.ResolveType<IUiFromModelElementRegistryService>().RegisterModelElement(_injectionContainer.ResolveType<GoosesUiHandlingService>(), "IED");
            _injectionContainer.RegisterType<GooseGroupTreeItemViewModel>();
            _injectionContainer.RegisterType<GooseSubscriptionTabViewModel>();
            _injectionContainer.RegisterType<GooseMatrixTabViewModel>();
            _injectionContainer.RegisterType<GooseControlsTabViewModel>();
            _injectionContainer.RegisterType<ISelectableValueViewModel, SelectableValueViewModel>();
            _injectionContainer.RegisterType<GooseRowViewModelFactory>();
            _injectionContainer.RegisterType<GooseControlBlockViewModel>();

            _injectionContainer.RegisterType<IGooseControlBlockViewModelFactory, GooseControlBlockViewModelFromFtpFactory>();
            _injectionContainer.RegisterType<GooseControlsProjectSavingCommand>();
            //_injectionContainer.RegisterType<GooseMatrixSavingCommand>();

            _injectionContainer.RegisterType<GooseSubscriptionSavingCommand>();



            _injectionContainer.RegisterType<GooseControlsConflictsViewModel>();
            _injectionContainer.RegisterType<IElementConflictResolver, GoosesControlsConflictResolver>(Guid.NewGuid().ToString());
            _injectionContainer.RegisterType<IElementConflictResolver, GooseSubscriptionConflictResolver>(Guid.NewGuid().ToString());

            _injectionContainer.RegisterType<IGooseRowViewModelFactory, GooseRowViewModelFactory>();


            _injectionContainer.RegisterType<object, GooseGroupTreeItemView>(GooseKeys.GoosePresentationKeys.GooseGroupTreeItemViewKey);
            _injectionContainer.RegisterType<object, GooseSubscriptionTab>(GooseKeys.GoosePresentationKeys.GooseSubscriptionTabKey);
            _injectionContainer.RegisterType<object, GooseControlsTab>(GooseKeys.GoosePresentationKeys.GooseControlsTabKey);
            _injectionContainer.RegisterType<object, GooseMatrixTab>(GooseKeys.GoosePresentationKeys.GooseMatrixTabKey);
            _injectionContainer.RegisterType<IDeviceElementSavingService, GooseSubscriptionsSavingService>(Guid.NewGuid().ToString());
            _injectionContainer.RegisterType<IDeviceElementSavingService, GooseControlSavingService>(Guid.NewGuid().ToString());
            _injectionContainer.RegisterType<object, GooseControlsConflictsView>(GooseKeys.GoosePresentationKeys.GooseControlsConflictsView);

            _injectionContainer.RegisterType<IGooseSubscriptionDataTableFactory, GooseSubscriptionDataTableFactory>();
            _injectionContainer.RegisterType<IGooseViewModelService, GooseViewModelService>();

            _injectionContainer.RegisterType<GoosePresentationInitialization>(true);

            _injectionContainer.RegisterType<IGooseMatrixSelectableCellViewModel, GooseMatrixSelectableCellViewModel>();
            _injectionContainer.RegisterType<IGooseMatrixRowDescription, GooseMatrixRowDescription>();


            GoosePresentationInitialization presentationInitialization = _injectionContainer.ResolveType(typeof(GoosePresentationInitialization)) as GoosePresentationInitialization;
        }

        #endregion
    }
}
