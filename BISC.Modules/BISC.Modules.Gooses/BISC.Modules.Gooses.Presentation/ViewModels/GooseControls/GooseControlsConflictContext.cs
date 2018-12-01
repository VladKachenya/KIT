using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.Gooses.Presentation.ViewModels.GooseControls
{
   public class GooseControlsConflictContext
    {
        public GooseControlsConflictContext(ObservableCollection<GooseControlViewModel> gooseControlsollectionInDevice,
            ObservableCollection<GooseControlViewModel> gooseControlsCollectionInProject)
        {
            GooseControlsollectionInDevice = gooseControlsollectionInDevice;
            GooseControlsCollectionInProject = gooseControlsCollectionInProject;
        }

        public ObservableCollection<GooseControlViewModel> GooseControlsollectionInDevice { get; }
        public ObservableCollection<GooseControlViewModel> GooseControlsCollectionInProject { get; }

    }
}
