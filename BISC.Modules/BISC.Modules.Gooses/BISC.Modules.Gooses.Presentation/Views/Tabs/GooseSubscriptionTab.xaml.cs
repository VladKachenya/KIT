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
using BISC.Modules.Gooses.Presentation.ViewModels.Tabs;

namespace BISC.Modules.Gooses.Presentation.Views.Tabs
{
    /// <summary>
    /// Логика взаимодействия для GooseReceivingTab.xaml
    /// </summary>
    public partial class GooseSubscriptionTab : UserControl
    {
        public GooseSubscriptionTab(GooseSubscriptionTabViewModel gooseSubscriptionTabViewModel)
        {
            InitializeComponent();
            DataContext = gooseSubscriptionTabViewModel;
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid.CommitEdit();
            //CollectionViewSource.GetDefaultView(DataGrid.ItemsSource).Refresh();
            DataGrid.Items.Refresh();
            
        }
    }
}
