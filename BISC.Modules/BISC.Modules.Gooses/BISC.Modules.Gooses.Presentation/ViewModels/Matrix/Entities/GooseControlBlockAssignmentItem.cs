using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Modules.DataSets.Infrastructure.Model;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;

namespace BISC.Modules.Gooses.Presentation.ViewModels.Matrix.Entities
{
   public class GooseControlBlockAssignmentItem:ViewModelBase
    {
        private string _signature;

        public GooseControlBlockAssignmentItem(ICommandFactory commandFactory)
        {
            SelectAllCommand = commandFactory.CreatePresentationCommand(OnSelectAll);
            UnSelectAllCommand = commandFactory.CreatePresentationCommand(OnUnSelectAll);

            FcdaAssignmentItems = new ObservableCollection<FcdaAssignmentItem>();
        }

        private void OnUnSelectAll()
        {
            foreach (var fcdaAssignmentItem in FcdaAssignmentItems)
            {
                fcdaAssignmentItem.IsSubscribed = false;

            }
        }

        private void OnSelectAll()
        {
            foreach (var fcdaAssignmentItem in FcdaAssignmentItems)
            {
                fcdaAssignmentItem.IsSubscribed = true;
            }
        }

        public string Signature
        {
            get => _signature;
            set { SetProperty(ref _signature , value); }
        }

        public ICommand SelectAllCommand { get; }
        public ICommand UnSelectAllCommand { get; }
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
