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
using BISC.Modules.Gooses.Presentation.ViewModels.Subscriptions;
using BISC.Modules.Gooses.Presentation.ViewModels.Tabs;

namespace BISC.Modules.Gooses.Presentation.Views.Tabs
{
    /// <summary>
    /// Логика взаимодействия для GooseReceivingTab.xaml
    /// </summary>
    public partial class GooseSubscriptionTab 
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

        private void DataGrid_OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            DataTemplate dt = null;
            if (e.PropertyType == typeof(SubscriptionValue))
                dt = (DataTemplate)Resources["SubscriptionValueTemplate"];
            if (e.PropertyType == typeof(string))
                dt = (DataTemplate)Resources["StringTemplate"];

            if (dt != null)
            {
                DataGridTemplateColumn c = new DataGridTemplateColumn()
                {
                    CellTemplate = dt,
                    Header = e.Column.Header,
                    HeaderTemplate = e.Column.HeaderTemplate,
                    HeaderStringFormat = e.Column.HeaderStringFormat,
                    SortMemberPath = e.PropertyName // this is used to index into the DataRowView so it MUST be the property's name (for this implementation anyways)
                };
                e.Column = c;
            }

        }
    }
}
