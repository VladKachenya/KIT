using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BISC.GlobalServices;
using BISC.Infrastructure.CompositionRoot.Bootstraper;
using BISC.Infrastructure.Global.Modularity;
using BISC.Infrastructure.Global.Services;
using BISC.Interfaces;
using BISC.Presentation.Infrastructure.ViewModel;
using BISC.ViewModel;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Unity.Lifetime;
using ContainerControlledLifetimeManager = Microsoft.Practices.Unity.ContainerControlledLifetimeManager;

namespace BISC.Bootstrapper
{
   public class ShellBootstrapper: ApplicationBootstrapper
   {
       private bool _isRunWithDefaultConfiguration;


        #region Overrides of UnityBootstrapper

        public override void Run(bool runWithDefaultConfiguration)
        {
            _isRunWithDefaultConfiguration = runWithDefaultConfiguration;
            base.Run(runWithDefaultConfiguration);
        }

        #endregion

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow = (Window)Shell;
            if (_isRunWithDefaultConfiguration)
            {
                Application.Current.MainWindow.Show();
            }
        }

        protected override void ConfigureContainer()
        {
            Container.RegisterType<IUserInterfaceComposingService,UserInterfaceComposingService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IShellViewModel, ShellViewModel>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IGlobalCommand, GlobalCommand>();
            Container.RegisterType<IGlobalCommandGroup, GlobalCommandGroup>();
            Container.RegisterType<IUserNotificationService, UserNotificationService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IConfigurationService, ConfigurationService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ShellLoadedService>(new ContainerControlledLifetimeManager());
            base.ConfigureContainer();
            Container.RegisterInstance<IApplicationTitle>(Container.Resolve<IShellViewModel>());
            Container.RegisterType<Shell>();
        }
    }

}
