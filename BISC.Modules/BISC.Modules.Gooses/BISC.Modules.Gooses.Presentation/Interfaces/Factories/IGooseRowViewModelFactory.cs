using System.Collections.Generic;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix;

namespace BISC.Modules.Gooses.Presentation.Interfaces.Factories
{
	public interface IGooseRowViewModelFactory
	{
		IGooseRowViewModel CreateGooseRowViewModel(int numberOfColumns, string rowName,
			GooseControlBlockViewModel parent,IEnumerable<int> selectedColumns);
	}
}