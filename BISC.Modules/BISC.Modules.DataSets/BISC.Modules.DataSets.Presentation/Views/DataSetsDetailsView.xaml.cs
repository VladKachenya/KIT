using BISC.Modules.DataSets.Presentation.ViewModels;
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

namespace BISC.Modules.DataSets.Presentation.Views
{
    /// <summary>
    /// Логика взаимодействия для DataSetsDetailsView.xaml
    /// </summary>
    public partial class DataSetsDetailsView : UserControl
    {
        public DataSetsDetailsView(DataSetsDetailsViewModel dataSetsDetailsViewModel)
        {
            InitializeComponent();
            DataContext = dataSetsDetailsViewModel;
        }
    }
}
