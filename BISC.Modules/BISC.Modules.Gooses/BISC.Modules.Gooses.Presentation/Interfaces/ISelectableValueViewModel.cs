using System;
using System.Windows.Input;

namespace BISC.Modules.Gooses.Presentation.Interfaces
{
    public interface ISelectableValueViewModel : IDisposable
    {
        int ColumnNumber { get; set; }
        IGooseRowViewModel Parent { get; set; }
        bool SelectedValue { get; set; }
        ICommand OnMouseEnterCommand { get; }
        bool IsSelectingEnabled { get; set; }
        string ToolTip { get; set; }
        void SetValue(bool value);
    }
}