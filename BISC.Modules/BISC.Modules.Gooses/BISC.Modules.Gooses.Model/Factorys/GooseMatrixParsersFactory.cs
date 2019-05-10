using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Factorys;
using BISC.Modules.Gooses.Infrastructure.Helpers;
using BISC.Modules.Gooses.Model.Helpers;

namespace BISC.Modules.Gooses.Model.Factorys
{
    public class GooseMatrixParsersFactory : IGooseMatrixParsersFactory
    {
        public IGooseMatrixFtpToFileParser GetGooseMatrixParser(IDevice device)
        {
            if(device.RevisionDetails == null || device.RevisionDetails.CompareVersionTo(23, 8) <= 0)
            {
                return null;
            }
            if (device.RevisionDetails.CompareVersionTo(23, 9) <= 0)
            {
                return new GooseMatrixFtpToFileParserFrom23_9();
            }
            return new GooseMatrixFtpToFileParser23_10();
        }
    }
}