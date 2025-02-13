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
using BISC.Modules.Reports.Presentation.ViewModels;

namespace BISC.Modules.Reports.Presentation.Views
{
    /// <summary>
    /// Логика взаимодействия для ReportsConflictsWindow.xaml
    /// </summary>
    public partial class ReportsConflictsWindow : UserControl
    {
        public ReportsConflictsWindow(ReportsConflictsViewModel reportsConflictsViewModel)
        {
            InitializeComponent();
            DataContext = reportsConflictsViewModel;
        }
    }
}
