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
using BISC.Modules.InformationModel.Presentation.Interfaces;
using BISC.Modules.InformationModel.Presentation.ViewModels;

namespace BISC.Modules.InformationModel.Presentation.Views
{
    /// <summary>
    /// Interaction logic for InfoModelTreeItemView.xaml
    /// </summary>
    public partial class InfoModelTreeItemView : UserControl
    {
        public InfoModelTreeItemView(InfoModelTreeItemViewModel infoModelTreeItemViewModel)
        {
            InitializeComponent();
            DataContext = infoModelTreeItemViewModel;
        }
    }
}
