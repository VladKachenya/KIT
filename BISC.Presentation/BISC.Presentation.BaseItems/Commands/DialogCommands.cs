using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;

namespace BISC.Presentation.BaseItems.Commands
{
   public static class DialogCommands
   {
       public static RoutedCommand CloseDialogCommand => DialogHost.CloseDialogCommand;
   }
}
