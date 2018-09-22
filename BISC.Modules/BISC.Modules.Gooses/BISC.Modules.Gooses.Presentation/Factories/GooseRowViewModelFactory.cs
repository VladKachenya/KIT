using BISC.Infrastructure.Global.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Presentation.Interfaces;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix.Rows;

namespace BISC.Modules.Gooses.Presentation.Factories
{
    public class GooseRowViewModelFactory
    {
        private readonly IInjectionContainer _container;

        public GooseRowViewModelFactory(IInjectionContainer container)
        {
            _container = container;
        }


        #region Implementation of IGooseRowViewModelFactory

        public IGooseRowViewModel CreateGooseRowViewModel(IGooseRow gooseRow, GooseControlBlockViewModel gooseControlBlockViewModel)
        {

            IGooseRowViewModel gooseRowViewModel = new GooseRowViewModel();
            gooseRowViewModel.Model = gooseRow;
            gooseRowViewModel.Parent = gooseControlBlockViewModel;
            return gooseRowViewModel;

        }

        #endregion
    }
}
