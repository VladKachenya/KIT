using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private Timer _timer;
        private readonly int _maxValue;
        public ProgressWindow(int maxValue, Func<int> getCurrentProgress) : base()
        {
            _maxValue = maxValue;
            GetCurrentProgress = getCurrentProgress;
            InitializeComponent();
            ProgressBar.Maximum = maxValue;
            var tm = new TimerCallback(UpdateProgressBar);
            _timer = new Timer(tm, null, 0, 100);
        }

        private void UpdateProgressBar(object state)
        {
            Dispatcher?.Invoke(() => ProgressBar.Value = GetCurrentProgress());
        }

        public void CloseWindow()
        {
	        _timer.Dispose();
	        Dispatcher?.Invoke(this.Close);
		}
        public Func<int> GetCurrentProgress;
    }
}
