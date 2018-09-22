using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Presentation.BaseItems.ViewModels;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Matrix.Entities
{
   public class GooseControlBlockAssignmentItem:ViewModelBase
    {
        private string _signature;

        public GooseControlBlockAssignmentItem()
        {
            FcdaAssignmentItems=new ObservableCollection<FcdaAssignmentItem>();
        }

        public string Signature
        {
            get => _signature;
            set { SetProperty(ref _signature , value); }
        }

        public ObservableCollection<FcdaAssignmentItem> FcdaAssignmentItems { get; }
    }
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
        public IFcda Model { get; set; }
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
