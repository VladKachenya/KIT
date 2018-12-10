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
using BISC.Modules.Connection.Presentation.Interfaces.ViewModel.ChangeIpNetworkCard;

namespace BISC.Modules.Connection.Presentation.View.ChangeIpNetworkCard
{
    /// <summary>
    /// Логика взаимодействия для ChangeIpNetworkCardView.xaml
    /// </summary>
    public partial class ChangeIpNetworkCardView : UserControl
    {
        public ChangeIpNetworkCardView(IChangeIpNetworkCardViewModel changeIpNetworkCardViewModel)
        {
            InitializeComponent();
            this.DataContext = changeIpNetworkCardViewModel;
        }
    }
}
