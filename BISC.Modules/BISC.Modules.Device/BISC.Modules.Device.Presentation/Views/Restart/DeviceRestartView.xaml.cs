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
using BISC.Modules.Device.Presentation.ViewModels.Restart;

namespace BISC.Modules.Device.Presentation.Views.Restart
{
    /// <summary>
    /// Логика взаимодействия для DeviceRestartView.xaml
    /// </summary>
    public partial class DeviceRestartView : UserControl
    {
        public DeviceRestartView(DeviceRestartViewModel deviceRestartViewModel)
        {
            InitializeComponent();
            DataContext = deviceRestartViewModel;
        }
    }
}
