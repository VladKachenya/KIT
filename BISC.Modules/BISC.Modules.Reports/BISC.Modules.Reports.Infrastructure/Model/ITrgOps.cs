using BISC.Model.Infrastructure.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Reports.Infrastructure.Model
{
    public interface ITrgOps : IModelElement
    {
        bool Dchg { get; set; }
        bool Qchg { get; set; }
        bool Dupd { get; set; }
        bool Period { get; set; }
        bool Gi { get; set; }
    }
}
