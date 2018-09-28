using BISC.Modules.DataSets.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.DataSets.Infrastructure.ViewModels.Factorys
{
    public interface IFcdaViewModelFactory
    {
        ObservableCollection<IFcdaViewModel> GetFcdaViewModelCollection(IDataSet model);
        IFcdaViewModel GetFcdaViewModelElement(IFcda model);
   
    }
}
