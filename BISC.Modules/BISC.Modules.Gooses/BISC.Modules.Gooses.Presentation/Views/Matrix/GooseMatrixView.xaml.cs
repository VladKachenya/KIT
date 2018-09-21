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
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix;

namespace BISC.Modules.Gooses.Presentation.Views.Matrix
{
    /// <summary>
    /// Логика взаимодействия для GooseMatrixView.xaml
    /// </summary>
    public partial class GooseMatrixView : System.Windows.Controls.UserControl
    {
        public GooseMatrixView(GooseMatrixViewModel gooseMatrixViewModel)
        {
            InitializeComponent();
            DataContext = gooseMatrixViewModel;
        }
    }
}
