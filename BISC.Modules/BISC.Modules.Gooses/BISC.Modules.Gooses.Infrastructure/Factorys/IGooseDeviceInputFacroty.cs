using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Gooses.Infrastructure.Model.FTP;

namespace BISC.Modules.Gooses.Infrastructure.Factorys
{
    public interface IGooseDeviceInputFacroty
    {
        IGooseDeviceInput GetGooseDeviceInput();
    }
}
