using System;
using System.Collections.Generic;
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
using BISC.Interfaces;
using MaterialDesignThemes.Wpf;

namespace BISC
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class Shell :Window
    {
        public static Snackbar Snackbar;
        public Shell(IShellViewModel shellViewModel)
        {

            InitializeComponent();
            DataContext = shellViewModel;
            Snackbar = this.MainSnackbar;

        }
    }
}
