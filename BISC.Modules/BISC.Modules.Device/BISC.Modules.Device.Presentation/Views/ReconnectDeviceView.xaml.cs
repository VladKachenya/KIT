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
using BISC.Modules.Device.Presentation.ViewModels;

namespace BISC.Modules.Device.Presentation.Views
{
    /// <summary>
    /// Логика взаимодействия для ReconnectDeviceView.xaml
    /// </summary>
    public partial class ReconnectDeviceView : UserControl
    {
        public ReconnectDeviceView(ReconnectDeviceViewModel reconnectDeviceViewModel)
        {
            InitializeComponent();
            DataContext = reconnectDeviceViewModel;
        }
    }
}
