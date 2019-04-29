using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BISC.Infrastructure.Global.Modularity;
using BISC.Infrastructure.Global.Services;
using BISC.Presentation.Interfaces.Menu;
using MaterialDesignThemes.Wpf;

namespace BISC.Presentation.ViewModels
{
   public class HamburgerMenuViewModel: IHamburgerMenuViewModel
    {
        public HamburgerMenuViewModel(IUserInterfaceComposingService userInterfaceComposingService)
        {
            GlobalCommands = userInterfaceComposingService.GetMenuCommands();
            GlobalCommandGroups = userInterfaceComposingService.GetToolBarCommandGroups();
            var f = new FileInfo(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + AppDomain.CurrentDomain.SetupInformation.ApplicationName);
            //FileInfo f = new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
            LastBuildDateTime = $"ОАО Белэлектромонтажналадка\n" +
                                $"Версия программы от {f.LastWriteTime}\n" +
                                $"Разработчики:\n" +
                                $"Старовойтов Юрий\n" +
                                $"Каченя Владислав";
			 
        }
        public ObservableCollection<IGlobalCommand> GlobalCommands { get; }

        public ObservableCollection<IGlobalCommandGroup> GlobalCommandGroups { get; }
	   public string LastBuildDateTime { get; }

	}
}
