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
    public interface IPingAddingViewModel
    {


        ICommand AddIpCommand { get; }

        ICommand PingCommand { get; }     
        ObservableCollection<string> LastConnections  { get; }


    }
}
