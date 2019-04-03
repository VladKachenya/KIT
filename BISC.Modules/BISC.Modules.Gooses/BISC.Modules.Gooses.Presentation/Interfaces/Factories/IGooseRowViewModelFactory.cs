using System.Collections.Generic;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Model.Model;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix;

namespace BISC.Modules.Gooses.Presentation.Interfaces.Factories
{
	public interface IGooseRowViewModelFactory
	{
	    List<IGooseRowViewModel> BuildGooseRowViewModels(GooseControlBlockViewModel parent,
	        IGooseInputModelInfo gooseModelInfo, IGooseMatrixFtp gooseMatrix);
    }
}