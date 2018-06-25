using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Presentation.Infrastructure.Tree;
using BISC.Presentation.Interfaces.Tree;

namespace BISC.Presentation.ViewModels.Tree
{
    public class MainTreeViewModel : IMainTreeViewModel
    {
        public MainTreeViewModel()
        {
            MainTreeItems=new ObservableCollection<IMainTreeItem>();
        }

        public ObservableCollection<IMainTreeItem> MainTreeItems { get; }
    }
}
