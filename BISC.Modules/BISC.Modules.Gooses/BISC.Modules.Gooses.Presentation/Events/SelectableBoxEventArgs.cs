using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Gooses.Presentation.Events
{
    public class SelectableBoxEventArgs
    {
        public SelectableBoxEventArgs(ISelectableValueViewModel selectableValueViewModel)
        {
            SelectableValueViewModel = selectableValueViewModel;
        }
        public ISelectableValueViewModel SelectableValueViewModel { get; }
    }
}
