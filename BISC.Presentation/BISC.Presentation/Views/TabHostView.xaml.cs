﻿using System.Windows;
using System.Windows.Controls;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Interfaces;
using BISC.Presentation.Interfaces.Tree;
using BISC.Presentation.ViewModels.Tab;
using CommonServiceLocator;
using Prism.Regions;
using Xceed.Wpf.AvalonDock;

namespace BISC.Presentation.Views
{
    /// <summary>
    /// Логика взаимодействия для TabHost.xaml
    /// </summary>
    public partial class TabHostView : UserControl
    {
        public TabHostView(ITabHostViewModel tabHostViewModel)
        {
            InitializeComponent();
            DataContext = tabHostViewModel;
        }
    }
}