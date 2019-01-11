using BISC.Infrastructure.Global.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Presentation.Interfaces;
using BISC.Modules.Gooses.Presentation.Interfaces.Factories;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix.Rows;

namespace BISC.Modules.Gooses.Presentation.Factories
{
    public class GooseRowViewModelFactory:IGooseRowViewModelFactory
    {
        private readonly IInjectionContainer _container;

        public GooseRowViewModelFactory(IInjectionContainer container)
        {
            _container = container;
        }


        #region Implementation of IGooseRowViewModelFactory

        public IGooseRowViewModel CreateGooseRowViewModelOld(IGooseRow gooseRow, GooseControlBlockViewModel gooseControlBlockViewModel)
        {

            IGooseRowViewModel gooseRowViewModel = new GooseRowViewModel();
            gooseRowViewModel.Model = gooseRow;
            gooseRowViewModel.Parent = gooseControlBlockViewModel;
            return gooseRowViewModel;

        }

        #endregion

	    public IGooseRowViewModel CreateGooseRowViewModel(int numberOfColumns, string rowName, GooseControlBlockViewModel parent, IEnumerable<int> selectedColumns)
	    {
		    IGooseRowViewModel gooseRowViewModel = new GooseRowViewModel();


		    var columnIndexes = selectedColumns.ToList();

			gooseRowViewModel.RowName = rowName;

		    for (int i = 0; i < numberOfColumns; i++)
		    {
			    ISelectableValueViewModel selectableValueViewModel = StaticContainer.CurrentContainer.ResolveType<ISelectableValueViewModel>();
			    selectableValueViewModel.SelectedValue = columnIndexes.Any((columnIndex => columnIndex==i));
			    selectableValueViewModel.ColumnNumber = i;
			    selectableValueViewModel.Parent = gooseRowViewModel;
			    selectableValueViewModel.ToolTip = rowName + "     " + (i + 1);
			    gooseRowViewModel.SelectableValueViewModels.Add(selectableValueViewModel);

		    }
			gooseRowViewModel.Parent = parent;
		    return gooseRowViewModel;
	    }
    }
}
