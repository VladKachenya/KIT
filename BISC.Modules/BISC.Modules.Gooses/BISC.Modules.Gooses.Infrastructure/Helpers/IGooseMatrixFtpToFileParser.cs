using BISC.Modules.Gooses.Infrastructure.Model.Matrix;

namespace BISC.Modules.Gooses.Infrastructure.Helpers
{
    public interface IGooseMatrixFtpToFileParser
    {
        string GetFileStringFromMatrixModel(IGooseMatrixFtp matrixFtp);
    }
}