using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Presentation.Infrastructure.Commands;

namespace BISC.Modules.Connection.Presentation.Interfaces.ViewModel
{
    public  interface IIpAddressViewModel
    {
        string FullIp { get; set; }
        ICommand PingCommand { get; }
        ICommand IpSelectedCommand { get; }
        ICommand ClearIpCommand { get; }

        bool? IsPingSuccess { get; set; }
        string ForToolTip { get; set; }

        Task PingAsync();
        Task PingGlobalEventAsync();
    }
}
