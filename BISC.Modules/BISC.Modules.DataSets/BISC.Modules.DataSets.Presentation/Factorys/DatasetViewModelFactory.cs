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
using BISC.Infrastructure.Global.IoC;

namespace BISC.Modules.DataSets.Presentation.Factorys
{
    public class DatasetViewModelFactory : IDatasetViewModelFactory
    {
        private readonly IInjectionContainer _injectionContainer;

        public DatasetViewModelFactory(IInjectionContainer injectionContainer)
        {
            _injectionContainer = injectionContainer;
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
            IDataSetViewModel result = _injectionContainer.ResolveType<IDataSetViewModel>();
            result.SetModel(dataSet);
            return result;
        }
    }
}
