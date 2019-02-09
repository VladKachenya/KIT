using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Modules.DataSets.Presentation.HelperEntites;

namespace BISC.Modules.DataSets.Infrastructure.ViewModels
{
    public interface IFcdaViewModel: IDataSetElementBaseViewModel<IFcda>, IFunctionalConstrainter
    {
        IFcda GetFcda();
        string FullName { get; }
        IWeigher ParentWeiger { get; set; }
    }
}
