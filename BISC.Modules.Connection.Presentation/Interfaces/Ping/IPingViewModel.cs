using BISC.Modules.Connection.Infrastructure.DeviceConnection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BISC.Modules.Connection.Presentation.Interfaces.Ping
{
    public interface IPingViewModel 
    {            
        ObservableCollection<IPingItemViewModel> LastConnections  { get; }
        ICommand PingCommand { get; }
        ICommand ClearSelectedIPCommand { get; }
        ICommand PingAllCommand { get; }
        string SelectedItemm { get; set; }
        int IP0 { get; set; }
        int IP1 { get; set; }
        int IP2 { get; set; }
        int IP3 { get; set; }

    }
}
