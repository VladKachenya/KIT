using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;

namespace BISC.Presentation.BaseItems.Commands
{
   public class RegionsClosedCommands
    {
        public static RoutedCommand RegionsClosedCommand =new RoutedCommand("RegionsClosedCommand",typeof(FrameworkElement));

    }
}
