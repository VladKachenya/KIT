using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Infrastructure.Global.Modularity;
using BISC.Interfaces;

namespace BISC.ViewModel
{
   public class GlobalCommand:IGlobalCommand
    {
        public string CommandName { get; set; }
        public ICommand Command { get; set; }
        public string IconId { get; set; }
    }
}
