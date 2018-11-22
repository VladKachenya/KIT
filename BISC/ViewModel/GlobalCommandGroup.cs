using System.Collections.ObjectModel;
using BISC.Infrastructure.Global.Modularity;
using BISC.Presentation.BaseItems.ViewModels;

namespace BISC.ViewModel
{
    public class GlobalCommandGroup : ViewModelBase, IGlobalCommandGroup
    {
        public GlobalCommandGroup()
        {
                GlobalCommandsGroup = new ObservableCollection<IGlobalCommand>();
        }

        public ObservableCollection<IGlobalCommand> GlobalCommandsGroup { get;}
        public string CommandsName { get; set ; }
        public string IconId { get; set; }
    }
}