using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BISC.Infrastructure.Global.Services;
using BISC.Interfaces;
using BISC.Presentation.Infrastructure.Events;
using Prism.Commands;
using Prism.Events;

namespace BISC.ViewModel
{
   public class ShellViewModel: IShellViewModel
    {
        private readonly IUserNotificationService _userNotificationService;

        public ShellViewModel(IUserNotificationService userNotificationService,IEventAggregator eventAggregator)
        {
            _userNotificationService = userNotificationService;
            GlobalCommands=new ObservableCollection<IGlobalCommand>();
            GlobalCommands.Add(new GlobalCommand(){CommandName = "Add",Command = new DelegateCommand((() =>
            {
               _userNotificationService.NotifyUserGlobal("Jopa");
            }))});
            ShellLoadedCommand = new DelegateCommand((() => eventAggregator.GetEvent<ShellLoadedEvent>().Publish(new ShellLoadedEventArgs())));

        }
        public ObservableCollection<IGlobalCommand> GlobalCommands { get; }
        public ICommand ShellLoadedCommand { get; }
    }
}
