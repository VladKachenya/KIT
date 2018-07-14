using System.Windows.Controls;
using BISC.Modules.Device.Presentation.Interfaces;
using BISC.Modules.Device.Presentation.ViewModels.Tree;

namespace BISC.Modules.Device.Presentation.Views
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class DeviceTreeItemView : UserControl
    {
        public DeviceTreeItemView(DeviceTreeItemViewModel deviceTreeItemViewModel)
        {
            InitializeComponent();
            DataContext = deviceTreeItemViewModel;
        }
    }
}
