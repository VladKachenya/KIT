using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BISC.Infrastructure.Global.Services;
using BISC.Interfaces;
using BISC.Presentation.Infrastructure.Events;
using MaterialDesignThemes.Wpf;

namespace BISC
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class Shell : Window
    {

        public Shell(IShellViewModel shellViewModel)
        {
            InitializeComponent();
            DataContext = shellViewModel;
            Closing += (s, e) => shellViewModel.ShellClosingCommand.Execute(e);
        }
    }
}
