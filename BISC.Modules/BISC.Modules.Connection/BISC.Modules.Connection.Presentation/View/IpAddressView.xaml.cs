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
    /// Логика взаимодействия для IpAddressView.xaml
    /// </summary>
    public partial class IpAddressView : UserControl
    {
        #region private fields
        private readonly List<TextBox> _textBoxes;
        private int _selectedBoxIndex;
        #endregion
        public IpAddressView()
        {
            InitializeComponent();
            this._textBoxes = new List<TextBox> { this.IP0, this.IP1, this.IP2, this.IP3 };

        }
      

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            this._selectedBoxIndex = -1;
            try
            {
                TextBox box = sender as TextBox;
                if (box == null) return;
                _selectedBoxIndex = this._textBoxes.IndexOf(box);

                if (e.Key == Key.Right && box.CaretIndex == box.Text.Length)
                {
                    this._textBoxes[_selectedBoxIndex + 1].Focus();
                }
                else if (e.Key == Key.Left && box.CaretIndex == 0)
                {
                    if (box.SelectedText == "")
                        this._textBoxes[_selectedBoxIndex - 1].Focus();
                }
                else if (_selectedBoxIndex < 3 && box.Text.Length == 3 && _textBoxes[_selectedBoxIndex + 1].Text == "")
                {
                    if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                    {
                        this._textBoxes[_selectedBoxIndex + 1].Focus();
                    }
                }
                else if (e.Key == Key.Enter)
                {
                    if (_selectedBoxIndex == 3)
                    {
                        var PingCommand = this.PingButton.Command;
                        if (PingCommand.CanExecute(null))
                            PingCommand.Execute(null);
                    }
                    else
                    {
                        this._textBoxes[_selectedBoxIndex + 1].Focus();
                    }
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
                if (e.Key == Key.Right && _textBoxes[_selectedBoxIndex].CaretIndex == _textBoxes[_selectedBoxIndex].Text.Length)
                {
                    if (_selectedBoxIndex == 3) return;
                    this._textBoxes[_selectedBoxIndex + 1].SelectAll();
                }
                else if (e.Key == Key.Left && _textBoxes[_selectedBoxIndex].CaretIndex == 0)
                {
                    if (_selectedBoxIndex == 0) return;
                    this._textBoxes[_selectedBoxIndex - 1].SelectAll();
                }
                else if (e.Key == Key.Enter && _selectedBoxIndex != 3)
                {
                    this._textBoxes[_selectedBoxIndex + 1].SelectAll();
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

      





    }
}
