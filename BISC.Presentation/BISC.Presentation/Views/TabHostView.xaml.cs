using System.Windows.Controls;
using BISC.Presentation.Interfaces;

namespace BISC.Presentation.Views
{
    /// <summary>
    /// Логика взаимодействия для TabHost.xaml
    /// </summary>
    public partial class TabHostView : UserControl
    {
        public TabHostView(ITabHostViewModel tabHostViewModel)
        {
            InitializeComponent();
            DataContext = tabHostViewModel;
        }
    }
}