using System;
using System.Collections.ObjectModel;
using BISC.Model.Infrastructure.Elements;

namespace BISC.Modules.InformationModel.Presentation.Interfaces
{
    public interface IInfoModelItemViewModel
    {
        string Header { get; set; }
        IModelElement Model { get; }
        int Level { get; set; }
        Action<bool?> Checked { get; set; }
        string TypeName { get; }
        bool IsChecked { get; set; }
        string Description { get; set; }
        bool IsCheckable { get; set; }

        ObservableCollection<IInfoModelItemViewModel> ChildInfoModelItemViewModels { get; set; }
        IInfoModelItemViewModel Parent { get; set; }
    }


    public interface IGroupedConfigurationItemViewModel
    {
        bool IsGroupedProperty { get; set; }
    }
}