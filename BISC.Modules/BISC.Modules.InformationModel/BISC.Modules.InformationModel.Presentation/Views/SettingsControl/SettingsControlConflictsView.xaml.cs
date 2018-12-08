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
using BISC.Modules.InformationModel.Presentation.ViewModels.SettingControl;

namespace BISC.Modules.InformationModel.Presentation.Views.SettingsControl
{
    /// <summary>
    /// Логика взаимодействия для SettingsControlConflictsView.xaml
    /// </summary>
    public partial class SettingsControlConflictsView : UserControl
    {
        public SettingsControlConflictsView(SettingsControlConflivtsViewModel settingsControlConflivtsViewModel)
        {
            InitializeComponent();
            DataContext = settingsControlConflivtsViewModel;
        }
    }
}
