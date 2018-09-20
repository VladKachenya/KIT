using BISC.Modules.FTP.Infrastructure.ViewModels;
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

namespace BISC.Modules.FTP.FTPConnection.Views
{
    /// <summary>
    /// Interaction logic for FTPServiceView.xaml
    /// </summary>
    public partial class FTPServiceView : UserControl
    {
        public FTPServiceView(IFTPServiceViewModel FTPServiceViewModel)
        {
            InitializeComponent();
            this.DataContext = FTPServiceViewModel;
        }

        private void LoadedEvent(object sender, RoutedEventArgs e)
        {
            (sender as FrameworkElement).Width = (sender as FrameworkElement).ActualWidth -40;
        }
    }
}
