using System.Collections.Generic;
using System.Collections.ObjectModel;
using BISC.Modules.InformationModel.Infrastucture.Elements;

namespace BISC.Modules.InformationModel.Presentation.Interfaces.Factories
{
    public interface IInfoModelTreeFactory
    {
        ObservableCollection<IInfoModelItemViewModel> CreateFullInfoModelTree(List<ILDevice> lDevices,
            bool isFcSortingEnabled,
            ObservableCollection<IInfoModelItemViewModel> existingInfoModelItemViewModels = null);
        ObservableCollection<IInfoModelItemViewModel> CreateLDeviceInfoModelTree(ILDevice lDevices, bool isFcSortingEnabled, ObservableCollection<IInfoModelItemViewModel> existingInfoModelItemViewModels = null);

    }
}