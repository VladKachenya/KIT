﻿using BISC.Modules.Connection.Presentation.Interfaces.Ping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BISC.Modules.Connection.Presentation.View
{
    /// <summary>
    /// Логика взаимодействия для PingsViev.xaml
    /// </summary>
    public partial class PingView : UserControl
    {
        public PingView(IPingViewModel pingAddingViewModel)
        {
            InitializeComponent();
            DataContext = pingAddingViewModel;
        }
    }
}
