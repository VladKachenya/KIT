using System.Windows.Controls;
using BISC.Presentation.Interfaces;
using BISC.Presentation.Interfaces.Tree;

namespace BISC.Presentation.Views
{
    /// <summary>
    /// Логика взаимодействия для TabHost.xaml
    /// </summary>
    public partial class TabHostView : UserControl
    {
        public TabHostView(IMainTreeViewModel mainTreeViewModel)
        {
            InitializeComponent();
            DataContext = mainTreeViewModel;
        }
    }
}