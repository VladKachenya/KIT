using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BISC.GlobalServices;
using BISC.Infrastructure.CompositionRoot.Bootstraper;
using BISC.Infrastructure.Global.Services;
using BISC.Interfaces;
using BISC.ViewModel;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Unity.Lifetime;
using ContainerControlledLifetimeManager = Microsoft.Practices.Unity.ContainerControlledLifetimeManager;

namespace BISC.Bootstrapper
{
   public class ShellBootstrapper: ApplicationBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            Container.RegisterType<IServiceLocator>();
            Container.RegisterType<IUserInterfaceComposingService,UserInterfaceComposingService>();
            Container.RegisterType<IShellViewModel, ShellViewModel>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IGlobalCommand, GlobalCommand>();
            Container.RegisterType<IUserNotificationService, UserNotificationService>(new ContainerControlledLifetimeManager());
            base.ConfigureContainer();
            Container.RegisterType<Shell>();
        }
    }

}
