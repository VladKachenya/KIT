using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Presentation.Infrastructure.Events
{
    public class ShellBlockEvent
    {
        public string Message { get; set; }
        public bool IsBlocked { get; set; }
    }
}
