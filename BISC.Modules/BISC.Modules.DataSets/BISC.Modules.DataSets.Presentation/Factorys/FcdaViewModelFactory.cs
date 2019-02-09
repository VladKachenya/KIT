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
using BISC.Modules.DataSets.Infrastructure.ViewModels.Base;
using BISC.Modules.DataSets.Presentation.HelperEntites;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.DataSets.Presentation.Factorys
{
    public class FcdaViewModelFactory : IFcdaViewModelFactory
    {
        private readonly IInjectionContainer _injectionContainer;
        private readonly IFcdaInfoService _fcdaInfoService;

        public FcdaViewModelFactory(IInjectionContainer injectionContainer, IFcdaInfoService fcdaInfoService)
        {
            _injectionContainer = injectionContainer;
            _fcdaInfoService = fcdaInfoService;
        }
        public ObservableCollection<IFcdaViewModel> CreateFcdaViewModelCollection(IDevice device, IDataSet model, IWeigher parentWeiger)
        {
            ObservableCollection<IFcdaViewModel> result = new ObservableCollection<IFcdaViewModel>();
            foreach (IFcda element in model.ChildModelElements)
                result.Add(CreateFcdaViewModelElement(device, element, parentWeiger));
            return result;
        }

        public IFcdaViewModel CreateFcdaViewModelElement(IDevice device,IFcda fcdaModel, IWeigher parentWeiger)
        {
            var result = _injectionContainer.ResolveType<IFcdaViewModel>();
            result.SetModel(fcdaModel);

            var modelElementOfFcda = _fcdaInfoService.GetModelElementFromFcda(device, fcdaModel);
            var fcsOfModelElement = _fcdaInfoService.GetFcsOfModelElement(device, modelElementOfFcda);
            var fcHelperEntities = new List<FcHelperEntity>();
            result.ParentWeiger = parentWeiger;
            foreach (var fc in fcsOfModelElement)
            {
                int weight = _fcdaInfoService.GetModelElementWeight(device, modelElementOfFcda, fc);
                fcHelperEntities.Add(new FcHelperEntity(fc, weight));
            }
            result.SetFcCollection(fcHelperEntities);
        
            return result;
        }
    }
}
