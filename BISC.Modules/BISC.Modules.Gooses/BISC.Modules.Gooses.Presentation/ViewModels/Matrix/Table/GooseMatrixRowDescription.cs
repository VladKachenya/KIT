using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Presentation.Interfaces;
using BISC.Presentation.BaseItems.ViewModels;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Matrix.Table
{
    public class GooseMatrixRowDescription : IGooseMatrixRowDescription
    {
        public GooseKeys.GooseSubscriptionRowType RowType { get; set;}
        public string DataSetName { get; set; }
        public string RowName { get; set; }
        public string DoiDataRef { get; set; }
        public int IndexOfFcdaInDataSet { get; set; }
        public string GoCbReference { get; set; }

        public override string ToString()
        {
            return RowName;
        }
    }
}