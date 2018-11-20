using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Common;

namespace BISC.Modules.Device.Infrastructure.HelpClasses
{
    public class ResolvingResult:OperationResult
    {
        public new static ResolvingResult SucceedResult => new ResolvingResult();

        public ResolvingResult( ) : base()
        {

        }
        public ResolvingResult(string error):base(error)
        {
            
        }
        public bool IsRestartNeeded { get; set; }
    }
}
