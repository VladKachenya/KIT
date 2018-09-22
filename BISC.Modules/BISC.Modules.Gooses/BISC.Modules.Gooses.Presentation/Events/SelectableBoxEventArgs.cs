using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Gooses.Presentation.Interfaces;

namespace BISC.Modules.Gooses.Presentation.Events
{
    public class SelectableBoxEventArgs
    {
        public bool IsFocused { get; }

        public SelectableBoxEventArgs(ISelectableValueViewModel selectableValueViewModel,bool isFocused)
        {
            IsFocused = isFocused;
            SelectableValueViewModel = selectableValueViewModel;
        }
        public ISelectableValueViewModel SelectableValueViewModel { get; }
        
    }
}
