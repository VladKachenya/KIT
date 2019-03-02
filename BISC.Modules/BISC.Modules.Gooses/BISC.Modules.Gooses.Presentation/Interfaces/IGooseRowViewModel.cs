using System;
using System.Collections.Generic;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix;

namespace BISC.Modules.Gooses.Presentation.Interfaces
{
    public interface IGooseRowViewModel : IDisposable
    {
        List<ISelectableValueViewModel> SelectableValueViewModels { get; }
        string RowName { get; set; }
        GooseControlBlockViewModel Parent { get; set; }
        string GooseRowType { get; set; }
        int NumberOfFcdaInDataSet { get; set; }
        IDataSet RelatedDataSet { get; set; }
    }
}