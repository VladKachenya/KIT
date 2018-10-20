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
        public const string UploadNetworkKey = "UploadNetworkIcon";
        public const string AddIconKey = "PlusOutlineIcon";

    }

    public interface IUserInterfaceComposingService
    {
        void AddGlobalCommand(ICommand command,string name,string iconId=null, bool isAddToMenu = false, bool isAddToToolBar = false);
        void DeleteGlobalCommand(ICommand command);

        void SetCurrentSaveCommand(ICommand command, string name,bool isToDevice);
        void ClearCurrentSaveCommand();

        ObservableCollection<IGlobalCommand> GetToolBarCommands();
        ObservableCollection<IGlobalCommand> GetMenuCommands();

    }
}