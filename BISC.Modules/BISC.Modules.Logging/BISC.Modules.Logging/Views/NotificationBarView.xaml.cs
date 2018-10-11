using System.Windows.Controls;

namespace BISC.Modules.Logging.Infrastructure.Views
{
    /// <summary>
    /// Interaction logic for NotificationBarView.xaml
    /// </summary>
    public partial class NotificationBarView : UserControl
    {
        public NotificationBarView(NotificationBarViewModel notificationBarViewModel)
        {
            InitializeComponent();
            DataContext = notificationBarViewModel;
        }
    }
}
