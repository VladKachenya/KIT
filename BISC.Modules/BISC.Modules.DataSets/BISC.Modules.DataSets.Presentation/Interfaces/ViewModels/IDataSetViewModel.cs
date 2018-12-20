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
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Presentation.Infrastructure.ChangeTracker;

namespace BISC.Modules.DataSets.Infrastructure.ViewModels
{
    public interface IDataSetViewModel  : IDataSetElementBaseViewModel<IDataSet>,IObjectWithChangeTracker
    {
        int MaxSizeFcdaList { get; }
        bool IsExpanded { get; set; }
        bool IsEditing { get; set; }
        bool IsChanged { get; set; }
        int Weight { get; }
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
