using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Modules.DataSets.Infrastructure.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Model.Infrastructure.Elements;
using BISC.Modules.DataSets.Presentation.Interfaces.ViewModels;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Presentation.Infrastructure.ChangeTracker;

namespace BISC.Modules.DataSets.Infrastructure.ViewModels
{
    public interface IDataSetViewModel  : IDataSetElementBaseViewModel<IDataSet>, IWeigher, IObjectWithChangeTracker
    {
        bool IsEditing { get; set; }
        bool IsChanged { get; set; }
        bool IsInitialized { get; set; }
        bool IsSelect { get; set; }


        ObservableCollection<IFcdaViewModel> FcdaViewModels { get;}
        ICommand DeleteFcdaCommand { get; }
        ICommand AddFcdaToDataset { get; }
        string EditableNamePart { get; set; }
        string SelectedParentLd { get; set; }
        string SelectedParentLn { get; set; }
        List<string> ParentLdList { get; set; }
        List<string> ParentLnList { get; set; }
        void SetParentDevice(IModelElement device);
    }
}
