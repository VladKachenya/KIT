using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BISC.Bootstrapper;

namespace BISC
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ShellBootstrapper shellBootstrapper=new ShellBootstrapper();
            shellBootstrapper.Run(true);
            base.OnStartup(e);
        }
    }
}
