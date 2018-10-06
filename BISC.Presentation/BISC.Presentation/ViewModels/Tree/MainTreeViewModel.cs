using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Infrastructure.Global.Services;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Infrastructure.Tree;
using BISC.Presentation.Interfaces.Tree;

namespace BISC.Presentation.ViewModels.Tree
{
    public class MainTreeViewModel :ViewModelBase, IMainTreeViewModel
    {
        private readonly ISaveCheckingService _saveCheckingService;
        private readonly IProjectService _projectService;
        private readonly ObservableCollection<ITreeItemViewModel> _childItemViewModels;

        public MainTreeViewModel(ISaveCheckingService saveCheckingService,ICommandFactory commandFactory,IProjectService projectService)
        {
            _saveCheckingService = saveCheckingService;
            _projectService = projectService;
            _saveCheckingService.AddSaveCheckingEntity(new SaveCheckingEntity(ChangeTracker,"Основное дерево проекта",commandFactory.CreatePresentationCommand(OnSaveCommand)));
            SetProperty(ref _childItemViewModels, new ObservableCollection<ITreeItemViewModel>(),false,nameof(ChildItemViewModels));
        }

        private void OnSaveCommand()
        {
            _projectService.SaveCurrentProject();
        }


        public ObservableCollection<ITreeItemViewModel> ChildItemViewModels => _childItemViewModels;
    }
}
