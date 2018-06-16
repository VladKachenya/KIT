using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Presentation.Infrastructure.Events;
using BISC.Presentation.Interfaces;

namespace BISC.Presentation.ViewModels.Tab
{
   public class TabHostViewModel: ITabHostViewModel
    {
        public TabHostViewModel()
        {
        
        }
        public ObservableCollection<ITabViewModel> TabViewModels { get; }
    }
}
