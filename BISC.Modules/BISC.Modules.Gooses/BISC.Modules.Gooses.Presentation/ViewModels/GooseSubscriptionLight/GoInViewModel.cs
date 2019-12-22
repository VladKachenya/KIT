using System;
using BISC.Modules.Gooses.Presentation.Interfaces.GooseSubscriptionLight;
using BISC.Presentation.BaseItems.ViewModels;

namespace BISC.Modules.Gooses.Presentation.ViewModels.GooseSubscriptionLight
{
    public class GoInViewModel : ViewModelBase, IGoInViewModel
    {
        public GoInViewModel()
        {
            IsSetSateEnable = true;
            IsSetQualityEnable = true;
            IsSetGooseMonitoringEnable = true;
        }
        private IGooseDataReferenceViewModel _gooseDataReferenceViewModel;
        private bool _enableState;
        private bool _enableQuality;
        private bool _enableGooseMonitoring;
        private bool _isSetSateEnable;
        private bool _isSetQualityEnable;
        private bool _isSetGooseMonitoringEnable;
        public string Name { get; set; }
        public int Number { get; set; }

        public bool EnableState
        {
            get => _enableState;
            set => SetProperty(ref _enableState, value);
        }

        public bool IsSetSateEnable
        {
            get => _isSetSateEnable;
            set
            {
                _isSetSateEnable = value;
                OnPropertyChanged();
            }
        }

        public bool EnableQuality
        {
            get => _enableQuality;
            set => SetProperty(ref _enableQuality, value);
        }

        public bool IsSetQualityEnable
        {
            get => _isSetQualityEnable;
            set
            {
                _isSetQualityEnable = value;
                OnPropertyChanged();
            }
        }

        public bool EnableGooseMonitoring
        {
            get => _enableGooseMonitoring;
            set => SetProperty(ref _enableGooseMonitoring, value);
        }

        public bool IsSetGooseMonitoringEnable
        {
            get => _isSetGooseMonitoringEnable;
            set
            {
                _isSetGooseMonitoringEnable = value;
                OnPropertyChanged();
            }
        }

        public IGooseDataReferenceViewModel GooseDataReferenceViewModel
        {
            get => _gooseDataReferenceViewModel;
            set
            {
                //if (string.IsNullOrWhiteSpace(value.DataSetReferenceState) ||
                //    value.DataSetReferenceState.ToLower() == "нет")
                //{
                //    IsSetSateEnable = false;
                //}
                //else
                //{
                //    IsSetSateEnable = true;
                //}

                //if (string.IsNullOrWhiteSpace(value.DataSetReferenceQuality) ||
                //    value.DataSetReferenceQuality.ToLower() == "нет")
                //{
                //    IsSetQualityEnable = false;
                //    IsSetGooseMonitoringEnable = false;
                //}
                //else
                //{
                //    IsSetQualityEnable = true;
                //    IsSetGooseMonitoringEnable = true;
                //}

                //if (string.IsNullOrWhiteSpace(value.DoiDataReference) ||
                //    value.DoiDataReference.ToLower() == "нет")
                //{
                //    IsSetSateEnable = false;
                //    IsSetQualityEnable = false;
                //    IsSetGooseMonitoringEnable = false;
                //}

                SetProperty(ref _gooseDataReferenceViewModel, value);
            }
        }
    }
}