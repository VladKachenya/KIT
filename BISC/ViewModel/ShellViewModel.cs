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
using BISC.Presentation.BaseItems.Events;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Keys;
using BISC.Presentation.Infrastructure.Services;
using Prism.Commands;
using Prism.Events;

namespace BISC.ViewModel
{
   public class ShellViewModel:ViewModelBase, IShellViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationService _navigationService;
        private readonly IProjectService _projectService;
        private bool _isLeftMenuEnabled;
        private string _applicationTitle;

        public ShellViewModel(IEventAggregator eventAggregator,INavigationService navigationService,IProjectService projectService)
        {
           
               _eventAggregator = eventAggregator;
            _navigationService = navigationService;
            _projectService = projectService;
            ShellLoadedCommand = new DelegateCommand(OnShellLoaded);
        }

        private void OnShellLoaded()
        {
            _eventAggregator.GetEvent<ShellLoadedEvent>().Publish(new ShellLoadedEventArgs());
            ApplicationTitle = $"Bemn Intellectual Substation Control (Текущий проект: {_projectService.GetCurrentProjectPath(false)})";

        }


        public ICommand ShellLoadedCommand { get; }

  

        public string ApplicationTitle
        {
            get => _applicationTitle;
            set
            {
               
                SetProperty(ref _applicationTitle, value);
            }
        }
    }
}
