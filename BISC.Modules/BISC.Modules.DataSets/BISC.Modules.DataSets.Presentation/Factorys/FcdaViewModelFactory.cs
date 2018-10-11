using BISC.Infrastructure.Global.IoC;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.ViewModels;
using BISC.Modules.DataSets.Infrastructure.ViewModels.Factorys;
using BISC.Modules.DataSets.Presentation.ViewModels;
using BISC.Presentation.Infrastructure.Factories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.DataSets.Presentation.Factorys
{
    public class FcdaViewModelFactory : IFcdaViewModelFactory
    {
        IInjectionContainer _injectionContainer;
        public FcdaViewModelFactory(IInjectionContainer injectionContainer)
        {
            _injectionContainer = injectionContainer;
        }
        public ObservableCollection<IFcdaViewModel> CreateFcdaViewModelCollection(IDataSet model)
        {
            ObservableCollection<IFcdaViewModel> result = new ObservableCollection<IFcdaViewModel>();
            foreach (IFcda element in model.ChildModelElements)
                result.Add(CreateFcdaViewModelElement(element));
            return result;
        }

        public IFcdaViewModel CreateFcdaViewModelElement(IFcda model)
        {
            var result = _injectionContainer.ResolveType<IFcdaViewModel>();
            result.SetModel(model);
            return result;
        }
    }
}
