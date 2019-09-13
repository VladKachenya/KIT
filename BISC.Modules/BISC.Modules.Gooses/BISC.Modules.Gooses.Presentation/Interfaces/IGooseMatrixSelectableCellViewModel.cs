using System;

namespace BISC.Modules.Gooses.Presentation.Interfaces
{
    public interface IGooseMatrixSelectableCellViewModel : IDisposable
    {
        bool IsSelectingEnabled { get; set; }
        string ToolTip { get; set; }
        void SetValue(bool value);
        bool SelectedValue { get; set; }
    }
}