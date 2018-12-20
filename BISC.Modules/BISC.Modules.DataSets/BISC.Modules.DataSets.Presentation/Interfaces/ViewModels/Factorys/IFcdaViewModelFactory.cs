using BISC.Modules.DataSets.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.Device.Infrastructure.Model;

namespace BISC.Modules.DataSets.Infrastructure.ViewModels.Factorys
{
    public interface IFcdaViewModelFactory
    {
        ObservableCollection<IFcdaViewModel> CreateFcdaViewModelCollection(IDevice device, IDataSet model);
        IFcdaViewModel CreateFcdaViewModelElement(IDevice device,IFcda model);
   
    }
}
