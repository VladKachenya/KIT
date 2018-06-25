using System.Windows.Controls;
using BISC.Presentation.Infrastructure.Tree;

namespace BISC.Presentation.Views
{
    /// <summary>
    /// Логика взаимодействия для MainTree.xaml
    /// </summary>
    public partial class MainTreeView : UserControl
    {
        public MainTreeView(IMainTreeItem mainTreeItem)
        {
            InitializeComponent();
            DataContext = mainTreeItem;
        }
    }
}
