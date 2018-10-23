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
    /// Логика взаимодействия для NameValueControl.xaml
    /// </summary>
    public partial class NameStringValueControl : UserControl
    {
        public static readonly DependencyProperty TextOfNameProperty = 
            DependencyProperty.Register("TextOfName", typeof(string), typeof(NameStringValueControl), new PropertyMetadata(TextOfNamePropertyChenged));

        public static readonly DependencyProperty TextOfValueProperty =
            DependencyProperty.Register("TextOfValue", typeof(string), typeof(NameStringValueControl), new PropertyMetadata(TextOfValuePropertyChenged));
        public NameStringValueControl()
        {
            InitializeComponent();
        }   

        public string TextOfName
        {
            get { return (string)GetValue(TextOfNameProperty); }
            set { SetValue(TextOfNameProperty, value); }
        }

        public string TextOfValue
        {
            get { return (string)GetValue(TextOfValueProperty); }
            set { SetValue(TextOfValueProperty, value); }
        }

        private static void TextOfNamePropertyChenged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as NameStringValueControl).NameTextBox.Text = e.NewValue.ToString();
        }

        private static void TextOfValuePropertyChenged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as NameStringValueControl).ValueTextBox.Text = e.NewValue.ToString();
        }

    }
}
