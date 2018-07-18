using System.Windows.Controls;
using BISC.Presentation.Interfaces.Tree;

namespace BISC.Presentation.Views
{
    /// <summary>
    /// Логика взаимодействия для MainTree.xaml
    /// </summary>
    public partial class MainTreeView : UserControl
    {
        public MainTreeView(IMainTreeViewModel mainTreeViewModel)
        {
            InitializeComponent();
            DataContext = mainTreeViewModel;

        }
    }
}
