using BISC.Model.Infrastructure.Elements;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;

namespace BISC.Modules.Gooses.Infrastructure.Model.FTP
{
    public interface IGooseDeviceInput:IModelElement
    {
        string DeviceOwnerName { get; set; }
        ChildModelsList<IGooseInputModelInfo> GooseInputModelInfoList { get; }
        ChildModelProperty<IGooseMatrixFtp> GooseMatrix { get; }

    }
}