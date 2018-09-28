using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISC.Modules.DataSets.Infrastructure.ViewModels
{
    public interface IDataSetViewModel  : IDataSetElementBaseViewModel<IDataSet>
    {
        ObservableCollection<IFcdaViewModel> FcdaViewModels { get;}
    }
}
