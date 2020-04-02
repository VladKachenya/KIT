using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Modules.Gooses.Presentation.ViewModels.GooseControls
{
    public class GooseControlViewModel : ComplexViewModelBase
    {
        private bool _fixedOffs;
        private uint _minTime;
        private uint _maxTime;
        private uint _vlanPriority;
        private uint _vlanId;
        private string _macAddress;
        private uint _appId;
        private bool _isDynamic;
        private string _name;
        private string _selectedDataset;
        private string _goId;
        private List<string> _availableDatasets;
        private int _confRev;
        private bool _isChanged;

        public string Name
        {
            get => _name;
            set
            {
                if (!StaticStringValidationService.NameValidation(value)) return;
                SetProperty(ref _name, value);
            }
        }

        public string GoId
        {
            get => _goId;
            set { SetProperty(ref _goId, value); }
        }

        public string SelectedDataset
        {
            get => _selectedDataset;
            set { SetProperty(ref _selectedDataset, value); }
        }

        public List<string> AvailableDatasets
        {
            get
            {
                return _availableDatasets;
            }
            set
            {
                SetProperty(ref _availableDatasets, value);
            }
        }

        public bool FixedOffs
        {
            get { return _fixedOffs; }
            set
            {
                SetProperty(ref _fixedOffs, value);
            }
        }

        public uint MinTime
        {
            get { return _minTime; }
            set
            {
                if (value < 0) value = 0;
                if (value > 2147483647) value = 2147483647;
                SetProperty(ref _minTime, value);

            }
        }

        public uint MaxTime
        {
            get { return _maxTime; }
            set
            {
                if (value < 0) value = 0;
                if (value > 2147483647) value = 2147483647;
                SetProperty(ref _maxTime, value);

            }
        }

        public uint VlanPriority
        {
            get { return _vlanPriority; }
            set


            {
                if (value < 0) value = 0;
                if (value > 7) value = 7;
                SetProperty(ref _vlanPriority, value);


            }
        }

        public uint VlanId
        {
            get { return _vlanId; }
            set
            {
                if (value < 0) value = 0;
                if (value > 4095) value = 4095;
                SetProperty(ref _vlanId, value);


            }
        }

        public uint AppId
        {
            get { return _appId; }
            set
            {
                SetProperty(ref _appId, value);


            }
        }

        public string MacAddress
        {
            get { return _macAddress; }
            set
            {
                SetProperty(ref _macAddress, value);
            }
        }

        public string GseType { get; set; }

        public int ConfRev
        {
            get => _confRev;
            set => SetProperty(ref _confRev, value);
        }

        public string LdInst { get; set; }

        public bool IsDynamic => true;

        public override void SetIsReadOnly(bool isReadOnly)
        {
            base.SetIsReadOnly(isReadOnly || !IsDynamic);
        }
        
        public bool IsChanged
        {
            get => _isChanged;
            set => SetProperty(ref _isChanged, value, true);
        }
    }
}
