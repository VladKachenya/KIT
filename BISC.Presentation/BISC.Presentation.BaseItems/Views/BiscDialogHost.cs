using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;

namespace BISC.Presentation.BaseItems.Views
{
    public class BiscDialogHost:ContentControl
    {
        private DialogHost _dialogHost;


        public BiscDialogHost()
        {
            _dialogHost = new DialogHost();
        }

        #region Overrides of ContentControl

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            _dialogHost.Content = newContent;
            base.OnContentChanged(oldContent, _dialogHost);
        }

        #endregion


        public string Identifier
        {
            get { return (string)GetValue(IdentifierProperty); }
            set { SetValue(IdentifierProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Identifier.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IdentifierProperty =
            DependencyProperty.Register("Identifier", typeof(string), typeof(BiscDialogHost), new PropertyMetadata("", OnIdentifierChanged));

        private static void OnIdentifierChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as BiscDialogHost)._dialogHost.Identifier = e.NewValue;
        }
    }
}