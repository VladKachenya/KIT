using BISC.Modules.Gooses.Presentation.Interfaces.GooseSubscriptionLight;
using BISC.Presentation.BaseItems.ViewModels;

namespace BISC.Modules.Gooses.Presentation.ViewModels.GooseSubscriptionLight
{
    public class GooseDataReferenceViewModel : ViewModelBase, IGooseDataReferenceViewModel
    {
        public string DeviceName { get; set; }
        public string GooseName { get; set; }
        public string DoiDataReference { get; set; }
        public string DataSetReferenceState { get; set; }
        public string DataSetReferenceQuality { get; set; }
    }
}