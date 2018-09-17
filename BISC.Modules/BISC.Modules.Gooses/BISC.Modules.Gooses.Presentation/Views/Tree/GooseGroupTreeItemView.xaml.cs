using System.Windows.Controls;
using BISC.Modules.Gooses.Presentation.ViewModels.Tree;

namespace BISC.Modules.Gooses.Presentation.Views.Tree
{
    /// <summary>
    /// Логика взаимодействия для GooseGroupTreeItemView.xaml
    /// </summary>
    public partial class GooseGroupTreeItemView : UserControl
    {
        public GooseGroupTreeItemView(GooseGroupTreeItemViewModel gooseGroupTreeItemViewModel)
        {
            InitializeComponent();
            DataContext = gooseGroupTreeItemViewModel;
        }
    }
}
