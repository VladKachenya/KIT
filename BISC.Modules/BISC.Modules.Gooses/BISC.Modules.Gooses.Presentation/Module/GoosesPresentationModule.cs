using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Modularity;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Modules.Gooses.Presentation.Services;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix;
using BISC.Modules.Gooses.Presentation.ViewModels.Tabs;
using BISC.Modules.Gooses.Presentation.ViewModels.Tree;
using BISC.Modules.Gooses.Presentation.Views.Matrix;
using BISC.Modules.Gooses.Presentation.Views.Tabs;
using BISC.Modules.Gooses.Presentation.Views.Tree;
using BISC.Presentation.Infrastructure.UiFromModel;

namespace BISC.Modules.Gooses.Presentation.Module
{
   public class GoosesPresentationModule:IAppModule
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
            _injectionContainer.ResolveType<IUiFromModelElementRegistryService>().RegisterModelElement(_injectionContainer.ResolveType<GoosesUiHandlingService>(),"IED");
            _injectionContainer.RegisterType<GooseGroupTreeItemViewModel>();
            _injectionContainer.RegisterType<GooseSubscriptionTabViewModel>();
            _injectionContainer.RegisterType<GooseMatrixTabViewModel>();
            _injectionContainer.RegisterType<GooseEditingTabViewModel>();
            _injectionContainer.RegisterType<GooseControlAssignmentViewModel>();

            _injectionContainer.RegisterType<object,GooseGroupTreeItemView>(GooseKeys.GoosePresentationKeys.GooseGroupTreeItemViewKey);
            _injectionContainer.RegisterType<object, GooseSubscriptionTab>(GooseKeys.GoosePresentationKeys.GooseSubscriptionTabKey);
            _injectionContainer.RegisterType<object, GooseEditingTab>(GooseKeys.GoosePresentationKeys.GooseEditingTabKey);
            _injectionContainer.RegisterType<object, GooseMatrixTab>(GooseKeys.GoosePresentationKeys.GooseMatrixTabKey);
            _injectionContainer.RegisterType<object, GooseMatrixView>(GooseKeys.GoosePresentationKeys.GooseMatrixViewKey);
            _injectionContainer.RegisterType<object, GooseControlAssignmentView>(GooseKeys.GoosePresentationKeys.GooseControlAssignmentViewKey);

        }

        #endregion
    }
}
