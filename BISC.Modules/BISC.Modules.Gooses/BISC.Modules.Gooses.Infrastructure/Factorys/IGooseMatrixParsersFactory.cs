using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Helpers;

namespace BISC.Modules.Gooses.Infrastructure.Factorys
{
    public interface IGooseMatrixParsersFactory
    {
        IGooseMatrixFtpToFileParser GetGooseMatrixParser(IDevice device);
    }
}