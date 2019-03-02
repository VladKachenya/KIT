using System;
using System.Collections.Generic;
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
        public const string CloseCircleIconKey ="CloseCircleIcon";
        public const string UpdateIconKey = "ReloadIcon";
        public const string BookMultipleIconKey = "BookMultipleIcon";
        public const string SettingsIconKey = "SettingsIcon";
        public const string EthernetIconKey = "EthernetIcon";
        public const string LanPendingIconKey = "LanPendingIcon";

    }

    public interface IUserInterfaceComposingService
    {
        void AddGlobalCommand(ICommand command,string name,string iconId=null, bool isAddToMenu = false, bool isAddToToolBar = false);
        void AddGlobalCommandGroup( List<ICommand> commands, List<string> names, string groupName, string iconId = null,
            List<string> iconIds = null, bool isAddToMenu = false, bool isAddToToolBar = false);


        void DeleteGlobalCommand(ICommand command);

        void SetCurrentSaveCommand(ICommand command, string name,bool isToDevice);
        void ClearCurrentSaveCommand();

        ObservableCollection<IGlobalCommand> GetToolBarCommands();
        ObservableCollection<IGlobalCommand> GetMenuCommands();
        ObservableCollection<IGlobalCommandGroup> GetToolBarCommandGroups();


    }
}