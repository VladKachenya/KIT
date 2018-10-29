using BISC.Modules.Reports.Presentation.ViewModels;
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

namespace BISC.Modules.Reports.Presentation.Views
{
    /// <summary>
    /// Логика взаимодействия для ReportsDetailsView.xaml
    /// </summary>
    public partial class ReportsDetailsView : UserControl
    {
        public ReportsDetailsView(ReportsDetailsViewModel reportsDetailsViewModel)
        {
            InitializeComponent();
            DataContext = reportsDetailsViewModel;
        }

        private void MainGrid_OnLoaded(object sender, RoutedEventArgs e)
        {
            //double controlWidth = MainGrid.Width - GridSplitterColumn.Width;
            //ListBoxColumn.Width = controlWidth / 3;
            //ReportColumn.Width = controlWidth * 2 / 3;
        }
    }
}
