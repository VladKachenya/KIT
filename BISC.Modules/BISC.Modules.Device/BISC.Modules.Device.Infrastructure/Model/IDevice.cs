using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Modularity;
using BISC.Model.Infrastructure;
using BISC.Model.Infrastructure.Project;

namespace BISC.Modules.Device.Infrastructure.Model
{
    public interface IDevice:IModelElement
    {
        string Name { get; set; }
        string Ip { get; set; }
    }
}
