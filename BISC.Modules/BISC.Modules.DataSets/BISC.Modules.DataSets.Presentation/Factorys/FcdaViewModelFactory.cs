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
using BISC.Modules.DataSets.Infrastructure.Services;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.DataSets.Presentation.Factorys
{
    public class FcdaViewModelFactory : IFcdaViewModelFactory
    {
        IInjectionContainer _injectionContainer;
        private readonly IFcdaInfoService _fcdaInfoService;

        public FcdaViewModelFactory(IInjectionContainer injectionContainer, IFcdaInfoService fcdaInfoService)
        {
            _injectionContainer = injectionContainer;
            _fcdaInfoService = fcdaInfoService;
        }
        public ObservableCollection<IFcdaViewModel> CreateFcdaViewModelCollection(IDevice device, IDataSet model)
        {
            ObservableCollection<IFcdaViewModel> result = new ObservableCollection<IFcdaViewModel>();
            foreach (IFcda element in model.ChildModelElements)
                result.Add(CreateFcdaViewModelElement(device, element));
            return result;
        }

        public IFcdaViewModel CreateFcdaViewModelElement(IDevice device,IFcda model)
        {
            var result = _injectionContainer.ResolveType<IFcdaViewModel>();
            result.SetModel(model);
            result.SetWeight(_fcdaInfoService.GetFcdaWeight(device, model));
            return result;
        }
    }
}
