using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Device.Infrastructure.Model
{
    public interface IDevice
    {
        string Name { get; set; }
        string Ip { get; set; }
    }
}
