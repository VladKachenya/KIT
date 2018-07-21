using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BISC.Modules.Connection.Presentation.Interfaces.Ping
{
    public interface IPingItemViewModel
    {
        
        string IP { get; set; }
        bool? IsPing { get; set; }

        Action<IPingItemViewModel> SetAsSelectedIP { get; set; }
        Action<IPingItemViewModel> DeleteItem { get; set; }

        ICommand ItemClickCommand { get; }
        ICommand DeleteItemCommand { get; }
        ICommand PingCommand { get; }
    }
}
