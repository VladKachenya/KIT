using BISC.Infrastructure.Global.Services;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.HelperEntities;
using BISC.Presentation.Infrastructure.Services;
using BISC.Presentation.Interfaces.Tree;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BISC.Presentation.Infrastructure.Keys;

namespace BISC.Presentation.ViewModels.Tree
{
    public class MainTreeViewModel : ViewModelBase, IMainTreeViewModel
    {
        private readonly ISaveCheckingService _saveCheckingService;
        private readonly IProjectService _projectService;
        private readonly ObservableCollection<ITreeItemViewModel> _childItemViewModels;

        public MainTreeViewModel(ISaveCheckingService saveCheckingService, ICommandFactory commandFactory, IProjectService projectService)
        {
            _saveCheckingService = saveCheckingService;
            _projectService = projectService;
            _saveCheckingService.AddSaveCheckingEntity(new SaveCheckingEntity(ChangeTracker, "Дерево устройств проекта", null, new Guid(KeysForNavigation.AppGuids.NullGuid)));
            SetProperty(ref _childItemViewModels, new ObservableCollection<ITreeItemViewModel>(), false, nameof(ChildItemViewModels));
        }

        private async Task SaveAsync()
        {
            _projectService.SaveCurrentProject();
        }


        public ObservableCollection<ITreeItemViewModel> ChildItemViewModels => _childItemViewModels;
    }
}
