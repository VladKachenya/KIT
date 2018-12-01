using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.ViewModels;

namespace BISC.Modules.DataSets.Presentation.ViewModels.Helpers
{
   public class DatasetsConflictContext
    {
        public DatasetsConflictContext(ObservableCollection<IDataSetViewModel> datasetViewModelsInDevice, ObservableCollection<IDataSetViewModel> datasetViewModelsInProject)
        {
            DatasetViewModelsInDevice = datasetViewModelsInDevice;
            DatasetViewModelsInProject = datasetViewModelsInProject;
        }

        public ObservableCollection<IDataSetViewModel> DatasetViewModelsInDevice { get; }
        public ObservableCollection<IDataSetViewModel> DatasetViewModelsInProject { get; }

    }
}
