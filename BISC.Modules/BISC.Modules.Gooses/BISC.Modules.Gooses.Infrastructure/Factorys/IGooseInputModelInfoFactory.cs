using System;
using System.Collections.Generic;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;

namespace BISC.Modules.Gooses.Infrastructure.Factorys
{
    public interface IGooseInputModelInfoFactory
    {
        IGooseInputModelInfo CreateGooseInputModelInfo(IDevice parientDevice, IGooseControl gooseControl);
        List<IGooseInputModelInfo> CreateGooseInputModelInfoList(List<Tuple<IDevice, IGooseControl>> gooseControls);
    }
}