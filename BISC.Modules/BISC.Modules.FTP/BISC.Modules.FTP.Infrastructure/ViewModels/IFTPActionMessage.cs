using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.FTP.Infrastructure.ViewModels
{
    public interface IFTPActionMessage
    {
        string CreationDateTime { get; }
        bool? Status { get; set; }
        string Message { get; set; }
    }
}
