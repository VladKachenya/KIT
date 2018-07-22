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

namespace BISC.Presentation.BaseItems.Controls
{
    /// <summary>
    /// Логика взаимодействия для BadgedControl.xaml
    /// </summary>
    public partial class BadgedControl : UserControl
    {
        public BadgedControl()
        {
            InitializeComponent();
        }


        public new object Content
        {
            get { return (object)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public new static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(BadgedControl), new PropertyMetadata(ContentChanged));

        private static void ContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as BadgedControl).Badged.Content = e.NewValue;
        }


        public object BadgeContent
        {
            get { return (object)GetValue(BadgeContentProperty); }
            set { SetValue(BadgeContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BadgeContentProperty =
            DependencyProperty.Register("BadgeContent", typeof(object), typeof(BadgedControl), new PropertyMetadata(BadgeContentChanged));

        private static void BadgeContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as BadgedControl).Badged.Badge = e.NewValue;

        }
    }
}
