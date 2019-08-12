using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using BISC.Bootstrapper;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Logging;
using BISC.Infrastructure.Global.Services;

namespace BISC
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Process[] processes = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
            if (processes.Length > 2)
            {
                MessageBox.Show("Приложение \"КИТ\" уже запущено", "Внимание", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                Current.Shutdown();
            }
            else
            {
                ShellBootstrapper shellBootstrapper = new ShellBootstrapper();
                shellBootstrapper.Run(true);
                base.OnStartup(e);
            }
        }

        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
             StaticContainer.CurrentContainer.ResolveType<ILoggingService>()?.LogMessage(
                e.Exception.Message + Environment.NewLine + Environment.NewLine + e.Exception.StackTrace,
                SeverityEnum.Critical);
            MessageBox.Show(e.Exception.Message + Environment.NewLine + Environment.NewLine + e.Exception.StackTrace,
                "Ошибка приложения",MessageBoxButton.OK,MessageBoxImage.Error);
            e.Handled = true;
        }

    }
}