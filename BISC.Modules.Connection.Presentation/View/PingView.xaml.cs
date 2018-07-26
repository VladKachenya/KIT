using BISC.Modules.Connection.Presentation.Interfaces.Ping;
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

namespace BISC.Modules.Connection.Presentation.View
{
    /// <summary>
    /// Логика взаимодействия для PingsViev.xaml
    /// </summary>
    public partial class PingView : UserControl
    {
        #region private filds
        private readonly List<TextBox> _textBoxes;
        private int _selectedBoxIndex;
        #endregion
        public PingView(IPingViewModel pingAddingViewModel)
        {
            InitializeComponent();
            this._textBoxes = new List<TextBox> { this.IP0, this.IP1, this.IP2, this.IP3};
            DataContext = pingAddingViewModel;         
        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            this._selectedBoxIndex = -1;
            try
            {
                TextBox box = sender as TextBox;
                if (box == null) return;
                int index = this._textBoxes.IndexOf(box);
                //if (e.Key > Key.D0 && e.Key < Key.D9 || e.Key > Key.NumPad0 && e.Key < Key.NumPad0)
                //{
                //    string ch = (new KeyConverter().ConvertToString(e.Key));
                //    if (int.Parse(box.Text.ToString() + ch) > 255)
                //    { 
                //        if (index == 3)
                //        {
                //            this.PingButton.Focus();
                //            return;
                //        }
                //        this.SetFocusAndSelectAll(index + 1);
                //        return;
                //    }
                //}

                if (e.Key == Key.Right && box.CaretIndex == box.Text.Length)
                {
                    if (index == 3) return;
                    this.SetFocusAndSelectAll(index + 1, true);
                }
                else if (e.Key == Key.Left && box.CaretIndex == 0)
                {
                    if (index == 0) return;
                    this.SetFocusAndSelectAll(index - 1, false);
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                TextBox box = sender as TextBox;
                if (box == null) return;
                int index = this._textBoxes.IndexOf(box);
                if (box.Text.Length == 3 && box.CaretIndex == box.Text.Length)
                {
                    if (index == 3) return;
                    
                    this.SetFocusAndSelectAll(index + 1, true);
                }

                //    if (this._selectedBoxIndex != -1)
                //{
                //    this._textBoxes[this._selectedBoxIndex].SelectAll();
                //    this._textBoxes[this._selectedBoxIndex].Focus();
                //}
                //else
                //{
                //    TextBox box = (TextBox)sender;
                //    if (box == null) return;
                //    int index = this._textBoxes.IndexOf(box);
                //    if (box.Text.Length == box.MaxLength && box.CaretIndex == box.Text.Length
                //        && !(e.Key == Key.Left || e.Key == Key.Right))
                //    {
                //        this._textBoxes[index + 1].Focus();
                //    }
                //}
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void SetFocusAndSelectAll( int indexOfTextBoz, bool isSelectAll)
        {
            this._textBoxes[indexOfTextBoz].Focus();
            if (isSelectAll)
            {
                this._textBoxes[indexOfTextBoz].CaretIndex = this._textBoxes[indexOfTextBoz].Text.Length;
                this._textBoxes[indexOfTextBoz].SelectAll();
            }
        }

        private void OnClearPreviewClick(object sender, RoutedEventArgs e)
        {
            SetFocusAndSelectAll(0, true);
        }
    }
}
