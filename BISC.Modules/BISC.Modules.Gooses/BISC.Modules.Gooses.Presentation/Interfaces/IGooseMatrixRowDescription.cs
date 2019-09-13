using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;

namespace BISC.Modules.Gooses.Presentation.Interfaces
{
    public interface IGooseMatrixRowDescription
    {
        GooseKeys.GooseSubscriptionRowType RowType { get; set; }
        string DataSetName { get; set; }
        string RowName { get; set; }
        string DoiDataRef { get; set; }
        int IndexOfFcdaInDataSet { get; set; }
        string GoCbReference { get; set; }
    }
}