using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BISC.Infrastructure.Global.Services;
using BISC.Interfaces;
using Prism.Commands;

namespace BISC.ViewModel
{
   public class ShellViewModel: IShellViewModel
    {
        private readonly IUserNotificationService _userNotificationService;

        public ShellViewModel(IUserNotificationService userNotificationService)
        {
            _userNotificationService = userNotificationService;
            GlobalCommands=new ObservableCollection<IGlobalCommand>();
            GlobalCommands.Add(new GlobalCommand(){CommandName = "Add",Command = new DelegateCommand((() =>
            {
               _userNotificationService.NotifyUserGlobal("Jopa");
            }))});
        }
        public ObservableCollection<IGlobalCommand> GlobalCommands { get; }
    }
}
