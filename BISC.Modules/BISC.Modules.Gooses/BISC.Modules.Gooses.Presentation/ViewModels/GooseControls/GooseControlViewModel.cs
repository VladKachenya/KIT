using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Presentation.BaseItems.ViewModels;

namespace BISC.Modules.Gooses.Presentation.ViewModels.GooseControls
{
   public class GooseControlViewModel:ViewModelBase
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

        public string Name
        {
            get => _name;
            set { SetProperty(ref _name , value); }
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
            get => _availableDatasets;
            set { SetProperty(ref _availableDatasets, value); }
        }

        public bool FixedOffs
        {
            get { return _fixedOffs; }
            set
            {
               SetProperty(ref _fixedOffs , value);
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

        public string GseType => "GOOSE";

        public bool IsDynamic
        {
            get { return _isDynamic; }
            set
            {
                SetProperty(ref _isDynamic, value,true);
            }
        }
    }
}
