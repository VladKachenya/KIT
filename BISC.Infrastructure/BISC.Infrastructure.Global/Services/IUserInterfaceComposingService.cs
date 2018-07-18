using System.Collections.ObjectModel;
using System.Windows.Input;
using BISC.Infrastructure.Global.Modularity;

namespace BISC.Infrastructure.Global.Services
{
    public static class IconsKeys
    {
        public const string ServerPlusIconKey = "ServerPlusIcon";
        public const string SaveIconKey = "SaveIcon";
    }

    public interface IUserInterfaceComposingService
    {
        void AddGlobalCommand(ICommand command,string name,string iconId=null);
        ObservableCollection<IGlobalCommand> GetCommandsWithIcons();
        ObservableCollection<IGlobalCommand> GetAllCommands();

    }
}