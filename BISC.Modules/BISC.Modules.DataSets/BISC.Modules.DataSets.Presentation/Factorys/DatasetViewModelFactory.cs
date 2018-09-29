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
using BISC.Presentation.Infrastructure.Factories;

namespace BISC.Modules.DataSets.Presentation.Factorys
{
    public class DatasetViewModelFactory : IDatasetViewModelFactory
    {
        private IFcdaViewModelFactory _fcdaViewModelFactory;
        private readonly ICommandFactory _commandFactory;

        public DatasetViewModelFactory(IFcdaViewModelFactory fcdaViewModelFactory,ICommandFactory commandFactory)
        {
            _fcdaViewModelFactory = fcdaViewModelFactory;
            _commandFactory = commandFactory;
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
            IDataSetViewModel result = new DataSetViewModel( dataSet, _fcdaViewModelFactory,_commandFactory);
            return result;
        }
    }
}
