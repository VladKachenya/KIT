using System.Collections.ObjectModel;
using System.Windows.Input;
using BISC.Infrastructure.Global.Modularity;

namespace BISC.Infrastructure.Global.Services
{
    public static class IconsKeys
    {
        public const string ServerPlusIconKey = "ServerPlusIcon";
        public const string SaveIconKey = "SaveIcon";
        public const string ContentSaveAllKey = "ContentSaveAllIcon";

    }

    public interface IUserInterfaceComposingService
    {
        void AddGlobalCommand(ICommand command,string name,string iconId=null, bool isAddToMenu = false, bool isAddToToolBar = false);
        void SetCurrentSaveCommand(ICommand command, string name);
        void ClearCurrentSaveCommand();

        ObservableCollection<IGlobalCommand> GetToolBarCommands();
        ObservableCollection<IGlobalCommand> GetMenuCommands();

    }
}