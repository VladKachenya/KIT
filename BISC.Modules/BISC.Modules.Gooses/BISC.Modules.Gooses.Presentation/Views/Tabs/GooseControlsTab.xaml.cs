﻿using System;
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
using BISC.Modules.Gooses.Presentation.ViewModels.Tabs;

namespace BISC.Modules.Gooses.Presentation.Views.Tabs
{
    /// <summary>
    /// Логика взаимодействия для GooseEditingTab.xaml
    /// </summary>
    public partial class GooseControlsTab : System.Windows.Controls.UserControl
    {
        public GooseControlsTab(GooseControlsTabViewModel gooseControlsTabViewModel)
        {
            InitializeComponent();
            DataContext = gooseControlsTabViewModel;
        }
    }
}
