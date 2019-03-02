using BISC.Presentation.BaseItems.ViewModels;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Matrix.Entities
{
    public class FcdaAssignmentItem : ViewModelBase
    {
        private string _signature;
        private string _parentDeviceName;
        private bool _isSubscribed;

        public string ParentDeviceName
        {
            get => _parentDeviceName;
            set { SetProperty(ref _parentDeviceName, value); }
        }
        public int IndexOfFcdaInDataSet { get; set; }
        public string Signature
        {
            get => _signature;
            set { SetProperty(ref _signature, value); }
        }
        public bool IsSubscribed
        {
            get => _isSubscribed;
            set { SetProperty(ref _isSubscribed, value); }
        }
    }
}