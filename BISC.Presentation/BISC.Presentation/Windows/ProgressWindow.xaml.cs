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

namespace BISC.Presentation.Windows
{
    /// <summary>
    /// Interaction logic for ProgressWindow.xaml
    /// </summary>
    public partial class ProgressWindow : Window
    {
        private object _lockObject = new object();
        public ProgressWindow(int maxValue, Func<int> getCurrentProgress) : base()
        {
            GetCurrentProgress = getCurrentProgress;
            InitializeComponent();
            ProgressBar.Maximum = maxValue;

        }

        public Func<int> GetCurrentProgress;

    }
}
