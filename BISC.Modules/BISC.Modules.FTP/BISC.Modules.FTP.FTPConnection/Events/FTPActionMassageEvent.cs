using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.FTP.FTPConnection.Events
{
    class FTPActionMassageEvent
    {
        public bool? Status { get; set; }
        public string Message { get; set; }
    }
}
