using BISC.Modules.DataSets.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.DataSets.Infrastructure.ViewModels.Factorys
{
    public interface IDatasetViewModelFactory
    {
        IDataSetViewModel CreateDataSetViewModel(List<string> existingNames,IModelElement device);
        ObservableCollection<IDataSetViewModel> GetDataSetsViewModel(List<IDataSet> dataSet);

    }
}
