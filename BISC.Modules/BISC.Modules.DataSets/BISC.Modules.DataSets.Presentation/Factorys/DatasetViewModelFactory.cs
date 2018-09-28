using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.ViewModels;
using BISC.Modules.DataSets.Infrastructure.ViewModels.Factorys;
using BISC.Modules.DataSets.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.DataSets.Presentation.Factorys
{
    public class DatasetViewModelFactory : IDatasetViewModelFactory
    {
        private IFcdaViewModelFactory _fcdaViewModelFactory;
        public DatasetViewModelFactory(IFcdaViewModelFactory fcdaViewModelFactory)
        {
            _fcdaViewModelFactory = fcdaViewModelFactory;
        }
        public ObservableCollection<IDataSetViewModel> GetDataSetsViewModel(List<IDataSet> dataSets)
        {
            ObservableCollection<IDataSetViewModel> result = new ObservableCollection<IDataSetViewModel>();
            foreach (var element in dataSets)
                result.Add(GetDataSetViewModel(element));
            return result;
        }

        public IDataSetViewModel GetDataSetViewModel(IDataSet dataSet)
        {
            IDataSetViewModel result = new DataSetViewModel( dataSet, _fcdaViewModelFactory);
            return result;
        }
    }
}
