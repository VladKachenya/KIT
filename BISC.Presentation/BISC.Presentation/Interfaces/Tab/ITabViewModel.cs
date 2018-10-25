using System;
using System.Windows.Input;
using BISC.Presentation.Docking;
using BISC.Presentation.Infrastructure.Tab;

namespace BISC.Presentation.Interfaces
{
    public interface ITabViewModel:IDisposable
    {
        string TabRegionName { get; set; }
        string TabHeader { get; set; }
        bool IsHaveChanges { get; set; }

        ICommand CloseFragmentCommand { get; }
    }
}