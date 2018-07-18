using System.Windows.Controls;
using BISC.Presentation.Interfaces;
using BISC.Presentation.Interfaces.Tree;
using BISC.Presentation.ViewModels.Tab;

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