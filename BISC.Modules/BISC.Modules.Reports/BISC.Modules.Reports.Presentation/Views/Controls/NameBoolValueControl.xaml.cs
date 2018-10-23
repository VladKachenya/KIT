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

namespace BISC.Modules.Reports.Presentation.Views.Controls
{
    /// <summary>
    /// Логика взаимодействия для NameBoolValueControl.xaml
    /// </summary>
    public partial class NameBoolValueControl : UserControl
    {
        public static readonly DependencyProperty TextOfNameProperty =
            DependencyProperty.Register("TextOfName", typeof(string), typeof(NameBoolValueControl), new PropertyMetadata(TextOfNamePropertyChenged));

        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(NameBoolValueControl), new PropertyMetadata(IsCheckedPropertyChenged));

        public NameBoolValueControl()
        {
            InitializeComponent();
        }

        public string TextOfName
        {
            get { return (string)GetValue(TextOfNameProperty); }
            set { SetValue(TextOfNameProperty, value); }
        }

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        private static void TextOfNamePropertyChenged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as NameBoolValueControl).NameTextBox.Text = e.NewValue.ToString();
        }

        private static void IsCheckedPropertyChenged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as NameBoolValueControl).VelueCheckBox.IsChecked = (bool)e.NewValue;
        }
    }
}
