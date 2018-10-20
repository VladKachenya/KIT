using BISC.Model.Infrastructure.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Reports.Infrastructure.Model
{
    public interface IRptEnabled : IModelElement
    {
       int Max { get; set; }
    }
}
