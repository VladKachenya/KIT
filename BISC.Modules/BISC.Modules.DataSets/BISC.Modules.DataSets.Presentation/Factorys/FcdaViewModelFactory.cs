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
        private ICommandFactory _commandFactory;
        public FcdaViewModelFactory(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
        }
        public ObservableCollection<IFcdaViewModel> GetFcdaViewModelCollection(IDataSet model)
        {
            ObservableCollection<IFcdaViewModel> result = new ObservableCollection<IFcdaViewModel>();
            foreach (IFcda element in model.ChildModelElements)
                result.Add(GetFcdaViewModelElement(element));
            return result;
        }

        public IFcdaViewModel GetFcdaViewModelElement(IFcda model)
        {
            return new FcdaViewModel(model, _commandFactory);
        }
    }
}
