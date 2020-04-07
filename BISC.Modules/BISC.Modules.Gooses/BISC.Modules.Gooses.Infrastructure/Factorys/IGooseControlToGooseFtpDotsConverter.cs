using System.Collections.Generic;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;

namespace BISC.Modules.Gooses.Infrastructure.Factorys
{
    public interface IGooseControlToGooseFtpDotsConverter
    {
        IEnumerable<GooseFtpDto> ConvertToDots(IEnumerable<IGooseControl> gooseControls);
    }
}