using System.Collections.Generic;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Model.Model;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix;

namespace BISC.Modules.Gooses.Presentation.Interfaces.Factories
{
	public interface IGooseRowViewModelFactory
	{
		List<IGooseRowViewModel> CreateGooseFtpOnlyRowsViewModel(List<IGooseRowFtpEntity> gooseRowFtpEntities, GooseControlBlockViewModel parent);
	    List<IGooseRowViewModel> CreateGooseProjectRowsViewModel(List<IGooseRowFtpEntity> gooseRowFtpEntities,IDataSet relatedDataset, GooseControlBlockViewModel parent,IGooseInput gooseInput);

    }
}