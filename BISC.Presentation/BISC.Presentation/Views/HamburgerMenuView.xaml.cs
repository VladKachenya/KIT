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
using BISC.Presentation.Interfaces.Menu;
using UserControl = System.Windows.Controls.UserControl;

namespace BISC.Presentation.Views
{
    /// <summary>
    /// Логика взаимодействия для HamburgerMenuView.xaml
    /// </summary>
    public partial class HamburgerMenuView : UserControl
    {

        public HamburgerMenuView(IHamburgerMenuViewModel hamburgerMenuViewModel)
        {
            InitializeComponent();
            DataContext = hamburgerMenuViewModel;
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem)
            {
                var sentMenuItem = sender as MenuItem;
                sentMenuItem.IsSubmenuOpen = true;
            }
        }


        private void UIElement_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem)
            {
                var sentMenuItem = sender as MenuItem;
                sentMenuItem.IsSubmenuOpen = false;
            }
        }
    }
}
